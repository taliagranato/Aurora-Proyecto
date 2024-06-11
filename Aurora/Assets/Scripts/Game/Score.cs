using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public int score;
    public int enemiesKilled;
    public TMP_Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        enemiesKilled = 0;
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
        if (score < 0) score = 0;
        UpdateScore();
    }

    private void UpdateScore()
    {
        PlayerPrefs.SetInt("score", score);
        scoreText.text = "Puntos: " + score;
    }
    public void AddEnemyCount()
    {
        enemiesKilled ++;
        Debug.Log(" Enemigos eliminados: " + enemiesKilled);
        PlayerPrefs.SetInt("kills", enemiesKilled);
    }
    public int GetEnemies()
    { 
        return PlayerPrefs.GetInt("kills");
    }

    public int GetScore()
    {
        return PlayerPrefs.GetInt("score");
    }
}
