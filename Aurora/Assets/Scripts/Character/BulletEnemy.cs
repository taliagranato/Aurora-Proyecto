using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{

    public Rigidbody rb;
    public float speed;
    Character player_hit;
    public int damage;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Player hit");
            if (player_hit.playerBool) 
            {
                player_hit = collision.gameObject.GetComponent<Character>();
                player_hit.TakeDamage(this.damage);
            }  
        }
        Destroy(this.gameObject);
    }
}






















