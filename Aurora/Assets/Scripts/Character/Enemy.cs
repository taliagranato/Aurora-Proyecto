using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character
{
    public Vector3 dir;
    public float floatSpeed = 1f; // Velocidad de flotaci�n del enemigo
    public float chaseRange = 10f; // Rango de distancia para comenzar a perseguir al jugador
    public float updateDestinationInterval = 3f; // Intervalo para actualizar el destino del enemigo
    private NavMeshAgent agent; // Componente NavMeshAgent para mover al enemigo
    private Pause pauseScript;
    public GameObject player; // Referencia al transform del jugador
    private bool isChasing = false; // Indica si el enemigo est� persiguiendo al jugador
    private float timeSinceLastUpdate = 0f; // Variable para rastrear el tiempo desde la �ltima actualizaci�n

    public List<Sprite> enemySprites; // Lista de sprites disponibles para los enemigos

    public event Action OnDeath;// Declaraci�n del evento OnDeath

    protected override void Start()
    {
        base.Start();
        pauseScript = GameObject.FindObjectOfType<Pause>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("sprite");
        // Seleccionar un sprite aleatorio de la lista y asignarlo al SpriteRenderer
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null && enemySprites.Count > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, enemySprites.Count);
            spriteRenderer.sprite = enemySprites[randomIndex];
        }

        InvokeRepeating("UpdateDestination", 0f, updateDestinationInterval); // Actualiza el destino cada cierto intervalo de tiempo
    }

    private void Update()
    {
        if (!pauseScript.isPaused)
        {
            // Vector de distancia entre el jugador y el enemigo
            Vector3 distJugador = player.transform.position - this.transform.position;

            RaycastHit resultadoRay;
            Debug.DrawRay(transform.position, distJugador, Color.red);
            if (distJugador.magnitude <= chaseRange) // Si est� el jugador a rango del enemigo
            {
                if (Physics.Raycast(this.transform.position, distJugador, out resultadoRay))
                {
                    Debug.Log("Objeto colisionado: " + resultadoRay.transform.name); // Imprime la etiqueta del objeto colisionado
                    // Rayo colisiona con algo
                    if (resultadoRay.transform.tag == "Player") // Que tiene l�nea de visi�n
                    {
                        Debug.Log("Persiguiendo al jugador");
                        agent.SetDestination(player.transform.position); // Seguir al jugador
                        return; // Salir del m�todo para evitar la actualizaci�n adicional del destino
                    }
                }
            }

            // Verificar si ha pasado suficiente tiempo desde la �ltima actualizaci�n del destino
            if (Time.time - timeSinceLastUpdate >= updateDestinationInterval)
            {
                UpdateDestination();
                timeSinceLastUpdate = Time.time; // Actualizar el tiempo de la �ltima actualizaci�n
            }
        }
        this.IsDead();
    }

    // Actualiza el destino del enemigo
    private void UpdateDestination()
    {
        Vector3 randomPosition = GetRandomNavMeshPosition();
        agent.SetDestination(randomPosition);
        //Float();

    }
    protected override void Death()
    {
        base.Death(); // Llamar al m�todo Death de la clase base
        OnDeath?.Invoke();
        Destroy(this.gameObject, 0.1f); // Invoca el evento OnDeath para notificar que el enemigo ha muerto  
    }

    // Obtener un punto aleatorio dentro de la NavMesh
    private Vector3 GetRandomNavMeshPosition()
    {
        NavMeshHit hit;
        Vector3 randomPosition = transform.position + UnityEngine.Random.insideUnitSphere * 10f;
        NavMesh.SamplePosition(randomPosition, out hit, 10f, NavMesh.AllAreas); // Ajusta el radio seg�n el tama�o de tu NavMesh
        return hit.position;
    }

    // Simular el efecto de flotaci�n
    /* private void Float()
     {
         // Aplica un movimiento sinusoidal en la posici�n vertical
         float yOffset = Mathf.Sin(Time.time * floatSpeed) * 0.2f; // La amplitud determina la altura de la flotaci�n
         transform.position += Vector3.up * yOffset * Time.deltaTime;
     }*/


    public void Damage(int damage)
    {
        hp -= damage;
    }
}


