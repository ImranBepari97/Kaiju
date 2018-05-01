using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fracture : MonoBehaviour {
	[SerializeField]
	GameObject fractureObject;

	[SerializeField]
	float forceToBreak;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider coll) {
		if(coll.gameObject.tag == "Player") {
			

			ControllerScript controller;

			//Check if the force of the impact is big enough to fracture
			if(controller = coll.gameObject.GetComponent<ControllerScript>()) {
				
				Debug.Log ("Collision at " + controller.device.velocity.magnitude);
				if(Mathf.Abs(controller.device.velocity.magnitude) > forceToBreak) {
					//Replace?
					Instantiate (fractureObject, transform.position, Quaternion.identity);
					Destroy (this.gameObject);
				}
			}
		}
	}
}
