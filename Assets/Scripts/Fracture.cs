using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fracture : MonoBehaviour {
	[SerializeField]
	GameObject fractureObject;
	[SerializeField]
	float forceToBreak;
	[SerializeField]
	float fractureModelRatio; //Ratio of the Whole:Fractured size

	bool hasBeenDestroyed; //Stops double spawning


	// Use this for initialization
	void Start () {
		hasBeenDestroyed = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision coll) {
		if(coll.gameObject.tag == "Player") {
			

			ControllerScript controller;

			//Check if the force of the impact is big enough to fracture
			if(controller = coll.gameObject.GetComponent<ControllerScript>()) {
				
				Debug.Log ("Collision at " + controller.device.velocity.magnitude);
				if(Mathf.Abs(controller.device.velocity.magnitude) > forceToBreak && !hasBeenDestroyed) {
					hasBeenDestroyed = true;
					Destroy (this.gameObject);
					//Replace?

					GameObject thing;
					if (transform.parent != null) {
						thing = Instantiate (fractureObject, transform.parent.position, Quaternion.identity);
						thing.transform.localScale = new Vector3(transform.parent.localScale.x / fractureModelRatio, transform.parent.localScale.y / fractureModelRatio, transform.parent.localScale.z / fractureModelRatio);
					} else {
						thing = Instantiate (fractureObject, transform.position, Quaternion.identity);
						thing.transform.localScale = new Vector3(transform.localScale.x / fractureModelRatio, transform.localScale.y / fractureModelRatio, transform.localScale.z / fractureModelRatio);
					}
				}
			}
		}
	}
}
