using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject pauseMenuUI;
    private bool isPaused = false;


    private void Start()
    {
        isPaused = false;
        pauseMenuUI.SetActive(false);
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
    }

    void PauseAllScripts()
    {
        isPaused = true;
        Cursor.visible = true;
        pauseMenuUI.SetActive(true);
        PauseScriptsRecursive(transform); // Llama a la función recursiva para pausar todos los scripts
        Time.timeScale = 0f; // Pausa el tiempo en el juego
    }

    void ResumeAllScripts()
    {
        isPaused = false;
        Cursor.visible = false;
        pauseMenuUI.SetActive(false);
        ResumeScriptsRecursive(transform); // Llama a la función recursiva para reanudar todos los scripts
        Time.timeScale = 1f; // Reanuda el tiempo en el juego
    }

    // Función recursiva para pausar todos los scripts de los hijos de un GameObject
    void PauseScriptsRecursive(Transform parent)
    {
        foreach (Transform child in parent)
        {
            // Pausar todos los scripts del GameObject hijo
            MonoBehaviour[] scripts = child.GetComponents<MonoBehaviour>();
            foreach (MonoBehaviour script in scripts)
            {
                if (script != null && script.enabled)
                {
                    script.enabled = false;
                }
            }

            // Llamar recursivamente a la función para los hijos del GameObject hijo
            PauseScriptsRecursive(child);
        }
    }

    // Función recursiva para reanudar todos los scripts de los hijos de un GameObject
    void ResumeScriptsRecursive(Transform parent)
    {
        foreach (Transform child in parent)
        {
            // Reanudar todos los scripts del GameObject hijo
            MonoBehaviour[] scripts = child.GetComponents<MonoBehaviour>();
            foreach (MonoBehaviour script in scripts)
            {
                if (script != null)
                {
                    script.enabled = true;
                }
            }

            // Llamar recursivamente a la función para los hijos del GameObject hijo
            ResumeScriptsRecursive(child);
        }
    }
}
