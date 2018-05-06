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
	[SerializeField]
	bool requiresSupport; //Bool to check if the thing requires support
	[SerializeField]
	GameObject pointPopup;

	bool hasBeenDestroyed; //Stops double spawning
	public int collateralLevel = 0;
	public int pointValue;


	Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		hasBeenDestroyed = false;
	}

	// Update is called once per frame
	void Update () {
		CheckForSupport ();
	}

	void CheckForSupport() {
		if(!hasBeenDestroyed && !Physics.Raycast(transform.position, Vector3.down, 0.3f) && requiresSupport) {
			Break (0);
		}
	}

	void OnCollisionEnter(Collision coll) {
		if(coll.gameObject.tag == "Player") {
			ControllerScript controller;
			//Check if the force of the impact is big enough to fracture
			if(controller = coll.gameObject.GetComponent<ControllerScript>()) {
				if(controller.device.velocity.magnitude > forceToBreak && !hasBeenDestroyed ) {
					Break (0);

				}
			}
		}

		if(coll.gameObject.tag == "Debris") {
			Rigidbody rb; 
			if (rb = coll.gameObject.GetComponent<Rigidbody> ()) {
				if(rb.velocity.magnitude > forceToBreak * 2.25f && !hasBeenDestroyed) {
					Break (coll.gameObject.GetComponent<DebrisManager>().collateralLevel);
				}
			}
		}
	}

	//collateralDegree is whether collateral damage is causing the break
	void Break(int collateralDegree) {
		hasBeenDestroyed = true; //Stop from being destroyed multiple times

		PointSystem.totalPoints += pointValue * (collateralDegree + 1); //Add the points
		GameObject popup = Instantiate(pointPopup, transform.position, Quaternion.identity);

		popup.GetComponent<PointPopup>().textMesh.text = "+" + pointValue * (collateralDegree + 1);
		

		Destroy (this.gameObject); 	//Destroy this and replace it with the fractured version

		GameObject thing;
		if (transform.parent != null) {
			thing = Instantiate (fractureObject, transform.parent.position, Quaternion.identity);
			thing.transform.localScale = new Vector3(transform.parent.localScale.x / fractureModelRatio, transform.parent.localScale.y / fractureModelRatio, transform.parent.localScale.z / fractureModelRatio);
		} else {
			thing = Instantiate (fractureObject, transform.position, Quaternion.identity);
			thing.transform.localScale = new Vector3(transform.localScale.x / fractureModelRatio, transform.localScale.y / fractureModelRatio, transform.localScale.z / fractureModelRatio);
		}
			
		Fracture[] thingsFrac;
		if((thingsFrac = thing.GetComponentsInChildren<Fracture>()) != null) {
			for(int i = 0; i < thingsFrac.Length; i++) {
				thingsFrac [i].collateralLevel = collateralDegree + 1;
			}
		}

		DebrisManager[] debrisSpawn;
		if((debrisSpawn = thing.GetComponentsInChildren<DebrisManager>()) != null) {
			for(int i = 0; i < debrisSpawn.Length; i++) {
				debrisSpawn [i].collateralLevel = collateralDegree + 1;
			}
		}

	}
}
