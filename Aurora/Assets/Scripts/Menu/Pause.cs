using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public bool isPaused = false;
    public Options optionsPanel;

    private void Start()
    {
        isPaused = false;
        pauseMenuUI.SetActive(false);
        optionsPanel = GameObject.FindGameObjectWithTag("Options").GetComponent<Options>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeAllScripts();
            }
            else
            {
                PauseAllScripts();
            }
        }
        // Mostrar siempre el cursor del ratón mientras el juego esté en pausa
        if (isPaused)
        {
            Cursor.visible = true;
        }
    }

    void PauseAllScripts()
    {
        isPaused = true;
        pauseMenuUI.SetActive(true);
        optionsPanel.optionsScreen.SetActive(true);
        // Busca el objeto de la cámara por su etiqueta y pausa su movimiento si lo encuentra
        GameObject mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        if (mainCamera != null)
        {
            MonoBehaviour cameraMovement = mainCamera.GetComponent<MonoBehaviour>();
            if (cameraMovement != null)
            {
                cameraMovement.enabled = false;
            }
        }
        Time.timeScale = 0f; // Pausa el tiempo en el juego
    }

    void ResumeAllScripts()
    {
        isPaused = false;
        pauseMenuUI.SetActive(false);
        optionsPanel.optionsScreen.SetActive(false);
        // Reanuda el movimiento de la cámara si estaba pausado
        GameObject mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        if (mainCamera != null)
        {
            MonoBehaviour cameraMovement = mainCamera.GetComponent<MonoBehaviour>();
            if (cameraMovement != null)
            {
                cameraMovement.enabled = true;
            }
        }
        Time.timeScale = 1f; // Reanuda el tiempo en el juego
    }

    
}
