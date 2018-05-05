using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Circular Array manager for debris
public class DebrisManager : MonoBehaviour {
	static GameObject[] debrisList = new GameObject[1500];
	static int currentIndex = 0;

	public int collateralLevel = 0;
	public bool beingHeld;

	// Use this for initialization
	void Start () {
		beingHeld = false;
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

	//Adds current piece of debris to the list, makes sure no overcrowding
	void AddDebris() {
		if(debrisList[currentIndex] != null) {
			if (debrisList [currentIndex].GetComponent<DebrisManager> ().beingHeld) {
				//If the piece to be deleted is held by the player, just delete the next one
				currentIndex++;
				currentIndex %= debrisList.Length;
			} else {
				Destroy (debrisList [currentIndex]);
			}
		}

			
		debrisList [currentIndex] = this.gameObject;

		//Get the next piece of debris, used a prime number because seemed right
		//Jumps in multiples so it's not literally the oldest debris being deleted
		//Otherwise, seems like the debris is hoovering itself up
		currentIndex += 17;
		currentIndex %= debrisList.Length;
		if(currentIndex < 17) {
			currentIndex = Random.Range (0,17);
		}

	}
}
