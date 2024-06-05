using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;

public class Enemy : Character
{
    public float chaseRange = 10f; // Rango de distancia para comenzar a perseguir al jugador
    public float updateDestinationInterval = 3f; // Intervalo para actualizar el destino del enemigo
    private NavMeshAgent agent; // Componente NavMeshAgent para mover al enemigo
    private Pause pauseScript;
    public GameObject player; // Referencia al transform del jugador
    private GameObject sprite_gameobject;
    private float timeSinceLastUpdate = 0f; // Variable para rastrear el tiempo desde la última actualización
    public Animator animator;
    public event Action OnDeath;// Declaración del evento OnDeath

    // Disparo
    public GameObject bulletPrefab;
    public Transform[] firePoints; // Array de puntos de disparo
    private bool canShoot = true;
    // Valores para definir un rango de tiempo aleatorio entre disparos
    public float randomFireRateMax = 1.0f;


    protected override void Start()
    {
        base.Start();
        StartCoroutine(ShootRoutine());
        pauseScript = GameObject.FindObjectOfType<Pause>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("sprite");
     
        // Seleccionar un sprite aleatorio de la lista y asignarlo al SpriteRenderer
        sprite_gameobject = this.transform.GetChild(0).gameObject;
        SpriteRenderer spriteRenderer = this.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        animator.SetBool("Death", false);

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
            if (distJugador.magnitude <= chaseRange) // Si está el jugador a rango del enemigo
            {
                if (Physics.Raycast(this.transform.position, distJugador, out resultadoRay))
                {
                    Debug.Log("Objeto colisionado: " + resultadoRay.transform.name); // Imprime la etiqueta del objeto colisionado
                    // Rayo colisiona con algo
                    if (resultadoRay.transform.tag == "Player") // Que tiene línea de visión
                    {
                        Debug.Log("Persiguiendo al jugador");
                        agent.SetDestination(player.transform.position); // Seguir al jugador
                        return; // Salir del método para evitar la actualización adicional del destino
                    }
                }
            }

            // Verificar si ha pasado suficiente tiempo desde la última actualización del destino
            if (Time.time - timeSinceLastUpdate >= updateDestinationInterval)
            {
                UpdateDestination();
                timeSinceLastUpdate = Time.time; // Actualizar el tiempo de la última actualización
            }
        }
        
    }

    // Actualiza el destino del enemigo
    private void UpdateDestination()
    {
        Vector3 randomPosition = GetRandomNavMeshPosition();
        agent.SetDestination(randomPosition);
    }
    protected override void Death()
    {
        animator.SetBool("Death", true);
        base.Death(); // Llamar al método Death de la clase base
        OnDeath?.Invoke();

        Destroy(this.gameObject, 1f); // Invoca el evento OnDeath para notificar que el enemigo ha muerto  
    }

    // Obtener un punto aleatorio dentro de la NavMesh
    private Vector3 GetRandomNavMeshPosition()
    {
        NavMeshHit hit;
        Vector3 randomPosition = transform.position + UnityEngine.Random.insideUnitSphere * 10f;
        NavMesh.SamplePosition(randomPosition, out hit, 10f, NavMesh.AllAreas); // Ajusta el radio según el tamaño de tu NavMesh
        return hit.position;
    }


    IEnumerator ShootRoutine()
    {
        while (true)
        {
            // Obtener un tiempo aleatorio entre disparos dentro del rango definido
            float randomFireRate = UnityEngine.Random.Range(randomFireRateMax, fire_rate);
            yield return new WaitForSeconds(randomFireRate);

            if (canShoot)
            {
                Shoot();
                canShoot = false;
                StartCoroutine(ResetShoot());
            }
        }
    }
    void Shoot()
    {
        foreach (Transform firePoint in firePoints)
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        }
    }

    IEnumerator ResetShoot()
    {
        // Esperar hasta que pueda disparar de nuevo según el tiempo aleatorio entre disparos
        float randomFireRate = UnityEngine.Random.Range(randomFireRateMax, fire_rate);
        yield return new WaitForSeconds(randomFireRate);

        canShoot = true;
    }

    public void Damage(int damage)
    {
        hp -= damage;
        this.IsDead();
    }
}


