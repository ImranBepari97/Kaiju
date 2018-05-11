using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PointSystem : MonoBehaviour {

    public static readonly float MINUTES_IN_GAME = 5.0f;
	public static int totalPoints = 0;
    private static int finalScore;
    public static int highscore { get; private set; }
    public static float TimeRemaining { get; private set; }
    public static bool IsRunning { get; private set; }

	public TextMesh pointDisplay;

	// Use this for initialization
	void Start () {
        highscore = PlayerPrefs.GetInt("highscore", 0);
        Debug.Log("Highscore set to: " + highscore + (highscore == 0 ? " (may be first game)" : ""));
        StartGame();
	}

    void StartGame()
    {
        totalPoints = 0;
        TimeRemaining = (float) MINUTES_IN_GAME * 60f;
        IsRunning = true;
    }
	
	// Update is called once per frame
	void Update () {
		if (pointDisplay != null) {
			pointDisplay.text = "Points: " + totalPoints;
		}

        if (IsRunning)
        {
            TimeRemaining -= Time.deltaTime;
            if (TimeRemaining <= 0)
            {
                EndGame();
            }
        }
	}

    void EndGame()
    {

        IsRunning = false; //Stop game
        finalScore = totalPoints; //Copy score
        if (finalScore > highscore)
        {
            highscore = finalScore;
            PlayerPrefs.SetInt("lastscore", finalScore);
            PlayerPrefs.SetInt("highscore", highscore);
        }
        SceneManager.LoadScene("GameOver");
    }

}
