using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;

public class GameManager : MonoBehaviour
{
    //Properties...
    public static GameManager singleton;
    public int score;
    public int bestScore;
    private int currentStage = 0;

    //Called before the start method...
    private void Awake()
    {
        Advertisement.Initialize("4382089");
        if (!singleton)
            singleton = this;
        else if (singleton != this)
            Destroy(gameObject);
        bestScore = PlayerPrefs.GetInt("Highscore");
    }

    public void NextLevel()
    {
        Debug.Log("Next level called...");
        currentStage++;
        FindObjectOfType<BallController>().ResetBallPosition();
        if (currentStage > 2)
            currentStage = 0;
        FindObjectOfType<HelixController>().LoadStage(currentStage);
    }

    public void RestartLevel()
    {
        Debug.Log("Restart level called....Show Ads...");
        Advertisement.Show();
        singleton.score = 0;
        FindObjectOfType<BallController>().ResetBallPosition();
        FindObjectOfType<HelixController>().ResetHelixOrientation();
        FindObjectOfType<HelixController>().LoadStage(currentStage);
    }
    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
        if(score > bestScore)
        {
            bestScore = score;

            PlayerPrefs.SetInt("Highscore", score);
        }
    }
}
