using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bell : MonoBehaviour
{
    public AudioSource bell_sound;
    public float min_time;
    public float max_time;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RandomBell());
    }


    IEnumerator RandomBell()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(min_time, max_time));
            bell_sound.Play();
        }
    }
}
