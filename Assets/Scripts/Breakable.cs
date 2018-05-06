using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour {

	[SerializeField]
	float forceToBreak;
	[SerializeField]
	int pointValue;
	[SerializeField]
	GameObject pointPopup;

	Rigidbody rb;


	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (rb.velocity + " " + rb.angularVelocity);
	}

	void OnCollisionEnter(Collision coll) {
		if(coll.gameObject.tag == "Player") {
			ControllerScript controller;
			//Check if the force of the impact is big enough to fracture
			if(controller = coll.gameObject.GetComponent<ControllerScript>()) {
				if(controller.device.velocity.magnitude > forceToBreak) {
					Break (0);
				}
			}
		}

		if(coll.gameObject.tag == "Debris") {
			Rigidbody rb; 
			if (rb = coll.gameObject.GetComponent<Rigidbody> ()) {
				if(rb.velocity.magnitude > forceToBreak * 2.25f ) {
					Break (coll.gameObject.GetComponent<DebrisManager>().collateralLevel);
				}
			}
		}
	}

	void Break(int collateralDegree) {
		rb.constraints = RigidbodyConstraints.None;

		PointSystem.totalPoints += pointValue * (collateralDegree + 1); //Add the points
		GameObject popup = Instantiate(pointPopup, transform.position, Quaternion.identity);

		popup.GetComponent<PointPopup>().textMesh.text = "+" + pointValue * (collateralDegree + 1);

		GetComponent<DebrisManager> ().shouldBeManaged = true;
		GetComponent<DebrisManager> ().AddDebris ();

	}

}
