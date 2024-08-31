using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    Text score;
    Energy energy;
    int scorePoint;
    int targetHits;
    Animator scoreAnim;
    void Awake()
    {
        score = GetComponent<Text>();
        scoreAnim = GetComponent<Animator>();
        energy = FindObjectOfType<Energy>();
       
        scorePoint = 0;
        targetHits = 0;
        score.text = "0";

    }

    public void AddScore(int point)
    {
        scorePoint += point;
        if (scorePoint < 0) { scorePoint = 0; }

        score.text = scorePoint.ToString("0");
        scoreAnim.Play("Score Pop");

        energy.IncreaseDrainSpeed();
    }

    public void AddTargetHits()
    {
        ++targetHits;
    }

    public int GetScorePoint()
    {
        return scorePoint;
    }

    public int GetTargetHits()
    {
        return targetHits;
    }
}
