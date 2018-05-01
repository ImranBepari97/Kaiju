using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerScript : MonoBehaviour {

	public SteamVR_TrackedObject trackedObject;
	public SteamVR_Controller.Device device; //Device lets you access velocity

	// Use this for initialization
	void Start () {
		trackedObject = GetComponent<SteamVR_TrackedObject> ();
		device = SteamVR_Controller.Input ((int)trackedObject.index);
	}
	
	// Update is called once per frame
	void Update () {
	//	Debug.Log (device.velocity);
	}
}
