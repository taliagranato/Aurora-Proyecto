using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

// Script del menu de opciones del brillo

public class Brightness : MonoBehaviour
{
    public Slider slider;
    public float clippingValue;
    public Light ligthScene;
    public Volume volume;
    public ColorAdjustments colorAdjustments;

    private void Awake()
    {
        volume = GameObject.FindGameObjectWithTag("PostProcess").GetComponent<Volume>();
        volume.profile.TryGet(out colorAdjustments);
    }

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
        colorAdjustments.postExposure.value = slider.value;
        //ligthScene.intensity = slider.value;
    }
}
