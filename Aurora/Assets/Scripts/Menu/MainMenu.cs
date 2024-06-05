using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Script que gestiona el menu principal

public class MainMenu : MonoBehaviour
{
    public GameObject optionsWindow;
    public GameObject controlsWindow;
//public GameObject creditsWindow;
    private bool windowsActive = false; // Variable para controlar el estado de las ventanas


    // Start is called before the first frame update
    void Start()
    {
        optionsWindow.SetActive(false);
        controlsWindow.SetActive(false);
      //  creditsWindow.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Alternar la visibilidad de las ventanas cuando se presiona Escape
            if (windowsActive)
            {
                WindowsOff();
            }
            else
            {
                OptionsOn();
            }
        }

    }

    // M�todo que hace el fade y carga la escena 01
    public void StartGame()
    {
        FadeOut fadeScript = FindObjectOfType<FadeOut>();
        Debug.Log("Empieza el juego");
        fadeScript.StartFade("Game");
    }
    // M�todo que hace el fade y carga la escena 01
    public void Menu()
    {
        FadeOut fadeScript = FindObjectOfType<FadeOut>();
        Debug.Log("Volver al menu.");
        fadeScript.StartFade("Title Screen");
    }

    // M�todo que muestran las opciones
    public void OptionsOn()
    {
        optionsWindow.SetActive(true);
        controlsWindow.SetActive(true);
      //  creditsWindow.SetActive(false);
        windowsActive = true; // Establecer el estado de las ventanas como abierto
    }

  /*  public void CreditsOn()
    {
        creditsWindow.SetActive(true);
    }
    public void CreditsOff()
    {
        creditsWindow.SetActive(false);
    }*/

    // M�todo que cierra las ventanas 
    public void WindowsOff()
    {
        optionsWindow.SetActive(false);
        controlsWindow.SetActive(false);
       // creditsWindow.SetActive(false);
        windowsActive = false; // Establecer el estado de las ventanas como cerrado
    }

    // M�todo que cierra el juego
    public void QuitGame()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();
    }
}
