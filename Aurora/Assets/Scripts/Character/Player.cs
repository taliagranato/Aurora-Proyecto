using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public int score;

    private void Awake()
    {
        hp = 100;
    }

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {

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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Damage")
        {
            hp -= 5;
            Debug.Log("Hp: " + hp);
        }
    }
}
