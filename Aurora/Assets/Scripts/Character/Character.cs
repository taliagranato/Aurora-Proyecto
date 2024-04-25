using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public int hp;
    public int hp_max;
    public float fire_rate;
    public float firing;
    public bool playerBool;
    public Vector3 initialPos;// variable que guarda la posición inicial del jugador

    protected virtual void Start()
    {
        // Guardar la posición inicial del jugador al inicio del juego
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        initialPos = player.transform.position;
        // Ajustar la posición inicial según el tamaño del collider del jugador
        Collider playerCollider = player.GetComponent<Collider>();
        hp = hp_max;
        if (playerCollider != null)
        {
            initialPos.y += playerCollider.bounds.extents.y; // Ajustar según el tamaño del collider
        }
    }
    
    protected void LateUpdate()
    {
        if (firing < fire_rate)
        {
            firing += Time.deltaTime;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void IsDead()
    {
        if (this.hp <= 0)
        {
            Debug.Log("a");
            if (this.playerBool)
            {
                Death();
            }
            else
            {
                Debug.Log("Enemy dies");
                Destroy(this.gameObject, 0.1f);
            }

        }
    }

    public bool CanFire()
    {
        if(firing >= fire_rate)
            return true;
        else return false;
    }
    public void Death()
    {
        if (this.gameObject.CompareTag("Player"))
        { 
            StartCoroutine(Reaparecer());
            
        }
       Debug.Log(this.name + " dies");
    }
    IEnumerator Reaparecer()
    {
        // Esperar un pequeño tiempo antes de mover al jugador para asegurar que la animación de muerte se complete
        yield return new WaitForSeconds(0.1f);

        // Mover al jugador de vuelta a la posición inicial
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            hp = hp_max;
            player.transform.position = initialPos;
        }
    }
}
