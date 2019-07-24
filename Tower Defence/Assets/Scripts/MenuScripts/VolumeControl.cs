using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour {

	public Slider SFXvolume;
	public Slider Musicvolume;
	public Dropdown grapQuality;

	void Start(){
		SFXvolume.value = PlayerPrefs.GetFloat("SFXvolume", 0.5f);
		Musicvolume.value = PlayerPrefs.GetFloat("Musicvolume", 0.5f);
		grapQuality.value = QualitySettings.GetQualityLevel();

		SFXvolume.onValueChanged.AddListener(delegate {SFXValueChangeCheck(); });
		Musicvolume.onValueChanged.AddListener(delegate {MusicValueChangeCheck(); });
	}

	void Update (){
		PlayerPrefs.Save();
	}

	public void SFXValueChangeCheck() {
		PlayerPrefs.SetFloat("SFXvolume", SFXvolume.value);
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

	public void MusicValueChangeCheck() {
		PlayerPrefs.SetFloat("Musicvolume", Musicvolume.value);
		GameObject.Find("MusicPlayer").GetComponent<MusicPlayerScript>().setMusicVolume(Musicvolume.value);
	}
}