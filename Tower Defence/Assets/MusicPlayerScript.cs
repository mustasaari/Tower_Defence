using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayerScript : MonoBehaviour
{

    float musicVolume;

    public AudioClip attackMusic;
    public AudioClip buildMusic;
    // Start is called before the first frame update
    bool canPlayAudio;

    void Start()
    {
        canPlayAudio = true;
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
        if (canPlayAudio) {
            GetComponent<AudioSource>().clip = attackMusic;
            GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("Musicvolume", 0.5f);
            GetComponent<AudioSource>().Play();
        }
    }

    public void playBuildMusic() {
        if (canPlayAudio) {
            GetComponent<AudioSource>().clip = buildMusic;
            GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("Musicvolume", 0.5f);
            GetComponent<AudioSource>().Play();
        }
    }

    public void stopMusic() {
        canPlayAudio = false;
        GetComponent<AudioSource>().Stop();
    }
}
