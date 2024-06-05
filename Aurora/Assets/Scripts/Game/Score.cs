using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public int score;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        UpdateScore();
    }

    public void AddScore(int ammount)
    {
        score += ammount;
        UpdateScore();
    }

    public void RemoveScore(int ammount)
    {
        score -= ammount;
        UpdateScore();
    }

    private void UpdateScore()
    {
        PlayerPrefs.SetInt("score", score);
    }

    public int GetScore()
    {
        return PlayerPrefs.GetInt("score");
    }
}
