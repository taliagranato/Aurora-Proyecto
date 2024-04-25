using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Material color;
    Color random_color;
    Color saved_color;
    public Rigidbody rb;
    public float speed;
    public int damage;
    Enemy enemy_hit;

    public GameObject paint;


    // Start is called before the first frame update
    void Start()
    {
        AssignColor();
        rb = GetComponent<Rigidbody>();
        color.SetColor("_BaseColor", random_color);
        rb.AddForce(this.transform.forward * speed);
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity != Vector3.zero) // Verifica si la flecha está cayendo
        {
            float rot = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0, 0, rot);
        }
    }

    private void AssignColor()
    {
        random_color = new Color(Random.Range(0.0f,1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f),1.0f);
        saved_color = random_color;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("Enemy hit");
            enemy_hit = collision.gameObject.GetComponent<Enemy>();
            enemy_hit.Damage(this.damage);
        }
        else
        {
            GameObject paintSplash = Instantiate(paint, collision.contacts[0].point, Quaternion.LookRotation(collision.contacts[0].normal));
            paintSplash.transform.position += paintSplash.transform.forward / 1000;
            paintSplash.GetComponent<Paint>().SetColor(saved_color);
            
        }
        Destroy(this.gameObject);



    }

}






















