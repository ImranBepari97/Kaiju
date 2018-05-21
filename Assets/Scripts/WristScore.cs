using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WristScore : MonoBehaviour {
    private Text text;

	void Awake () {
        text = this.GetComponent<Text>();
	}

    void OnGUI()
    {
        string s = "";
        s += "<b>Score</b>\n";
        s += PointSystem.totalPoints + "\n";
        s += "<b>Highscore</b>\n";
        s += PointSystem.Highscore;
        text.text = s;
    }
}
