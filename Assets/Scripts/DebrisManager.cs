using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Circular Array manager for debris
public class DebrisManager : MonoBehaviour {

	static GameObject[] debrisList = new GameObject[1000];
	static int currentIndex = 0;

	// Use this for initialization
	void Start () {
		AddDebris ();	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void AddDebris() {

		if(debrisList[currentIndex] != null) {
			Destroy (debrisList[currentIndex]);
		}

		debrisList [currentIndex] = this.gameObject;

		currentIndex++;
		currentIndex %= debrisList.Length;
	}
}
