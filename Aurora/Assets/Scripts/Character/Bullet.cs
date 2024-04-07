using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Material color;
    Color random_color;


    // Start is called before the first frame update
    void Start()
    {
        AssignColor();
        color.SetColor("_Color", random_color);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void AssignColor()
    {
        random_color = new Color(Random.Range(0.0f,1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f),1.0f);
    }

}
