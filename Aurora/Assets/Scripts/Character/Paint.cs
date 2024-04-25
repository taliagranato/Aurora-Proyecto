using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Paint : MonoBehaviour
{
    public Sprite[] sprites;
    public SpriteRenderer sprite_renderer;
    private float fade;
    public Color fading;

    // Start is called before the first frame update
    void Start()
    {
        fade = 1.0f;
        sprite_renderer = this.GetComponent<SpriteRenderer>();
        sprite_renderer.sprite = sprites[Random.Range(0, 5)];
    }

    // Update is called once per frame
    void Update()
    {
        fading.a = fade;
        sprite_renderer.color = fading;
        fade -= 0.1f * Time.deltaTime; 
        if (fade <= 0.1f)
        {
            Destroy(this.gameObject, 0.5f);
        }
    }

    public void SetColor(Color c)
    {
        fading = c;
    }
}
