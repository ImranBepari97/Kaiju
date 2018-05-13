using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {

	// Use this for initialization
	void Start () {
        int finalScore = PlayerPrefs.GetInt("lastscore", 0);
        int highscore = PlayerPrefs.GetInt("highscore", 0);

        string s = "";
        s += "<b>Score</b>\n";
        s += finalScore + (highscore == finalScore ? " (<b><i>New Highscore!</i></b>)" : "") + "\n\n";
        s += "<b>Highscore</b>\n";
        s += highscore;

		if (GetComponentInChildren<Text> ()) {
			GetComponentInChildren<Text> ().text = s;
		}
    }
	
	public void Restart()
    {
        SceneManager.LoadScene("main2");
    }
}
