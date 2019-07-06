using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundDestroyerScript : MonoBehaviour
{

    public float volumeModifier = 0f;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("SFXvolume", 0.5f) + volumeModifier;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GetComponent<AudioSource>().isPlaying) {
            Destroy(gameObject);
        }
    }
}
