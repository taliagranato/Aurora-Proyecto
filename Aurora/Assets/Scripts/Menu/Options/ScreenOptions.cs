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

        RevisarResolucion();
    }

    public void ActivarPantallaCompleta(bool pantCompleta)
    {
        Screen.fullScreen = pantCompleta; // Poner pantalla completa
    }

    public void RevisarResolucion()
    {
        resolution = Screen.resolutions;
        resolutionDropDown.ClearOptions();

        List<string> opciones = new List<string>();
        int resolucionActual = 0;

        for (int i = 0; i < resolution.Length; i++)
        {
            string opcion = resolution[i].width + " x " + resolution[i].height + " @ " + resolution[i].refreshRate + "hz";
            opciones.Add(opcion);

            if (Screen.fullScreen && resolution[i].width == Screen.currentResolution.width &&
                resolution[i].height == Screen.currentResolution.height)
            {
                resolucionActual = i;
            }
        }
        resolutionDropDown.AddOptions(opciones);
        resolutionDropDown.value = resolucionActual;
        resolutionDropDown.RefreshShownValue();

        resolutionDropDown.value = PlayerPrefs.GetInt("numeroResolucion", 0);
    }

    // Método para cambiar la resolucion
    public void CambiarResolucion(int indiceResolucion)
    {
        // Guardar el valor al volver a abrir el juego
        PlayerPrefs.SetInt("numeroResolucion", resolutionDropDown.value);

        Resolution resolucion = resolution[indiceResolucion];
        Screen.SetResolution(resolucion.width, resolucion.height, Screen.fullScreen);
    }
}
