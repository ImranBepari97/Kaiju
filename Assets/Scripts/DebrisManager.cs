using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Circular Array manager for debris
public class DebrisManager : MonoBehaviour {
	static GameObject[] debrisList = new GameObject[1500];
	static int currentIndex = 0;

	public int collateralLevel = 0;

	// Use this for initialization
	void Start () {
		AddDebris ();	

		if(transform.parent != null) {
			Vector3 savedScale = transform.parent.localScale;
			transform.parent = null;
			transform.localScale = new Vector3 (
				savedScale.x * transform.localScale.x,
				savedScale.y * transform.localScale.y,
				savedScale.z * transform.localScale.z);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void AddDebris() {

		if(debrisList[currentIndex] != null ) {
			Destroy (debrisList[currentIndex]);
		}

		debrisList [currentIndex] = this.gameObject;

		currentIndex += 17;
		currentIndex %= debrisList.Length;

		if(currentIndex < 17) {
			currentIndex = Random.Range (0,17);
		}
	}
}
