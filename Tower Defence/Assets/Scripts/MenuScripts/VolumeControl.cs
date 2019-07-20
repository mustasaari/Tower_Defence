using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour {

	public Slider SFXvolume;
	public Slider Musicvolume;

	void Start(){
		SFXvolume.value = PlayerPrefs.GetFloat("SFXvolume", 0.5f);
		Musicvolume.value = PlayerPrefs.GetFloat("Musicvolume", 0.35f);

		SFXvolume.onValueChanged.AddListener(delegate {SFXValueChangeCheck(); });
	}

	void Update (){
		PlayerPrefs.SetFloat("SFXvolume", SFXvolume.value);
		PlayerPrefs.SetFloat("Musicvolume", Musicvolume.value);
		PlayerPrefs.Save();
	}

	public void SFXValueChangeCheck() {
		if (!GetComponent<AudioSource>().isPlaying ) {
			PlayerPrefs.SetFloat("SFXvolume", SFXvolume.value);
			GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("SFXvolume", 0.5f);
			GetComponent<AudioSource>().Play();
		}
			Debug.Log("SFX changed");
	}

    public void dropdownChanged(int value) {
        Debug.Log("Value" + value);
        QualitySettings.SetQualityLevel(value, true);
    }
}