using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicOptions : MonoBehaviour
{
    public Options optionsPanel;

    // Start is called before the first frame update
    void Start()
    {
        optionsPanel = GameObject.FindGameObjectWithTag("Options").GetComponent<Options>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ShowOptions();
        }
    }

    public void ShowOptions()
    {
        optionsPanel.optionsScreen.SetActive(true);
    }
}
