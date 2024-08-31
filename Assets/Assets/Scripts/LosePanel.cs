using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LosePanel : MonoBehaviour
{
    Score score;
    SpawnManager spawnManager;
    InitializeAd adManager;
    int loseAmount;

    [Header("Score Multiplier")]
    [SerializeField] float distanceMultiplier = 0f;
    [SerializeField] float targetHitsMultiplier = 0f;

    [Header("Lose Panel Components")]
    [SerializeField] Text scoreText = null;
    [SerializeField] Text highscoreText = null;
    [SerializeField] Text distance = null;
    [SerializeField] Text targetHits = null;

    [Header("Gameplay UI I want to disable after lose")]
    [SerializeField] GameObject[] gameplayUI = null;


    void Awake()
    {
        loseAmount = PlayerPrefs.GetInt("Lose Amount", 0);
        PlayerPrefs.SetInt("Lose Amount", ++loseAmount);

        score = FindObjectOfType<Score>();
        spawnManager = FindObjectOfType<SpawnManager>();
        adManager = FindObjectOfType<InitializeAd>();
    }

    void Start()
    {
        DisableUI();
        StartCoroutine(StartAd());
        SetScore();
    }

    void DisableUI()
    {
        foreach(GameObject obj in gameplayUI)
        {
            obj.SetActive(false);
        }
    }

    void SetScore()
    {
        //Extra score
        int distanceAmount = spawnManager.GetTravelDistance();
        int targetHitAmount = score.GetTargetHits();
        distance.text = "Distance: +" + distanceAmount.ToString("0");
        targetHits.text = "Target Hits: +" + targetHitAmount.ToString("0");

        //Adding stats
        PlayerPrefs.SetInt("Total Distance", PlayerPrefs.GetInt("Total Distance", 0) + distanceAmount);
        PlayerPrefs.SetInt("Total Hits", PlayerPrefs.GetInt("Total Hits", 0) + targetHitAmount);
        PlayerPrefs.SetInt("Total Plays", PlayerPrefs.GetInt("Total Plays", 0) + 1);


        int highScorePoint = PlayerPrefs.GetInt("Highscore", 0);
        int scorePoint = (int)(score.GetScorePoint() + (targetHitAmount * targetHitsMultiplier) + (distanceAmount * distanceMultiplier));
        print("Distance Score: " + distanceAmount * distanceMultiplier + "      TargetHits Score: " + targetHitAmount * targetHitsMultiplier);

        scoreText.text = scorePoint.ToString("0");

        if (scorePoint > highScorePoint)
        {
            highscoreText.text = scorePoint.ToString("0");

            PlayerPrefs.SetInt("Highscore", scorePoint);
        }
        else
        {
            highscoreText.text = highScorePoint.ToString("0");
        }
    }

    IEnumerator StartAd()
    {
        yield return new WaitForSeconds(1f);

        if (loseAmount % 3 == 0)
        {
            adManager.ShowSkippableAd();
        }
    }
}
