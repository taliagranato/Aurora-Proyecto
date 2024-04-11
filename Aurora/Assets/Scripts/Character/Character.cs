using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public int hp;
    public int hp_max;
    public float fire_rate;
    public float firing;
    public bool playerBool;// variable que guarda la posici�n inicial del jugador
    public Vector3 initialPos;

    protected virtual void Start()
    {
        // Guardar la posici�n inicial del jugador al inicio del juego
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        initialPos = player.transform.position;
        // Ajustar la posici�n inicial seg�n el tama�o del collider del jugador
        Collider playerCollider = player.GetComponent<Collider>();
        if (playerCollider != null)
        {
            initialPos.y += playerCollider.bounds.extents.y; // Ajustar seg�n el tama�o del collider
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
        if (playerBool && hp <= 0) 
        {
            Death();
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
        // Esperar un peque�o tiempo antes de mover al jugador para asegurar que la animaci�n de muerte se complete
        yield return new WaitForSeconds(0.1f);

        // Mover al jugador de vuelta a la posici�n inicial
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.transform.position = initialPos;
        }
    }
}
