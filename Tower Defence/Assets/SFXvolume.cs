using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXvolume : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("SFXvolume", 0.5f);
        GetComponent<AudioSource>().Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
