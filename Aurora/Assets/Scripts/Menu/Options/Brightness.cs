using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Script del menu de opciones del brillo

public class Brightness : MonoBehaviour
{
    public Slider slider;
    public float clippingValue = 1;
    public Light ligthScene;

    // Start is called before the first frame update
    void Start()
    {
        slider.value = PlayerPrefs.GetFloat("brightness", clippingValue);
        ligthScene.intensity = slider.value;
    }

    public void ChangeValue(float value)
    {
        value = clippingValue;
        PlayerPrefs.SetFloat("brightness", clippingValue);
        ligthScene.intensity = slider.value;
    }
}
