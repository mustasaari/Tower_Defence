using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroScript : MonoBehaviour
{
    // Start is called before the first frame update

    float volumeLevel;
    bool fadeToNextLevel;
    float sceneStartTime;

    void Start()
    {
        volumeLevel = PlayerPrefs.GetFloat("Musicvolume", 0.5f);
        GetComponent<AudioSource>().volume = volumeLevel;
        GetComponent<AudioSource>().Play();

        sceneStartTime = Time.time;

        fadeToNextLevel = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey) {
            Debug.Log("A key or mouse click has been detected");
            GameObject.Find("LevelChanger").GetComponent<LevelChanger>().FadeToLevel(1);
            fadeToNextLevel = true;
        }

        if (fadeToNextLevel) {
            volumeLevel -= 0.3f * Time.deltaTime;
            GetComponent<AudioSource>().volume = volumeLevel;
        }
    }
}
