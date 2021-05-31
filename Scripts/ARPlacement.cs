using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.SceneManagement;
using System;

//[RequireComponent(typeof(ARRaycastManager))]

public class ARPlacement : MonoBehaviour
{



    public ARSessionOrigin SessionOrigin;
     //public ARSession SessionCore;
    // public GameObject ARCoreExtensions;
    public Placementctrl Controller;
    public ARPlaneManager PlaneManager;
    private ARAnchor pendingHostAnchor = null;
    public static ARPlacement instance;

    [Header("AR Foundation")]
    public GameObject Navigate;
    public GameObject NavigateMenu;
    public GameObject Login;
    public GameObject AccountDetails;
    public GameObject StartHosting;
    public GameObject Routes;
    public GameObject SaveButton;



    


    [HideInInspector]
    public ApplicationMode Mode = ApplicationMode.Ready;
    public HashSet<string> ResolvingSet = new HashSet<string>();
    private const string _hasDisplayedStartInfoKey = "HasDisplayedStartInfo";
    private const string _persistentCloudAnchorsStorageKey = "PersistentCloudAnchors";
    private const int _storageLimit = 40;

    public enum ApplicationMode
    {
        Ready,
        Host,
        Resolve,
        
    }

    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }


    public void SwitchTosave()
    {
        // Arplacement.ui.SetActive(true);
        instance.SwitchToRoutes();
        ResetAllViews();
       // Controller.SaveCloudAnchorHistory(_hostedCloudAnchor);

    }

    public void OnNavigateButtonClicked()
    {
        
        // ValidateSSIDS();
        SwitchToNavigateMenu();

    }

    public void ValidateSSIDS()
    {
        ///IF VALID
       // SwitchToNavigateMenu();
    }
    public void SwitchToNavigateMenu()
    {
        ResetAllViews();
        NavigateMenu.SetActive(true);

        SessionOrigin.gameObject.SetActive(false);
        

    }
    //MAIN AR RESOLVING
    public void OnSubmitResolvingRouteDetailsButtonClicked()
    {
       
        SwitchToARResolving();
        Mode = ApplicationMode.Resolve;
    }

    
   
    public void SwitchToARResolving()
    {
        ResetAllViews();
        SaveButton.SetActive(false);
        SetPlatformActive(true);
        SessionOrigin.gameObject.SetActive(true);

    }

    //LOGIN
    public void OnAdminButtonClicked()
    {
       
        SwitchToLogin();
       
    }

    public void SwitchToLogin()
    {
        ResetAllViews();
        Login.SetActive(true);
    }

    public void OnSubmitLoginDetailsButtonClicked()
    {
        
        SwitchToRoutes();
        
    }

    public void SwitchToRoutes()
    {
        ResetAllViews();
        Routes.SetActive(true);

        SessionOrigin.gameObject.SetActive(false);
       

    }

 
    public void OnAddRouteClicked()
    {

        SwitchToStartHosting();
       
    }
    public void SwitchToStartHosting()
    {
        ResetAllViews();
        StartHosting.SetActive(true);
    }
 //MAIN AR HOSTING
    public void OnSubmitHostingRouteDetailsButtonClicked()
    {
        
        SwitchToARHost();
        Mode = ApplicationMode.Host;

    }
    public void SwitchToARHost()
    {
        
        ResetAllViews();
        SaveButton.SetActive(true);
        SetPlatformActive(true);
        SessionOrigin.gameObject.SetActive(true);
        


    }


    //CREATE ACCOUNT
    public void OnCreateAccountButtonClicked()
    {
        
        SwitchToAccountDetails();
    }
    public void SwitchToAccountDetails()
    {
        ResetAllViews();
        AccountDetails.SetActive(true);

        SessionOrigin.gameObject.SetActive(false);
       
    }
    public void OnSubmitAccountDetailsButtonClicked()
    {
        
        SwitchToLogin();
      
    }

    //BACK BUTTON
    public void QuitApp()
    {
       
            Application.Quit();
    }
   

    public void SwitchToNavigate()
    {
        ResetAllViews();
        Mode = ApplicationMode.Ready;
        ResolvingSet.Clear();
        Navigate.SetActive(true);

         SessionOrigin.gameObject.SetActive(false);
        ;
    }



    public void ResetAllViews()
    {
       // Screen.sleepTimeout = SleepTimeout.SystemSetting;
        Login.SetActive(false);
        Navigate.SetActive(false);
        NavigateMenu.SetActive(false);
        AccountDetails.SetActive(false);
        StartHosting.SetActive(false);
        Routes.SetActive(false);
        SaveButton.SetActive(false);
      
        SetPlatformActive(false);



    }
    public void SetPlatformActive(bool active)
    {
       // SessionOrigin.gameObject.SetActive(active);
       // SessionCore.gameObject.SetActive(active);
       // ARCoreExtensions.SetActive(active);
    }




    void Start()
        {
            // Lock screen to portrait.
            Screen.autorotateToLandscapeLeft = false;
            Screen.autorotateToLandscapeRight = false;
            Screen.autorotateToPortraitUpsideDown = false;
            Screen.orientation = ScreenOrientation.Portrait;

            // Enable Persistent Cloud Anchors sample to target 60fps camera capture frame rate
            // on supported devices.
            // Note, Application.targetFrameRate is ignored when QualitySettings.vSyncCount != 0.

           // Application.targetFrameRate = 60;

            SwitchToNavigate();



    }


    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (Navigate.activeSelf)
            {
                Application.Quit();
            }
            else
            {
                SwitchToNavigate();
            }
        }

       
    }



}
       