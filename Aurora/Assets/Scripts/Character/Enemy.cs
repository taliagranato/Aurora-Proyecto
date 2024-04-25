using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character
{
    public Vector3 dir;
    public float floatSpeed = 1f; // Velocidad de flotación del enemigo
    public float chaseRange = 10f; // Rango de distancia para comenzar a perseguir al jugador
    public float updateDestinationInterval = 3f; // Intervalo para actualizar el destino del enemigo
    private NavMeshAgent agent; // Componente NavMeshAgent para mover al enemigo
    private Pause pauseScript;
    public GameObject player; // Referencia al transform del jugador
   // private bool isChasing = false; // Indica si el enemigo está persiguiendo al jugador

    protected override void Start()
    {
        base.Start();
        pauseScript = GameObject.FindObjectOfType<Pause>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("PlayerCapsule");
        InvokeRepeating("UpdateDestination", 0f, updateDestinationInterval); // Actualiza el destino cada cierto intervalo de tiempo
    }

    private void Update()
    {
        if (!pauseScript.isPaused)
        {
            // Vector de distanci entre el jugador y el enemigo
            Vector3 distJugador = player.transform.position - this.transform.position;

            RaycastHit resultadoRay;
            Debug.DrawRay(transform.position, distJugador, Color.red);
            if (distJugador.magnitude <= chaseRange) // Si está el jugador a rango del enemigo
            {
                if (Physics.Raycast(this.transform.position, distJugador, out resultadoRay))
                {
                    // Rayo colisiona con algo
                    if (resultadoRay.transform.tag == "Player") // Que tiene linea de visión
                    {
                        Debug.Log("Persiguiendo al jugador");
                        agent.SetDestination(player.transform.position); // Seguir al jugador

                    }
                    else
                    {
                        UpdateDestination();
                    }
                }
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

    // Obtener un punto aleatorio dentro de la NavMesh
    private Vector3 GetRandomNavMeshPosition()
    {
        NavMeshHit hit;
        Vector3 randomPosition = transform.position + Random.insideUnitSphere * 10f; // Genera un punto aleatorio cerca del enemigo
        NavMesh.SamplePosition(randomPosition, out hit, 10f, NavMesh.AllAreas); // Ajusta el radio según el tamaño de tu NavMesh
        return hit.position;
    }

    // Simular el efecto de flotación
    /* private void Float()
     {
         // Aplica un movimiento sinusoidal en la posición vertical
         float yOffset = Mathf.Sin(Time.time * floatSpeed) * 0.2f; // La amplitud determina la altura de la flotación
         transform.position += Vector3.up * yOffset * Time.deltaTime;
     }*/


    public void Damage(int damage)
    {
        hp -= damage;
    }

    /*
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "bullet")
        {
            Debug.Log("Enemy Damaged");
        }
    }*/

}
/*
    public Vector3 dir;
    public float floatSpeed = 1f; // Velocidad de flotación del enemigo
    public float chaseRange = 10f; // Rango de distancia para comenzar a perseguir al jugador
    public float updateDestinationInterval = 3f; // Intervalo para actualizar el destino del enemigo
    public GameObject[] normalSprites; // Sprites normales del enemigo
    public GameObject[] chaseSprites; // Sprites del enemigo cuando persigue al jugador
    private NavMeshAgent agent; // Componente NavMeshAgent para mover al enemigo
    private Pause pauseScript;
    public GameObject player; // Referencia al transform del jugador
    private bool isChasing = false; // Indica si el enemigo está persiguiendo al jugador

    protected override void Start()
    {
        pauseScript = GameObject.FindObjectOfType<Pause>();
        agent = GetComponent<NavMeshAgent>();

        InvokeRepeating("UpdateDestination", 0f, updateDestinationInterval); // Actualiza el destino cada cierto intervalo de tiempo
    }

    private void Update()
    {
        if (!pauseScript.isPaused)
        {
            // Vector de distancia entre el jugador y el enemigo
            Vector3 distJugador = player.transform.position - transform.position;

            RaycastHit resultadoRay;
            Debug.DrawRay(transform.position, distJugador, Color.red);
            if (distJugador.magnitude <= chaseRange) // Si está el jugador a rango del enemigo
            {
                if (Physics.Raycast(transform.position, distJugador, out resultadoRay))
                {
                    // Rayo colisiona con algo
                    if (resultadoRay.transform.tag == "Player") // Que tiene línea de visión
                    {
                        if (!isChasing)
                        {
                            StartChasing(); // Comienza a perseguir al jugador
                        }
                        agent.SetDestination(player.transform.position); // Seguir al jugador
                    }
                    else
                    {
                        if (isChasing)
                        {
                            StopChasing(); // Deja de perseguir al jugador
                        }
                        UpdateDestination();
                    }
                }
            }
            else
            {
                if (isChasing)
                {
                    StopChasing(); // Deja de perseguir al jugador
                }
                UpdateDestination();
            }
        }
    }

    // Método para comenzar a perseguir al jugador
    private void StartChasing()
    {
        isChasing = true;
        // Aquí puedes agregar código adicional que desees ejecutar al comenzar a perseguir
    }

    // Método para dejar de perseguir al jugador
    private void StopChasing()
    {
        isChasing = false;
        // Aquí puedes agregar código adicional que desees ejecutar al dejar de perseguir
    }

    // Actualiza el destino del enemigo
    private void UpdateDestination()
    {
        Vector3 randomPosition = GetRandomNavMeshPosition();
        agent.SetDestination(randomPosition);
        //Float();
    }

    // Obtener un punto aleatorio dentro de la NavMesh
    private Vector3 GetRandomNavMeshPosition()
    {
        NavMeshHit hit;
        Vector3 randomPosition = transform.position + Random.insideUnitSphere * 10f; // Genera un punto aleatorio cerca del enemigo
        NavMesh.SamplePosition(randomPosition, out hit, 10f, NavMesh.AllAreas); // Ajusta el radio según el tamaño de tu NavMesh
        return hit.position;
    }

    // Simular el efecto de flotación
    /* private void Float()
     {
         // Aplica un movimiento sinusoidal en la posición vertical
         float yOffset = Mathf.Sin(Time.time * floatSpeed) * 0.2f; // La amplitud determina la altura de la flotación
         transform.position += Vector3.up * yOffset * Time.deltaTime;
     }

public void Damage(int damage)
    {
        hp -= damage;
    }
}*/

