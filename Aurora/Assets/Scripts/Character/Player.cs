using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using StarterAssets;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using Cinemachine;
using Unity.VisualScripting;

public class Player : Character
{
  //  public int score;
    private int bullet_max = 4;
    public int bullet_active;
    public int collected;
    public GameObject end_text;

    public Image bar; // Rellenador barra de vida
    public Image[] bulletSprites; // Lista de los sprites para las balas
   
  private GameObject activeDescriptionText; // Almacena el texto de descripción activo actualmente
    
    public GameObject bullet;
    public GameObject specialBulletPrefab; // Prefab de la bala especial
    public GameObject fire_point;
    public GameObject Camera;

    private bool recharging;
    private bool specialBulletReady = false; // Indica si la bala especial está lista
    public Image specialBulletFill; // Relleno para el sprite de la bala especial
    public float specialBulletFillDuration = 10f; // Duración en segundos para llenar el sprite de la bala especial
                                                  //  private bool isFiringSpecialBullet = false; // Indica si el jugador está disparando la bala especial
    public Pause pauseScript;

    public ThirdPersonController _thirdPersonController; // Referencia al script ThirdPersonController
    public SpriteRenderer _spriteRenderer; // Referencia al componente SpriteRenderer del jugador

    //Post Processing
    public Volume volume;
    public ColorAdjustments color_adjustments;
    public float saturation;

    //Animation
    public Animator player_animator;

    //Sounds
    public AudioSource shoot_audio;
    public AudioSource reload_audio;


    private void Awake()
    {
        Cursor.visible = false;
        collected = 0;
        saturation = -60.0f;
        hp_max = 100;
        hp = hp_max;
        bullet_active = bullet_max;
        recharging = false;
        volume = GameObject.Find("PostProcessVolume").GetComponent<Volume>();
        volume.profile.TryGet(out color_adjustments);
        end_text.SetActive(false);
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        score = 0;
        InitializeBulletSprites();
        StartCoroutine(SpecialBulletTimer());
        pauseScript = GameObject.FindObjectOfType<Pause>();

    }

    // Update is called once per frame
    void Update()
    {
        player_animator.SetBool("shoot", false);
        color_adjustments.saturation.value = saturation;
        if (!pauseScript.isPaused)
        { 
            UpdateBar(); // Actualizar barra de vida
            if (CanFire() && Input.GetKeyDown(KeyCode.Mouse0) && !recharging && !player_animator.GetBool("jump") && player_animator.GetBool("landing"))
            {
                if (bullet_active > 0)
                {
                    player_animator.SetBool("shoot", true);
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
            if (Input.GetKeyDown(KeyCode.R))
            {
                StartCoroutine(Reloading());
            }
            if (_thirdPersonController != null)
            {
                // Obtener la dirección del movimiento del ThirdPersonController
                float moveDirectionX = _thirdPersonController.MoveDirection.x;
                
                // Escalar el sprite del jugador según la dirección del movimiento
                if (moveDirectionX > 0) // Movimiento hacia la derecha
                {
                    //ScalePlayerAndFirePoint(1);
                    _spriteRenderer.flipX = false;
                }
                else if (moveDirectionX < 0) // Movimiento hacia la izquierda
                {
                    // ScalePlayerAndFirePoint(moveDirectionX);
                    _spriteRenderer.flipX = true;
                }
            }
        }
        
        this.IsDead();   

    }
    // Método para ajustar la escala del jugador y del punto de disparo
    void ScalePlayerAndFirePoint(float direction)
    {
        // Obtener la escala actual del jugador y del punto de disparo
        Vector3 playerScale = transform.localScale;
        Vector3 firePointScale = fire_point.transform.localScale;

        // Calcular la nueva escala en función de la dirección
        float newScaleX = direction > 0 ? 1f : -1f;

        // Aplicar la nueva escala al jugador y al punto de disparo
        playerScale.x = newScaleX;
        firePointScale.x = newScaleX;

        // Establecer la escala actualizada
        transform.localScale = playerScale;
        fire_point.transform.localScale = firePointScale;
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

    // Update Sprites
    void UpdateBulletUI()
    {
        for (int i = 0; i < bullet_max; i++)
        {
            Color newColor = bulletSprites[i].color;

            if (i < bullet_active)
            {
                float value = 1f; // Componente de saturación
                Color.RGBToHSV(newColor, out float h, out float s, out float v); // Convertir el color a HSV
                v = value; // Ajustar la saturación
                newColor = Color.HSVToRGB(h, s, v);
                newColor.a = 1f; // Mantener la opacidad completa
            }
            else
            {
                float value = 0.3f;
                Color.RGBToHSV(newColor, out float h, out float s, out float v);
                v = value;
                newColor = Color.HSVToRGB(h, s, v);
                newColor.a = 0.8f; // Reducir la opacidad para las balas inactivas
            }
            bulletSprites[i].color = newColor;
        }
    }

    // Método que actualiza visualmente la barra de vida del jugador o enemigo
    public void UpdateBar()
    {
        // Regla de tres
        float lifeImage = (float)hp / hp_max; // imagen completa cuando la vida está al máximo
        bar.fillAmount = lifeImage;
    }

    // Disparo
    void Fire()
    {
        shoot_audio.Play();
        Instantiate(bullet, fire_point.transform.position, Camera.transform.rotation);

        firing = 0;
    }

    public void Saturation_Lighting()
    {
        saturation += 10.0f;


    }


    // Triggers and collisions
    protected override void OnTriggerEnter(Collider other)
     {
        base.OnTriggerEnter(other);
         if (other.tag == "Collectable")
         {
             collected++;
            Saturation_Lighting();
             score++;
             Debug.Log("Score: " + score);
             Destroy(other.gameObject);
             Collectable.Instance.OnCollectibleTriggered(other.gameObject); 
         }    


         
     }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "End" && collected >= 8)
        {
            end_text.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                end_text.SetActive(false);
                Debug.Log("Has Ganado");
                FadeOut fadeScript = FindObjectOfType<FadeOut>();
                fadeScript.StartFade("End Screen");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        end_text.SetActive(false);
    }

    /*
     private void OnCollisionEnter(Collision collision)
     {
         if (collision.gameObject.tag == "Damage")
         {
             hp -= 5;
             Debug.Log("Hp: " + hp);
         }
     }*/

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
        recharging = true;
        Debug.Log("Recharging...");
        for (int i = bullet_active; i < bullet_max; i++)
        {
            reload_audio.Play();
            yield return new WaitForSeconds(0.5f);
        }
        //yield return new WaitForSeconds(2f);
        bullet_active = bullet_max;
        UpdateBulletUI(); // Asegurar que los sprites se actualizan después de recargar
        recharging = false;
        
    }
}