using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;
using UnityEngine.UI;
using UnityEngine.Events;
//using UnityEditor.PackageManager;

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

    [Header("Wall Hit UI")]
    public float length;
    RaycastHit hit;
    public GameObject ForwardWallHitUI;
    public GameObject RightWallHitUI;
    public GameObject LeftWallHitUI;

    // Start is called before the first frame update
    void Start()
    {
        LayerWall = LayerMask.NameToLayer("Wall");
    }

    // Update is called once per frame
    void Update()
    {


        #region Forward WallHit UI
        Ray ray_forward = new Ray(cam.transform.position, cam.transform.forward);

        Debug.DrawRay(ray_forward.origin, ray_forward.direction * length, Color.blue);

        if (Physics.Raycast(ray_forward, out hit, length))
        {
            if (hit.transform.gameObject.layer == LayerWall)
            {
                Image UI = ForwardWallHitUI.GetComponent<Image>();
                Color UIColor = ForwardWallHitUI.GetComponent<Image>().color;
                UIColor.a = Mathf.Lerp(UIColor.a, Mathf.InverseLerp(length+2, 0, hit.distance), 1f);
                Color newColor = new Color(UIColor.r, UIColor.g, UIColor.b, UIColor.a);
                UI.color = newColor;
                Debug.Log(UIColor.a);
            }
        }
        else
        {
            Image UI = ForwardWallHitUI.GetComponent<Image>();
            Color UIColor = ForwardWallHitUI.GetComponent<Image>().color;
            UIColor.a = Mathf.Lerp(UIColor.a, 0, 1f);
            Color newColor = new Color(UIColor.r, UIColor.g, UIColor.b, UIColor.a);
            UI.color = newColor;
            //Debug.Log(UIColor.a);
        }
        #endregion

        #region Right WallHit UI
        Ray ray_right = new Ray(cam.transform.position, cam.transform.right);

        Debug.DrawRay(ray_right.origin, ray_right.direction * length, Color.blue);

        if (Physics.Raycast(ray_right, out hit, length))
        {
            if (hit.transform.gameObject.layer == LayerWall)
            {
                Image UI = RightWallHitUI.GetComponent<Image>();
                Color UIColor = RightWallHitUI.GetComponent<Image>().color;
                UIColor.a = Mathf.Lerp(UIColor.a, Mathf.InverseLerp(length + 2, 0, hit.distance), 0.3f);
                Color newColor = new Color(UIColor.r, UIColor.g, UIColor.b, UIColor.a);
                UI.color = newColor;
                //Debug.Log(UIColor.a);
            }
        }
        else
        {
            Image UI = RightWallHitUI.GetComponent<Image>();
            Color UIColor = RightWallHitUI.GetComponent<Image>().color;
            UIColor.a = Mathf.Lerp(UIColor.a, 0, 0.3f);
            Color newColor = new Color(UIColor.r, UIColor.g, UIColor.b, UIColor.a);
            UI.color = newColor;
            //Debug.Log(UIColor.a);
        }
        #endregion

        #region Left WallHit UI
        Ray ray_left = new Ray(cam.transform.position, -cam.transform.right);

        Debug.DrawRay(ray_left.origin, ray_left.direction * length, Color.blue);

        if (Physics.Raycast(ray_left, out hit, length))
        {
            if (hit.transform.gameObject.layer == LayerWall)
            {
                Image UI = LeftWallHitUI.GetComponent<Image>();
                Color UIColor = LeftWallHitUI.GetComponent<Image>().color;
                UIColor.a = Mathf.Lerp(UIColor.a, Mathf.InverseLerp(length + 2, 0, hit.distance), 0.3f);
                Color newColor = new Color(UIColor.r, UIColor.g, UIColor.b, UIColor.a);
                UI.color = newColor;
                //Debug.Log(UIColor.a);
            }
        }
        else
        {
            Image UI = LeftWallHitUI.GetComponent<Image>();
            Color UIColor = LeftWallHitUI.GetComponent<Image>().color;
            UIColor.a = Mathf.Lerp(UIColor.a, 0, 0.3f);
            Color newColor = new Color(UIColor.r, UIColor.g, UIColor.b, UIColor.a);
            UI.color = newColor;
            //Debug.Log(UIColor.a);
        }
        #endregion
    }
}
