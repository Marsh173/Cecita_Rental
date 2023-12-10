using System.Collections;
using UnityEngine;
using Cinemachine;

public class DollyCameraController : MonoBehaviour
{

    public CinemachineVirtualCamera virtualCamera;
    public float cameraDuration = 5f; // Time it takes to reach the end of the path
    public float endCameraPathPosition = 2f; // The target path position

    private bool isMoving = false;


    [Header("Door")]
    public GameObject objectToRotate1;
    public GameObject objectToRotate2;
    public float rotationDuration = 2f; // Time it takes for the rotation to complete (in seconds)
    public Vector3 rotationAmount = new Vector3(0f, 90f, 0f); // The amount to rotate the objects


    [Header("Camera Effects")]
    public float wiggleMagnitude = 0.1f; // Magnitude of the wiggle effect when m_PathPosition is at 0
    public float wiggleFrequency = 5f; // Frequency of the wiggle effect
    public float smoothTime = 1.2f; // Adjust the smooth time as needed


    
    


    private void Start()
    {
        
    }

    void Update()
    {
        StartCoroutine(WiggleEffect());

        if (Input.anyKeyDown && !isMoving)
        {
            StartCoroutine(AnimateIntroCam());
        }

    }

    IEnumerator AnimateIntroCam()
    {
        isMoving = true;

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

                elapsedTimeCamera += Time.deltaTime * 1.2f;
            }

            // Update object rotation
            if (elapsedTimeRotation < rotationDuration)
            {
                float tRotation = Mathf.SmoothStep(0f, 1f, elapsedTimeRotation / rotationDuration);
                float currentRotationAngle = Mathf.Lerp(0f, rotationAmount.y, tRotation);

                // Rotate the objects
                objectToRotate1.transform.rotation = Quaternion.Euler(0f, -currentRotationAngle, 0f);
                objectToRotate2.transform.rotation = Quaternion.Euler(0f, currentRotationAngle, 0f);

                elapsedTimeRotation += Time.deltaTime * 0.6f;
            }

            yield return null;
        }

       // isMoving = false;
        
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
}
