using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button : MonoBehaviour
{
    public bool buttonOn;
    public static bool activePlatform;
    public GameObject button;

    //public AudioSource soundButton; // Sonido activar boton

    // Start is called before the first frame update
    void Start()
    {
        buttonOn = false;
        activePlatform = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (buttonOn)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                activePlatform = true;
                // button.transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z + -90);
               // soundButton.Play();
            }

        }
    }

    private void OnTriggerEnter(Collider otro)
    {
        if (otro.gameObject.tag == "Player")
        {
            buttonOn = true;
        }
    }
}
