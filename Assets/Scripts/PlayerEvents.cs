using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerEvents : MonoBehaviour
{
    public static UnityAction OnTouchPadUp = null;
    public static UnityAction OnTouchPadDown = null;
    public static UnityAction<OVRInput.Controller, GameObject> OnControllerSource = null;

    public GameObject leftAnchor;
    public GameObject rightAnchor;
    public GameObject headAnchor;

    Dictionary<OVRInput.Controller, GameObject> controllerSets = null;
    OVRInput.Controller inputSource = OVRInput.Controller.None;
    OVRInput.Controller controller = OVRInput.Controller.None;
    bool inputActive = true;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        OVRManager.HMDMounted += PlayerFound;
        OVRManager.HMDUnmounted += PlayerLost;

        controllerSets = CreateControllerSets();
    }

    private void OnDestroy()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!inputActive) { return; }

        CheckForController();

        CheckInputSource();

        CheckInput();
    }

    void CheckForController()
    {
        OVRInput.Controller controllerCheck = controller;

        if (OVRInput.IsControllerConnected(OVRInput.Controller.RTrackedRemote))
        {
            controllerCheck = OVRInput.Controller.RTrackedRemote;
        }
        
        if (OVRInput.IsControllerConnected(OVRInput.Controller.LTrackedRemote))
        {
            controllerCheck = OVRInput.Controller.LTrackedRemote;
        }

        if (!OVRInput.IsControllerConnected(OVRInput.Controller.LTrackedRemote) && !OVRInput.IsControllerConnected(OVRInput.Controller.RTrackedRemote))
        {
            controllerCheck = OVRInput.Controller.Touchpad;
        }

        controller = UpdateSource(controllerCheck, controller);
    }

    void CheckInputSource()
    {
        //if (OVRInput.GetDown(OVRInput.Button.Any, OVRInput.Controller.Remote))
        //{

        //}

        //if (OVRInput.GetDown(OVRInput.Button.Any, OVRInput.Controller.LTrackedRemote))
        //{

        //}

        //if (OVRInput.GetDown(OVRInput.Button.Any, OVRInput.Controller.RTrackedRemote))
        //{

        //}

        inputSource = UpdateSource(OVRInput.GetActiveController(), inputSource);

    }

    void CheckInput()
    {
        if (OVRInput.GetDown(OVRInput.Button.PrimaryTouchpad))
        {
            if (OnTouchPadDown != null)
            {
                OnTouchPadDown();
            }
        }
        if (OVRInput.GetUp(OVRInput.Button.PrimaryTouchpad))
        {
            if (OnTouchPadUp != null)
            {
                OnTouchPadUp();
            }
        }
    }

    OVRInput.Controller UpdateSource(OVRInput.Controller check, OVRInput.Controller previous)
    {
        if (check == previous) { return previous; }

        GameObject controllerObject = null;
        controllerSets.TryGetValue(check, out controllerObject);

        if (controllerObject == null) { controllerObject = headAnchor; }

        if (OnControllerSource != null) { OnControllerSource(check, controllerObject); }

        return check;
    }

    void PlayerFound()
    {
        inputActive = true;
    }

    void PlayerLost()
    {
        inputActive = false;
    }

    Dictionary<OVRInput.Controller,GameObject> CreateControllerSets()
    {
        Dictionary<OVRInput.Controller, GameObject> newSets = new Dictionary<OVRInput.Controller, GameObject>()
        {
            {OVRInput.Controller.LTrackedRemote,leftAnchor },
            {OVRInput.Controller.RTrackedRemote,rightAnchor },
            {OVRInput.Controller.Touchpad,headAnchor }
        };
        return newSets;
    }

}
