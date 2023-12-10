using System.Collections;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class DollyCameraController : MonoBehaviour
{
    [Header("Start Menu Position")]
    public CinemachineVirtualCamera virtualCamera;
    public float cameraDuration = 5f; // Time it takes to reach the end of the path
    public float endCameraPathPosition = 2f; // The target path position

    private bool isMoving = false;

    [Header("Door")]
    public GameObject objectToRotate1;
    public GameObject objectToRotate2;
    public float rotationDuration = 2f; // Time it takes for the rotation to complete (in seconds)
    public Vector3 rotationAmount = new Vector3(0f, 90f, 0f); // The amount to rotate the objects
    Quaternion startRotationObject1;
    Quaternion startRotationObject2;


    [Header("Camera Effects")]
    public float wiggleMagnitude = 0.1f; // Magnitude of the wiggle effect when m_PathPosition is at 0
    public float wiggleFrequency = 5f; // Frequency of the wiggle effect
    public float smoothTime = 1.2f; // Adjust the smooth time as needed


    [Header("New Game Position")]
    public float newCameraDuration = 5f; // Time it takes to reach the end of the path
    public float newEndCameraPathPosition = 3f; // The target path position
    public float xRotationAmount = 45f; // Amount to rotate the camera around the X-axis


    [Header("BackToMenu")]
    public float BackCameraDuration = 5f; // Time it takes to reach the end of the path

    [Header("Quit Game")]
    public GameObject anykey;
    public GameObject quitMenu;
    public float QuitRotationDuration = 10f;


    private void Start()
    {
        startRotationObject1 = objectToRotate1.transform.rotation;
        startRotationObject2 = objectToRotate2.transform.rotation;
        quitMenu.SetActive(false);
        anykey.SetActive(true);
    }

    void Update()
    {
        StartCoroutine(WiggleEffect());

        if (Input.anyKeyDown && !isMoving)
        {
            isMoving = true;
            StartCoroutine(AnimateIntroCam());
        }

    }

    IEnumerator AnimateIntroCam()
    {

        // Animation for the Cinemachine Virtual Camera
        float elapsedTimeCamera = 0f;
        float startPathPositionCamera = virtualCamera.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition;

        // Animation for rotating GameObjects
        float elapsedTimeRotation = 0f;

        while (elapsedTimeCamera < cameraDuration || elapsedTimeRotation < rotationDuration)
        {
            // Update camera position
            if (elapsedTimeCamera < cameraDuration)
            {
                float tCamera = Mathf.SmoothStep(0f, 1f, elapsedTimeCamera / cameraDuration);
                float currentPathPositionCamera = Mathf.Lerp(startPathPositionCamera, endCameraPathPosition, tCamera);

                // Set the path position of the virtual camera
                virtualCamera.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition = currentPathPositionCamera;

                elapsedTimeCamera += Time.deltaTime;
            }

            // Update object rotation
            if (elapsedTimeRotation < rotationDuration)
            {
                float tRotation = Mathf.SmoothStep(0f, 1f, elapsedTimeRotation / rotationDuration);
                float currentRotationAngle = Mathf.Lerp(0f, rotationAmount.y, tRotation);

                // Rotate the objects
                objectToRotate1.transform.rotation = Quaternion.Euler(0f, -currentRotationAngle, 0f);
                objectToRotate2.transform.rotation = Quaternion.Euler(0f, currentRotationAngle, 0f);

                elapsedTimeRotation += Time.deltaTime;
            }

            yield return null;
        }

    }


    IEnumerator WiggleEffect()
    {
        float smoothVelocityX = 0f;
        float smoothVelocityY = 0f;
      
        while (!isMoving)
        {
            // Get the current path offset of the virtual camera
            Vector3 currentPathOffset = virtualCamera.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathOffset;

            // Apply the wiggle effect to each component of the path offset
            float wiggleX = Mathf.Sin(Time.time * wiggleFrequency) * wiggleMagnitude;
            float wiggleY = Mathf.Sin(Time.time * wiggleFrequency * 1.5f) * wiggleMagnitude; // Adjust frequency for Y if needed
           

            float targetX = currentPathOffset.x + wiggleX;
            float targetY = currentPathOffset.y + wiggleY;
         

            float clampedX = Mathf.SmoothDamp(currentPathOffset.x, targetX, ref smoothVelocityX, smoothTime);
            float clampedY = Mathf.SmoothDamp(currentPathOffset.y, targetY, ref smoothVelocityY, smoothTime);
           
            Vector3 newOffset = new Vector3(clampedX, clampedY, 0);

            virtualCamera.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathOffset = newOffset;

            yield return null;
        }
    }


    #region Start Menu
    public void OnClickNewGame()
    {
        StartCoroutine(AnimateNewGame());
    }

    IEnumerator AnimateNewGame()
    {
        // Animation for the Cinemachine Virtual Camera
        float elapsedTimeCamera = 0f;
        float startPathPositionCamera = virtualCamera.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition;


        // Initial rotation of the camera
        Quaternion startRotation = virtualCamera.transform.rotation;

        while (elapsedTimeCamera < newCameraDuration)
        {
            // Update camera position
            if (elapsedTimeCamera < newCameraDuration)
            {
                float tCamera = Mathf.SmoothStep(0f, 1f, elapsedTimeCamera / newCameraDuration);
                float currentPathPositionCamera = Mathf.Lerp(startPathPositionCamera, newEndCameraPathPosition, tCamera);

                // Set the path position of the virtual camera
                virtualCamera.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition = currentPathPositionCamera;

                float xRotation = Mathf.Lerp(0f, xRotationAmount, tCamera);
                virtualCamera.transform.rotation = startRotation * Quaternion.Euler(xRotation, 0f, 0f);

                elapsedTimeCamera += Time.deltaTime;
            }
            yield return null;
        }

        Debug.Log("test");
    }


    public void QuitGame()
    {
        StartCoroutine(AnimateQuitGame());
    }


    IEnumerator AnimateQuitGame()
    {
        //show confirmation popup window
        quitMenu.SetActive(true);
        anykey.SetActive(false);

        // Animation for the Cinemachine Virtual Camera
        float elapsedTimeCamera = 0f;
        float startPathPositionCamera = virtualCamera.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition;

        // Animation for rotating GameObjects
        float elapsedTimeRotation = 0f;

        while (elapsedTimeCamera < cameraDuration || elapsedTimeRotation < rotationDuration)
        {
            // Update camera position
            if (elapsedTimeCamera < cameraDuration)
            {
                float tCamera = Mathf.SmoothStep(0f, 1f, elapsedTimeCamera / cameraDuration);
                float currentPathPositionCamera = Mathf.Lerp(startPathPositionCamera, 0, tCamera);

                // Set the path position of the virtual camera
                virtualCamera.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition = currentPathPositionCamera;

                elapsedTimeCamera += Time.deltaTime;
            }

            // Update object rotation
            if (elapsedTimeRotation < rotationDuration)
            {
                
                float tRotation = Mathf.SmoothStep(0f, 1f, elapsedTimeRotation / QuitRotationDuration);
                objectToRotate1.transform.rotation = Quaternion.Slerp(objectToRotate1.transform.rotation, startRotationObject1, tRotation);
                objectToRotate2.transform.rotation = Quaternion.Slerp(objectToRotate2.transform.rotation, startRotationObject2, tRotation);

                elapsedTimeRotation += Time.deltaTime;
            }

            yield return null;
        }

        Debug.Log("happen");

    }

    public void reEnter()
    {
        StartCoroutine(AnimateIntroCam());
    }


    #endregion


    #region New Game Menu
    public void BackToStartMenu()
    {
        StartCoroutine(AnimateBackToStartMenu());
    }

    IEnumerator AnimateBackToStartMenu()
    {
        // Animation for the Cinemachine Virtual Camera
        float elapsedTimeCamera = 0f;
        float startPathPositionCamera = virtualCamera.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition;


        // Initial rotation of the camera
        Quaternion startRotation = virtualCamera.transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(0f, -90f, 0f);

        while (elapsedTimeCamera < BackCameraDuration)
        {
            // Update camera position
            if (elapsedTimeCamera < BackCameraDuration)
            {
                float tCamera = Mathf.SmoothStep(0f, 1f, elapsedTimeCamera / BackCameraDuration);
                float currentPathPositionCamera = Mathf.Lerp(startPathPositionCamera, 2, tCamera);

                // Set the path position of the virtual camera
                virtualCamera.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition = currentPathPositionCamera;

                virtualCamera.transform.rotation = Quaternion.Slerp(startRotation, targetRotation, tCamera);

                elapsedTimeCamera += Time.deltaTime;
            }
            yield return null;
        }

        virtualCamera.transform.rotation = targetRotation;


    }


    #endregion

    
}
