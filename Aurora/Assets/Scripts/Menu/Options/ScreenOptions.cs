using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Script del menu de opciones de la pantalla

public class ScreenOptions : MonoBehaviour
{
    public Toggle isChecked; // marca un tic si esta pantalla completa

    public TMP_Dropdown resolutionDropDown;
    Resolution[] resolution;



    // Start is called before the first frame update
    void Start()
    {
        if (Screen.fullScreen)
        {
            isChecked.isOn = true;
        }
        else
        {
            isChecked.isOn = false;
        }

        CheckResolution();
    }

    public void ActiveFullScreen(bool fullScreen)
    {
        Screen.fullScreen = fullScreen; // Poner pantalla completa
    }

    public void CheckResolution()
    {
        resolution = Screen.resolutions;
        resolutionDropDown.ClearOptions();

        List<string> options = new List<string>();
        int actualResolution = 0;

        for (int i = 0; i < resolution.Length; i++)
        {
            string option = resolution[i].width + " x " + resolution[i].height + " @ ";
            options.Add(option);

            if (Screen.fullScreen && resolution[i].width == Screen.currentResolution.width &&
                resolution[i].height == Screen.currentResolution.height)
            {
                actualResolution = i;
            }
        }
        resolutionDropDown.AddOptions(options);
        resolutionDropDown.value = actualResolution;
        resolutionDropDown.RefreshShownValue();

        resolutionDropDown.value = PlayerPrefs.GetInt("numResolution", 0);
    }

    // Método para cambiar la resolucion
    public void ChangeResolution(int indiceResolution)
    {
        // Guardar el valor al volver a abrir el juego
        PlayerPrefs.SetInt("numResolution", resolutionDropDown.value);

        Resolution resolucion = resolution[indiceResolution];
        Screen.SetResolution(resolucion.width, resolucion.height, Screen.fullScreen);
    }
}
