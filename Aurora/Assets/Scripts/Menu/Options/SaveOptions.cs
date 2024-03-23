using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script que se encargará de guardar las preferencias del jugador entre escenas

public class SaveOptions : MonoBehaviour
{
    private void Awake()
    {
        var dontDestroyScenes = FindObjectsOfType<SaveOptions>();
        if (dontDestroyScenes.Length > 1) // Evitar que haya duplicador
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject); // función de unity
    }
}
