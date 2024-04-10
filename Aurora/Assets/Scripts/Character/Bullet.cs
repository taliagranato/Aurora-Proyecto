using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Material color;
    Color random_color;
    public Rigidbody rb;
    public float speed;
    public int damage; 


    // Start is called before the first frame update
    void Start()
    {
        AssignColor();
        rb = GetComponent<Rigidbody>();
        color.SetColor("_Color", random_color);
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
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject, 0.2f);

    }

}






















