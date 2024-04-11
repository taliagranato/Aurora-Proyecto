using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : Character
{
    public Vector3 dir;
    public float movementSpeed = 2f; // Velocidad de movimiento del enemigo
    public float floatSpeed = 1f; // Velocidad de flotaci�n del enemigo

    private Pause pauseScript;
    // Start is called before the first frame update
    void Start()
    {
        pauseScript = GameObject.FindObjectOfType<Pause>();
        InvokeRepeating("ChangeDirection", 0f, 3f); // Cambia la direcci�n cada 3 segundos

    }

    // Update is called once per frame
    void Update()
    {
        if (!pauseScript.isPaused)
        {
            Move();
            Float();
        }
    }
    // Cambiar la direcci�n del movimiento aleatoriamente
    private void ChangeDirection()
    {
        dir = Random.insideUnitSphere.normalized;
    }

    // Mover el enemigo en la direcci�n aleatoria
    private void Move()
    {
        transform.Translate(dir * movementSpeed * Time.deltaTime);
    }

    // Simular el efecto de flotaci�n
    private void Float()
    {
        // Aplica un movimiento sinusoidal en la posici�n vertical
        float yOffset = Mathf.Sin(Time.time * floatSpeed) * 0.2f; // La amplitud determina la altura de la flotaci�n
        transform.position += Vector3.up * yOffset * Time.deltaTime;
    }

    public void Damage(int damage)
    {
        hp -= damage;
    }
}
