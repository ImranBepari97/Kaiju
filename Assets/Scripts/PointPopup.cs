using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointPopup : MonoBehaviour {

	public TextMesh textMesh;

	// Use this for initialization
	void Start () {
		Destroy (this.gameObject, 2.0f);
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3 (transform.position.x, transform.position.y + (1 * Time.deltaTime), transform.position.z);
		transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward);
	}
}
