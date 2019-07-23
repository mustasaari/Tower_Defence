using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicVolume : MonoBehaviour {


    void Start() {
		GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("Musicvolume");
	}

	void Update () {
		//GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("Musicvolume");
	}
}