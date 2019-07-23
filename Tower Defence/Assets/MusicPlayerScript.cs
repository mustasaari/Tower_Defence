using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayerScript : MonoBehaviour
{

    float musicVolume;

    public AudioClip attackMusic;
    public AudioClip buildMusic;
    // Start is called before the first frame update

    void Start()
    {
        musicVolume = PlayerPrefs.GetFloat("Musicvolume", 0.5f);
        GetComponent<AudioSource>().volume = musicVolume;
        GetComponent<AudioSource>().Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setMusicVolume(float vol) {
        musicVolume = vol;
        GetComponent<AudioSource>().volume = musicVolume;
    }

    public void playAttackMusic() {
        GetComponent<AudioSource>().clip = attackMusic;
        GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("Musicvolume", 0.5f);
        GetComponent<AudioSource>().Play();
    }

    public void playBuildMusic() {
        GetComponent<AudioSource>().clip = buildMusic;
        GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("Musicvolume", 0.5f);
        GetComponent<AudioSource>().Play();
    }
}
