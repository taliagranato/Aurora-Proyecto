using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Script que gestiona el menu principal

public class MainMenu : MonoBehaviour
{
    public GameObject controlsWindow;
    public GameObject optionsWindow;

    // Start is called before the first frame update
    void Start()
    {
        controlsWindow.SetActive(false);
        optionsWindow.SetActive(false); 
    }

    // Método que hace el fade y carga la escena 01
    public void StartGame()
    {
        FadeOut fadeScript = FindObjectOfType<FadeOut>();
        Debug.Log("Empieza el juego");
        fadeScript.StartFade("Game");
    }

    // Método que muestra los controles
    public void ControlsOn()
    {
        controlsWindow.SetActive(true);
    }
    
    // Método que muestran las opciones
    public void OptionsOn()
    {
        optionsWindow.SetActive(true);
    }

    // Método que cierra las ventanas 
    public void WindowsOff()
    {
        controlsWindow.SetActive(false);
        optionsWindow.SetActive(false);
    }

    // Método que cierra el juego
    public void QuitGame()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();
    }
}
