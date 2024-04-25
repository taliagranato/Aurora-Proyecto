using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteRotation : MonoBehaviour
{
    public GameObject Camera;

    private void Start()
    {
        Camera = GameObject.Find("MainCamera");
    }
    void Update()
    {
        this.transform.rotation = Camera.transform.rotation;
    }
}
