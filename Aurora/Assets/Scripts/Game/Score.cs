using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public int score;
    public int enemysDead;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        enemysDead = 0;
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
    public void AddEnemyCount()
    {
        enemysDead ++;
        Debug.Log(" Enemigos eliminados: " + enemysDead);
        PlayerPrefs.SetInt("enemys", enemysDead);
    }
    public int GetEnemys()
    { 
        return PlayerPrefs.GetInt("enemys");
    }

    public int GetScore()
    {
        return PlayerPrefs.GetInt("score");
    }
}
