using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Player : Character
{
    public int score;
    private int bullet_max = 4;
    public int bullet_active;

    public Image bar; // Rellenador barra de vida
    public Image[] bulletSprites; // Lista de los sprites para las balas


    public GameObject bullet;
    public GameObject specialBulletPrefab; // Prefab de la bala especial
    public GameObject fire_point;
    public GameObject Camera;

    private bool specialBulletReady = false; // Indica si la bala especial está lista
    private bool isFiringSpecialBullet = false; // Indica si el jugador está disparando la bala especial


    private void Awake()
    {
        hp_max = 100;
        hp = hp_max;
        bullet_active = bullet_max;
    }

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        InitializeBulletSprites();
        StartCoroutine(SpecialBulletTimer());
    }

    // Update is called once per frame
    void Update()
    {
       UpdateBar(); // Actualizar barra de vida
        if (CanFire() && Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (bullet_active > 0) 
            {
                Fire();
                bullet_active--;
                UpdateBulletUI();
            }  
            else if (bullet_active <= 0)
              StartCoroutine(Reloading());
        }
        if (Input.GetKeyDown(KeyCode.Q) && specialBulletReady)
        {
            FireSpecialBullet();
        }

    }
    void InitializeBulletSprites()
    {
        bulletSprites = new Image[bullet_max];
        for (int i = 0; i < bullet_max; i++)
        {
            bulletSprites[i] = GameObject.Find("Bullet" + (i+1)).GetComponent<Image>();
        }
        UpdateBulletUI();
    }

    void UpdateBulletUI()
    {
        for (int i = 0; i < bullet_max; i++)
        {
            Color newColor = bulletSprites[i].color;
            if (i < bullet_active)
            {
                newColor.a = 1f;
            }
            else
            {
                newColor.a = 0.2f; // Saturación reducida para las balas inactivas
            }
            bulletSprites[i].color = newColor;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Collectable")
        {
            score++;
            Debug.Log("Score: " + score);
            Destroy(other.gameObject);
        }
        if (other.tag == "Damage")
        {
            hp -= 5;
            Debug.Log("Hp: " + hp);
        }
    }

    // Método que actualiza visualmente la barra de vida del jugador o enemigo
    void UpdateBar()
    {
        // Regla de tres
        float lifeImage = (float)hp / hp_max; // imagen completa cuando la vida está al máximo
        bar.fillAmount = lifeImage;
    }

    void Fire()
    {
        Instantiate(bullet, fire_point.transform.position, Camera.transform.rotation);
        firing = 0;
    }

    void FireSpecialBullet()
    {
        Instantiate(specialBulletPrefab, fire_point.transform.position, Camera.transform.rotation);
        specialBulletReady = false; // Desactivar la bala especial después de usarla
    }
    IEnumerator SpecialBulletTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(10f); // Esperar 30 segundos
            Debug.Log("Bala especial a punto!");
            specialBulletReady = true; // Activar la bala especial
        }
    }

    IEnumerator Reloading()
    {
        Debug.Log("Recharging...");
        yield return new WaitForSeconds(2f);
        bullet_active = bullet_max;
        UpdateBulletUI(); // Asegurar que los sprites se actualizan después de recargar
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Damage")
        {
            hp -= 5;
            Debug.Log("Hp: " + hp);
        }
    }
}
