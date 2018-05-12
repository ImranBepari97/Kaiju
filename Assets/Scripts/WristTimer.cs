using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WristTimer : MonoBehaviour {
    private Text text;

    // Use this for initialization
    void Start () {
        text = this.GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
        TimeSpan ts = TimeSpan.FromSeconds((float)PointSystem.TimeRemaining);
        text.text = "" + ts.Minutes + ":" + ts.Seconds + ":" + ts.Milliseconds;
	}
}
