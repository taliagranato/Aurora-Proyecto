using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemyPrefab;

    public float cont;
    public float lim = 2;

    public int min = 1;
    public int max = 10;

    // Los enemigos que salgan de este punto, tendran esta direccion
    public Vector3 dirSpawn = Vector3.right;

    private void Start()
    {
        CalculoAleatorio();
    }

    // Update is called once per frame
    void Update()
    {
        //Actualizar el contador
        cont += Time.deltaTime;

        if (cont > lim)
        {
            GameObject newEnemy = Instantiate(enemyPrefab, this.transform.position, this.transform.rotation);
            newEnemy.GetComponent<Enemy>().dir = dirSpawn; // Actualizar dirección del enemigo creado

            cont = 0; // Volvemos el contador a 0
            CalculoAleatorio();
        }
    }

    void CalculoAleatorio()
    {
        lim = Random.Range(min, max);
    }
}
