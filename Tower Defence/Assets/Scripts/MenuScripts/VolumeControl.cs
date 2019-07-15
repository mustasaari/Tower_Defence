using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VolumeControl : MonoBehaviour {

	public Slider SFXvolume;
	public Slider Musicvolume;

	void Start(){
		SFXvolume.value = PlayerPrefs.GetFloat("SFXvolume", 0.5f);
		Musicvolume.value = PlayerPrefs.GetFloat("Musicvolume", 0.35f);
	}

	void Update (){
		PlayerPrefs.SetFloat("SFXvolume", SFXvolume.value);
		PlayerPrefs.SetFloat("Musicvolume", Musicvolume.value);
		PlayerPrefs.Save();

		if (Input.GetKeyDown(KeyCode.S)) {
            Debug.Log("this comes from volumecontrol script");
    		SceneManager.LoadScene("Shop");
        }
	}
}