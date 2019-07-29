using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VolumeControl : MonoBehaviour {

	public Slider SFXvolume;
	public Slider Musicvolume;
	public Dropdown grapQuality;
	public Dropdown resolutionDropdown;

	Resolution[] resolutions;
	Resolution[] filteredResolutions;
	List<string> dropdownItems;

	int currentResolutionSelection;

	void Start(){
		SFXvolume.value = PlayerPrefs.GetFloat("SFXvolume", 0.5f);
		Musicvolume.value = PlayerPrefs.GetFloat("Musicvolume", 0.5f);
		grapQuality.value = QualitySettings.GetQualityLevel();

		SFXvolume.onValueChanged.AddListener(delegate {SFXValueChangeCheck(); });
		Musicvolume.onValueChanged.AddListener(delegate {MusicValueChangeCheck(); });

		resolutions = Screen.resolutions;
		dropdownItems = new List<string>();
		currentResolutionSelection = 0;

		//int calculator = 0;
		//foreach (Resolution resolution in resolutions) {
		//for (int i = 1; i < resolutions.Length; i += 2) {
			//if (resolution.refreshRate == 60) {
			//dropdownItems.Add(resolution.width +" x " +resolution.height);
			//dropdownItems.Add(resolutions[i].width +" x " +resolutions[i].height +" " +resolutions[i].refreshRate +"Hz");
			//if (resolution.width == Screen.width && resolution.height == Screen.height) {
				//currentResolutionSelection = calculator;
			//}
			//if (resolutions)
			//calculator++;
			//}
		//}

		int calculator = 0;
		List<Resolution> tempres = new List<Resolution>();

		// //Debug.Log("Original Resolutions : " +resolutions.Length);

		dropdownItems.Add(resolutions[0].width + " x " +resolutions[0].height);
		tempres.Add(resolutions[0]);

		for (int i = 1; i < resolutions.Length; i++) {
			if (resolutions[i].width != resolutions[i-1].width || resolutions[i].height != resolutions[i-1].height) {
				dropdownItems.Add(resolutions[i].width +" x " +resolutions[i].height);
				tempres.Add(resolutions[i]);
				calculator++;

				if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height) {
					currentResolutionSelection = calculator;
				}

			}
		}

		//Debug.Log("Resolutions string : " +dropdownItems.Count);
		//Debug.Log("Tempres length : " +tempres.Count);

		filteredResolutions = new Resolution[tempres.Count];

		for (int i = 0; i < tempres.Count; i++) {
			filteredResolutions[i] = tempres[i];
		}

		//Debug.Log("Filtered Resolutions : " +filteredResolutions.Length);

		Scene currentScene = SceneManager.GetActiveScene();
        // Retrieve the name of this scene.s
        string sceneName = currentScene.name;

        if(sceneName.Equals("Main Menu")){
			resolutionDropdown.AddOptions(dropdownItems);
			resolutionDropdown.value = currentResolutionSelection;
		}
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
			// Debug.Log("SFX changed");
	}

    public void dropdownChanged(int value) {
        // Debug.Log("Value" + value);
        QualitySettings.SetQualityLevel(value, true);
    }

	public void MusicValueChangeCheck() {
		PlayerPrefs.SetFloat("Musicvolume", Musicvolume.value);
		GameObject.Find("MusicPlayer").GetComponent<MusicPlayerScript>().setMusicVolume(Musicvolume.value);
	}

	public void dropdownResolutionChanged(int value) {

		Resolution reso = filteredResolutions[value];
		Screen.SetResolution(reso.width , reso.height, true);
	}
}