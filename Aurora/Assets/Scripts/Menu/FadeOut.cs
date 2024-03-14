using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Script que se encarga de hacer un fade de una imagen de transparente a negro y que te carga a otra escena

public class FadeOut : MonoBehaviour
{
    public float durationFade = 2.5f; // Duración del efecto de fade
    public Image imageFade; // Referencia a la imagen que se desvanecerá

    private void Start()
    {
        if (imageFade != null)
            imageFade.color = new Color(imageFade.color.r, imageFade.color.g, imageFade.color.b, 0f);
    }

    public void StartFade(string scene)
    {
        StartCoroutine(FadeEffect(scene));
    }

    protected IEnumerator FadeEffect(string scene)
    {
        float initialTime = Time.time;
        float timeElapsed = 0f;

        while (timeElapsed < durationFade)
        {
            timeElapsed = Time.time - initialTime;
            float porcentajeCompletado = timeElapsed / durationFade;

            // Lerp para suavizar el cambio de opacidad
            float alphaActual = Mathf.Lerp(0f, 1f, porcentajeCompletado);
            imageFade.color = new Color(imageFade.color.r, imageFade.color.g, imageFade.color.b, alphaActual);

            yield return null;
        }
        SceneManager.LoadScene(scene);
    }

}