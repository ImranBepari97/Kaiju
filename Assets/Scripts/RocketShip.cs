using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketShip : MonoBehaviour {

	Rigidbody rb;
	Breakable br;
	DebrisManager dm;
	GameObject fire;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		br = GetComponent<Breakable> ();
		dm = GetComponent<DebrisManager> ();
		fire = transform.Find ("Fire").gameObject;
		fire.SetActive (false);
	}

	// Update is called once per frame
	void Update () {

		if(br == null) {
			fire.SetActive (true);
			RaycastHit hit;
			if (Physics.Raycast (fire.transform.position, fire.transform.forward, out hit, 1.5f)) {
				//Debug.DrawLine (fire.transform.position,fire.transform.position + (-fire.transform.up * 1.5f));
				Fracture fract = hit.collider.gameObject.GetComponent<Fracture> ();
				Breakable breaka = hit.collider.gameObject.GetComponent<Breakable> ();
				if(fract != null) {
					fract.Break (0);
				} else if(breaka != null) {
					breaka.Break (0);
				}
			}
		}

		if(!dm.beingHeld && br == null) {
			rb.AddExplosionForce (10f, fire.transform.position, 2f);
		}




	}
}
