using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightMovwement : MonoBehaviour
{
    public Light directionalLight;
    public Color coldColor = new Color(0.5f, 0.7f, 1f);
    public Color warmColor = new Color(1f, 0.7f, 0.5f);
    public float lightTransitionDuration = 10f;

    public Vector3 startPosition = new Vector3(1020f, 793f, 368f);
    public Vector3 finalPosition = new Vector3(1020f, 643f, 767f);
    public float startRotationX = 70f;
    public float finalRotationX = 145f;
    public float startIntensity = 0.8f;
    public float finalIntensity = 1.1f;

    void Start()
    {
        StartCoroutine(AnimateLight());
    }

    IEnumerator AnimateLight()
    {
        Color startColor = coldColor;
        float startIntensityValue = startIntensity;

        // Establecer la posición, rotación e intensidad inicial
        directionalLight.transform.position = startPosition;
        directionalLight.transform.rotation = Quaternion.Euler(startRotationX, 0f, 0f);
        directionalLight.intensity = startIntensityValue;

        float elapsedTime = 0f;

        while (elapsedTime < lightTransitionDuration)
        {
            // Animar color de la luz
            directionalLight.color = Color.Lerp(startColor, warmColor, elapsedTime / lightTransitionDuration);

            // Animar posición, rotación e intensidad de la luz
            Vector3 currentPosition = Vector3.Lerp(startPosition, finalPosition, elapsedTime / lightTransitionDuration);
            float currentRotationX = Mathf.Lerp(startRotationX, finalRotationX, elapsedTime / lightTransitionDuration);
            float currentIntensity = Mathf.Lerp(startIntensityValue, finalIntensity, elapsedTime / lightTransitionDuration);

            directionalLight.transform.position = currentPosition;
            directionalLight.transform.rotation = Quaternion.Euler(currentRotationX, 0f, 0f);
            directionalLight.intensity = currentIntensity;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Asegurarse de que la luz alcance exactamente los valores finales al final de la animación
        directionalLight.color = warmColor;
        directionalLight.transform.position = finalPosition;
        directionalLight.transform.rotation = Quaternion.Euler(finalRotationX, 0f, 0f);
        directionalLight.intensity = finalIntensity;
    }
}
