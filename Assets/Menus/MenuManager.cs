using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class MenuManager : MonoBehaviour {

    public Material whiteBall;
    public Material orangeBall;
    
    public SteamVR_Input_Sources right;
    public SteamVR_Input_Sources left;
    public SteamVR_Action_Boolean triggerPress;
    

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log("hi :)");

        if (triggerPress.GetStateDown(right))
        {
            Debug.Log("Right trigger pressed");
        }

        if (SteamVR_Input.GetStateDown("GrabPinch", right, true))
        {
            Debug.Log("Right trigger pressed 2");
        }
	}
}
