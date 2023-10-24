using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEditor.PackageManager;

public class WallHitCheck : MonoBehaviour
{
    public Camera cam;
    public float distance;
    [SerializeField] private LayerMask mask;
    private int LayerWall;

    [Header("Event On Hitting Wall")]
    public UnityEvent EventOnWallHit;
    public UnityEvent EventOnWallHitExit;
    private bool hasWallHitEnterEventTriggered = false;
    private bool hasWallHitExitEventTriggered = false;

    // Start is called before the first frame update
    void Start()
    {
        LayerWall = LayerMask.NameToLayer("Wall");
    }

    // Update is called once per frame
    void Update()
    {
        #region Wallhit Raycast
        Ray wallRay = new Ray(cam.transform.position, cam.transform.forward);
        Debug.DrawRay(wallRay.origin, wallRay.direction * distance);
        RaycastHit wallhit;

        if (Physics.Raycast(wallRay, out wallhit, distance, mask))
        {
            if (wallhit.transform.gameObject.layer == LayerWall)
            {
                if (!hasWallHitEnterEventTriggered)
                {
                    EventOnWallHit.Invoke();
                    hasWallHitEnterEventTriggered = true;
                    hasWallHitExitEventTriggered = false;
                    Debug.Log("Walllllll");
                }
            }
        }
        else 
        {
            if (!hasWallHitExitEventTriggered)
            {
                EventOnWallHitExit.Invoke(); Debug.Log("nothingggg");
                hasWallHitEnterEventTriggered = false;
                hasWallHitExitEventTriggered= true;
            }
        }
        #endregion
    }
}
