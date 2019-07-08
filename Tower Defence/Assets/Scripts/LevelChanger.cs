using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour {

	public Animator animator;

	bool playerInbound;

	private int LevelToLoad;

	public void FadeToNextLevel () {
        FadeToLevel(2);
	}

	public void FadeToLevel(int levelIndex) {

		if(levelIndex == 0){
			LevelToLoad = levelIndex;
			animator.SetTrigger("FadeOutEnd");
		}
		else {
			LevelToLoad = levelIndex;
			animator.SetTrigger("FadeOut");
		}
	}

	public void OnFadeComplete(){

		SceneManager.LoadScene(LevelToLoad);
	}
}