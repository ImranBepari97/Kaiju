using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointSystem : MonoBehaviour {

	public static int totalPoints = 0;

	public TextMesh pointDisplay;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (pointDisplay != null) {
			pointDisplay.text = "Points: " + totalPoints;
		}
	}
}
