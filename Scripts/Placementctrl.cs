using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(ARRaycastManager))]
public class Placementctrl : MonoBehaviour
{
    public ARPlacement Arplacement;
    public GameObject PlacedPrefab;
    public GameObject PlacedPrefab2;
    public GameObject placementIndicator;
    public GameObject spawed;
    private Pose PlacementPose;  // position and rotation
    private ARRaycastManager aRRaycastManager;
    private bool placementPoseIsValid = false;

    private Camera arCamera = null;
    private ARAnchor pendingHostAnchor = null;
    private Google.XR.ARCoreExtensions.ARCloudAnchor cloudAnchor = null;
    //private List<ARCloudAnchor> _pendingCloudAnchors = new List<ARCloudAnchor>();

/*
    public Pose GetCameraPose()
    {
        return new Pose(arCamera.transform.position,
            arCamera.transform.rotation);
    }

    public void QueneAnchor(ARAnchor aRAnchor)
    {
        pendingHostAnchor = aRAnchor;
    }*/

    void Start()
    {
        // Lock screen to portrait.
        Screen.autorotateToLandscapeLeft = false;
        Screen.autorotateToLandscapeRight = false;
        Screen.autorotateToPortraitUpsideDown = false;
        Screen.orientation = ScreenOrientation.Portrait;
        //Application.targetFrameRate = 60;
        aRRaycastManager = FindObjectOfType<ARRaycastManager>();
        
    }

    void Update()
    {
        if (placementPoseIsValid && Input.touchCount > 0 &&
        Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (spawed == null)
            {
                spawed = Instantiate(PlacedPrefab, PlacementPose.position, PlacementPose.rotation);
              
            }
            else
            {
               spawed =  Instantiate(PlacedPrefab2, PlacementPose.position, PlacementPose.rotation);
            }

           // var anchor = ARAnchorManager.AddAnchor(new Pose(PlacementPose.position, PlacementPose.rotation));
           // spawed.transform.parent = anchor.transform;
            
     
        }

        
        updatePose();
        UpdateIndicator();
        

        //Debug.Log(PlacementPose.position);

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (Arplacement.Navigate.activeSelf)
            {
                Application.Quit();
            }
            else
            {
                Arplacement.SwitchToNavigate();
            }
        }


    }
   private void updatePose()
    {
        var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();
        aRRaycastManager.Raycast(screenCenter, hits, TrackableType.Planes);
        placementPoseIsValid = hits.Count > 0;
        if (placementPoseIsValid)
        {
            PlacementPose = hits[0].pose;
         //   Debug.Log("PlacementPoseposition");
            //  GUILayout.Label(new Rect(0, 0, 100, 100), PlacementPose.position);//print anchor
             /*var cameraForward = Camera.current.transform.forward;
             var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
             PlacementPose.rotation = Quaternion.LookRotation(cameraForward);*/
        }
    }
    
    public void Host()
    {

      //  cloudAnchor = Google.XR.ARCoreExtensions.ARAnchorManagerExtensions.HostCloudAnchor(pendingHostAnchor, 1);
       

    }


    private void UpdateIndicator()
    {
        if (placementPoseIsValid )
        {
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(PlacementPose.position,
            PlacementPose.rotation);
           
        }
        else
        {
            placementIndicator.SetActive(false);
        }
    }

    public void OnEnable()
    {
        UpdatePlaneVisibility(true);
    }
    public void OnDisable()
    {
        UpdatePlaneVisibility(false);
    }


    private void UpdatePlaneVisibility(bool visible)
    {
        foreach (var plane in Arplacement.PlaneManager.trackables)
        {
            plane.gameObject.SetActive(visible);
        }
    }

}
