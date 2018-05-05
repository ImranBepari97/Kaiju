using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerScript : MonoBehaviour {

	public SteamVR_TrackedObject trackedObject;
	public SteamVR_Controller.Device device; //Device lets you access velocity and controls


	//Movement variables
	static ControllerScript currentMovingController; //what controllers being used to drag
	static bool isDragging; //whether theyre moving or not
	static Vector3 oldControllerPosition; //controller position, check distance for movement
	static Vector3 oldPlayerPosition; //use distance to offset player
	public float dragRatio; //drag speed ratio

	// Use this for initialization
	void Start () {
		trackedObject = GetComponent<SteamVR_TrackedObject> ();
		device = SteamVR_Controller.Input ((int)trackedObject.index);

	}
	
	// Update is called once per frame
	void Update () {
		//On press
		if(device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad)) {
			currentMovingController = this;
			isDragging = true;
			oldControllerPosition = transform.localPosition;
			oldPlayerPosition = transform.parent.position;

		}

		//On release
		if(device.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad)) {
			if(currentMovingController == this) {
				isDragging = false;
				currentMovingController = null;
			}
		}

		//When dragging
		if(isDragging) {
			Vector3 controllerDiff = currentMovingController.gameObject.transform.localPosition - oldControllerPosition; //get difference in drag distance
			controllerDiff = new Vector3 (controllerDiff.x * dragRatio, 0 , controllerDiff.z * dragRatio); //fix it
			Vector3 final = new Vector3(oldPlayerPosition.x - controllerDiff.x, transform.parent.position.y , oldPlayerPosition.z - controllerDiff.z); //Offset the player
			transform.parent.position = final;
		}
	}
}
