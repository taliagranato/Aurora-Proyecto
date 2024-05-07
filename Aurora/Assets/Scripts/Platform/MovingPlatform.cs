using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script que hace que una plataforma se mueva

public class MovingPlatform : MonoBehaviour
{
    public GameObject plataform;
    public float dist;
    public Vector3 mov;
    private float distActual = 0;
    public float vel = 2;

    public bool needButton;

    void Update()
    {
        if (!needButton)
        {
            MovePlataform();
        }
        else
        {
            if (Button.activePlatform)
                MovePlataform();
        }

    }

    // Hacer que si el jugador está en la plataforma, éste se mueva con ella, haciendolo hijo de ella
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.transform.SetParent(transform);
        }
    }

    // Si el jugador sale de la plataforma, la plataforma deja de ser padre del jugador
    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.transform.SetParent(null);
        }
    }

    public void MovePlataform()
    {
        plataform.transform.position = plataform.transform.position + mov;
        distActual += mov.magnitude;

        if (distActual >= dist)
        {
            mov *= -1;
            distActual = 0;
        }
    }
}
