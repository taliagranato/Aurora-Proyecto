using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Script del menu de opciones del volumen de musica

public class Volum : MonoBehaviour
{
    public Slider slider;
    public float sliderValue = 1;

    // Start is called before the first frame update
    void Start()
    {
        slider.value = PlayerPrefs.GetFloat("volumAudio", 1f);
        AudioListener.volume = slider.value;
    }

    public void ChangeSlider(float valor)
    {
        valor = sliderValue;
        PlayerPrefs.SetFloat("volumAudio", sliderValue);
        AudioListener.volume = slider.value;
    }
}
