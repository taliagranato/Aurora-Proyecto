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
    private int max_collectibles = 8;

    public Image bar; // Rellenador barra de vida
    public Image[] bulletSprites; // Lista de los sprites para las balas
    public Image[] collectableSprites; // Lista de los sprites para las balas
    private bool inici = true;
    private bool tocat = false;

    public GameObject bullet;
    public GameObject specialBulletPrefab; // Prefab de la bala especial
    public GameObject fire_point;
    public GameObject Camera;

    private bool specialBulletReady = false; // Indica si la bala especial está lista
    public Image specialBulletFill; // Relleno para el sprite de la bala especial
    public float specialBulletFillDuration = 10f; // Duración en segundos para llenar el sprite de la bala especial
                                                  //  private bool isFiringSpecialBullet = false; // Indica si el jugador está disparando la bala especial
    private Pause pauseScript;

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
        inici = true;
        InitializeBulletSprites();
        InitializeCollectSprites();
        StartCoroutine(SpecialBulletTimer());
        pauseScript = GameObject.FindObjectOfType<Pause>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!pauseScript.isPaused)
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
        

    }

    // Iniciacion Sprites
    void InitializeBulletSprites()
    {
        bulletSprites = new Image[bullet_max];
        for (int i = 0; i < bullet_max; i++)
        {
            bulletSprites[i] = GameObject.Find("Bullet" + (i + 1)).GetComponent<Image>();
        }
        UpdateBulletUI();
    }

    void InitializeCollectSprites()
    {
        collectableSprites = new Image[max_collectibles];
        for (int i = 0; i < max_collectibles; i++)
        {
            collectableSprites[i] = GameObject.Find("Collectible" + (i + 1)).GetComponent<Image>();
        }
        UpdateCollectUI();

    }
    // Update Sprites
    void UpdateBulletUI()
    {
        for (int i = 0; i < bullet_max; i++)
        {
            Color newColor = bulletSprites[i].color;

            if (i < bullet_active)
            {
                float saturation = 1f; // Componente de saturación
                Color.RGBToHSV(newColor, out float h, out float s, out float v); // Convertir el color a HSV
                s = saturation; // Ajustar la saturación
                newColor = Color.HSVToRGB(h, s, v);
                newColor.a = 1f; // Mantener la opacidad completa
            }
            else
            {
                float saturation = 0.3f;
                Color.RGBToHSV(newColor, out float h, out float s, out float v);
                s = saturation;
                newColor = Color.HSVToRGB(h, s, v);
                newColor.a = 0.8f; // Reducir la opacidad para las balas inactivas
            }
            bulletSprites[i].color = newColor;
        }
    }

    void UpdateCollectUI()
    {
        for (int i = 0; i < max_collectibles; i++)
        {
            Color newColor = collectableSprites[i].color;

            if (inici)
            {
              //  float saturation = 0.2f; // Componente de saturación
                Color.RGBToHSV(newColor, out float h, out float s, out float v); // Convertir el color a HSV
               // s = saturation; // Ajustar la saturación
                v = 0.2f;
                newColor = Color.HSVToRGB(h, s, v);
                newColor.a = 0.8f; // Mantener la opacidad completa
            }
            if (tocat)
            {
                //float saturation = 1f;
                Color.RGBToHSV(newColor, out float h, out float s, out float v);
               // s = saturation;
                v = 1f;
                newColor = Color.HSVToRGB(h, s, v);
                newColor.a = 1f; // Reducir la opacidad para las balas inactivas
                tocat = false;
            }
            collectableSprites[i].color = newColor;
        }
    }

    // Método que actualiza visualmente la barra de vida del jugador o enemigo
    void UpdateBar()
    {
        // Regla de tres
        float lifeImage = (float)hp / hp_max; // imagen completa cuando la vida está al máximo
        bar.fillAmount = lifeImage;
    }

    // Disparo
    void Fire()
    {
        Instantiate(bullet, fire_point.transform.position, Camera.transform.rotation);
        firing = 0;
    }


    // Triggers and collisions
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Collectable")
        {
            score++;
            Debug.Log("Score: " + score);
            Destroy(other.gameObject);
            tocat = true;
            UpdateCollectUI();
        }
        if (other.tag == "Damage")
        {
            hp -= 5;
            Debug.Log("Hp: " + hp);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Damage")
        {
            hp -= 5;
            Debug.Log("Hp: " + hp);
        }
    }

    // Corrutinas
    IEnumerator SpecialBulletTimer()
    {
        float timer = specialBulletFillDuration;

        while (timer > 0)
        {
            timer -= Time.deltaTime;
            float fillAmount = Mathf.Clamp01(1f - (timer / specialBulletFillDuration));
            UpdateSpecialBulletFill(fillAmount);
            yield return null;
        }

        // Llenar completamente el sprite de la bala especial
        UpdateSpecialBulletFill(1f);

        specialBulletReady = true; // Activar la bala especial
    }
    void FireSpecialBullet()
    {
        Instantiate(specialBulletPrefab, fire_point.transform.position, Camera.transform.rotation);
        specialBulletReady = false; // Desactivar la bala especial después de usarla
        StartCoroutine(SpecialBulletTimer());
    }


    // Método para actualizar el relleno del sprite de la bala especial
    void UpdateSpecialBulletFill(float fillAmount)
    {
        specialBulletFill.fillAmount = fillAmount;
    }

    IEnumerator Reloading()
    {
        Debug.Log("Recharging...");
        yield return new WaitForSeconds(2f);
        bullet_active = bullet_max;
        UpdateBulletUI(); // Asegurar que los sprites se actualizan después de recargar
    }
}