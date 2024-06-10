using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndText : MonoBehaviour
{ 
    public TMP_Text scoreText;
    public TMP_Text killsText;

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "" + PlayerPrefs.GetInt("score");
        killsText.text = "" + PlayerPrefs.GetInt("kills");
    }
}
