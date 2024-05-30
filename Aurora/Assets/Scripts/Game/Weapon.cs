using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{

    // Line Renderer para la predicci�n de la trayectoria
    private LineRenderer lineRenderer;
    public int lineSegmentCount = 20; // N�mero de segmentos en la l�nea de predicci�n
    public float predictionTime = 2f; // Tiempo de predicci�n de la trayectoria

    private void Awake()
    {
        // Configurar Line Renderer
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = lineSegmentCount;
    }
    private void Update()
    {
        // Calcular y dibujar la trayectoria de la bala solo si el bot�n derecho del rat�n est� presionado
        if (Input.GetKey(KeyCode.Mouse1))
        {
            CalculateBulletTrajectory();
            lineRenderer.enabled = true; // Habilitar LineRenderer cuando se dibuje la trayectoria
        }
        else
        {
            lineRenderer.enabled = false; // Deshabilitar LineRenderer cuando no se dibuje la trayectoria
        }
    }

    // M�todo para calcular y dibujar la trayectoria de la bala
    private void CalculateBulletTrajectory()
    {
        Vector3 startPosition = this.transform.position;
        Vector3 startVelocity = this.transform.forward * 11.5f; // Ajustar la velocidad inicial seg�n sea necesario
        float timeStep = predictionTime / lineSegmentCount;
        Vector3 gravity = Physics.gravity;

        lineRenderer.positionCount = lineSegmentCount;

        for (int i = 0; i < lineSegmentCount; i++)
        {
            float t = i * timeStep;
            Vector3 position = startPosition + startVelocity * t + 0.5f * gravity * t * t;
            lineRenderer.SetPosition(i, position);
        }
    }
}
