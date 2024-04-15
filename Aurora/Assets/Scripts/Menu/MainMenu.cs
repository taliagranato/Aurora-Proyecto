using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Script que gestiona el menu principal

public class MainMenu : MonoBehaviour
{
    public GameObject optionsWindow;
    public GameObject controlsWindow;
    // Start is called before the first frame update
    void Start()
    {
        optionsWindow.SetActive(false);
        controlsWindow.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) OptionsOn();

    }

    // Método que hace el fade y carga la escena 01
    public void StartGame()
    {
        FadeOut fadeScript = FindObjectOfType<FadeOut>();
        Debug.Log("Empieza el juego");
        fadeScript.StartFade("Game");
    }
    // Método que hace el fade y carga la escena 01
    public void Menu()
    {
        FadeOut fadeScript = FindObjectOfType<FadeOut>();
        Debug.Log("Volver al menu.");
        fadeScript.StartFade("Title Screen");
    }

    // Método que muestran las opciones
    public void OptionsOn()
    {
        optionsWindow.SetActive(true);
        controlsWindow.SetActive(true);
    }

    // Método que cierra las ventanas 
    public void WindowsOff()
    {
        optionsWindow.SetActive(false);
        controlsWindow.SetActive(false);
    }

    // Método que cierra el juego
    public void QuitGame()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();
    }
}
