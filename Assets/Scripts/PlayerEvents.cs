using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEvents : MonoBehaviour
{
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
    }

    void CheckInputSource()
    {
        if (OVRInput.GetDown(OVRInput.Button.Any, OVRInput.Controller.Remote))
        {

        }

        if (OVRInput.GetDown(OVRInput.Button.Any, OVRInput.Controller.LTrackedRemote))
        {

        }
        
        if (OVRInput.GetDown(OVRInput.Button.Any, OVRInput.Controller.RTrackedRemote))
        {

        }
    }

    void CheckInput()
    {

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
        return null;
    }

}
