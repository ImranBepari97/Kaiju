using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WristScore : MonoBehaviour {

    [SerializeField]
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
        s += "N/A";
        text.text = s;
    }
}
