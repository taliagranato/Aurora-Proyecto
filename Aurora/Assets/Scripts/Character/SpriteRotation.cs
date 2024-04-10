using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteRotation : MonoBehaviour
{
    public GameObject Camera;

    // Update is called once per frame
    void Update()
    {
        this.transform.rotation = Camera.transform.rotation;
    }
}
