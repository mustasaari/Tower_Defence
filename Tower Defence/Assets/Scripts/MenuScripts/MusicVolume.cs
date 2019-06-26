using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicVolume : MonoBehaviour {

	void Update () {
		GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("Musicvolume");
	}
}