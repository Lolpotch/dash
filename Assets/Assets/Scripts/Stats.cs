using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour
{
    [SerializeField] Text highScore = null;
    [SerializeField] Text totalDistance = null;
    [SerializeField] Text totalHits = null;
    [SerializeField] Text totalPlays = null;

    int highScoreInt,
        totalDistanceInt,
        totalHitsInt,
        totalPlaysInt;

    void Awake()
    {
        highScoreInt = PlayerPrefs.GetInt("Highscore", 0);
        totalDistanceInt = PlayerPrefs.GetInt("Total Distance", 0);
        totalHitsInt= PlayerPrefs.GetInt("Total Hits", 0);
        totalPlaysInt = PlayerPrefs.GetInt("Total Plays", 0);

        SetDisplayStats();
    }

    void SetDisplayStats()
    {
        highScore.text = highScoreInt.ToString();
        totalDistance.text = totalDistanceInt.ToString();
        totalHits.text = totalHitsInt.ToString();
        totalPlays.text = "You've played this game for " + totalPlaysInt.ToString() + " time(s)!";
    }
}
