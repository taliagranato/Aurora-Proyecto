using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Character : MonoBehaviour
{
    public int hp;
    public int hp_max;
    public float fire_rate;
    protected float firing;
    public GameObject controller;
    public Score score;

    // Player
    public bool playerBool;
    protected Vector3 initialPos;// variable que guarda la posición inicial del jugador
    public int scoreAmmount;
    public float invulnerabilityTime = 1f; // Duración de la invulnerabilidad en segundos
    private bool isInvulnerable = false; // Indica si el jugador es invulnerable en el momento

    // Regeneración de la salud
    public float regenerationDelay = 20f; // Tiempo de espera para iniciar la regeneración
    public float regenerationRate = 5f; // Cantidad de salud a regenerar por segundo
    private Coroutine regenerationCoroutine; // Referencia a la corrutina de regeneración

    protected virtual void Start()
    {
        controller = GameObject.Find("Controller");
        score = controller.GetComponent<Score>();
        // Guardar la posición inicial del jugador al inicio del juego
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        initialPos = player.transform.position;
        // Ajustar la posición inicial según el tamaño del collider del jugador
        Collider playerCollider = player.GetComponent<Collider>();
        hp = hp_max;
    }
    
    protected void LateUpdate()
    {
        if (firing < fire_rate)
        {
            firing += Time.deltaTime;
        }
    }


    public void IsDead()
    {
        if (this.hp <= 0)
        {
            if (this.playerBool)
            {
                Death();
                Player _player = GetComponentInChildren<Player>();

                StartCoroutine(_player.Respawn(50));
                _player.hp = hp_max / 2;
            }
            else
            {
                Death();
            }
        }
    }

    public bool CanFire()
    {
        if(firing >= fire_rate)
            return true;
        else return false;
    }

    public void TakeDamage(int damage)
    {
        if (!isInvulnerable) // Solo aplica daño si el jugador no es invulnerable
        {
            hp -= damage;
            IsDead();
            StartCoroutine(InvulnerabilityRoutine());
            

            if (regenerationCoroutine != null) // Detener la regeneración si se recibe daño
            {
                StopCoroutine(regenerationCoroutine);
                regenerationCoroutine = null;
            }
        }
    }

    protected virtual void Death()
    {
        if (this.gameObject.CompareTag("Player"))
        { 
            
        }
        else
        {
            score.AddScore(scoreAmmount);
            Debug.Log("Enemy dies. Score: " + scoreAmmount + ". Total Score: " + score.GetScore());
            score.AddEnemyCount();
        }
    }
    
    // Triggers and collisions
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (playerBool)
        {

            if (other.tag == "Damage")
            {
                TakeDamage(10); // Aplica daño y activa la invulnerabilidad
                Debug.Log("Hp: " + hp);
            }
            if (other.tag == "Enemy")
            {
                TakeDamage(15); // Aplica daño y activa la invulnerabilidad
                Debug.Log("Hp: " + hp);
            }
        }

    }


    IEnumerator InvulnerabilityRoutine()
    {
        isInvulnerable = true;
        Player _player = GetComponentInChildren<Player>();
        _player.EffectCorroutine();
        yield return new WaitForSeconds(invulnerabilityTime);
        isInvulnerable = false;

        // Iniciar regeneración si no está ya activa
        if (regenerationCoroutine == null)
        {
            regenerationCoroutine = StartCoroutine(RegenerationRoutine());
        }
    }
    IEnumerator RegenerationRoutine()
    {
        Debug.Log("Health regeneration started");
        yield return new WaitForSeconds(regenerationDelay);

        // Regenerar gradualmente la salud hasta alcanzar la salud máxima
        while (hp < hp_max)
        {
            hp = Mathf.Min(hp_max, hp + 1); // Añadir 1 unidad a la vida
            Debug.Log("Current health: " + hp);
            yield return new WaitForSeconds(0.25f); // Esperar 0.25 segundos
            
        }

        // Restablecer la corrutina de regeneración
        regenerationCoroutine = null;
       // Asegurar que la vida alcanza el máximo al final de la regeneración
        hp = hp_max;
        Debug.Log("Health regeneration completed");
    }
}
