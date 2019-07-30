using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour {

	public Animator animator;

	bool playerInbound;

	private int LevelToLoad;

	public GameObject datacont;

	public void FadeToNextLevel () {
        FadeToLevel(3);
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
	public void OnFadeToGameOver(){
		Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

		if(sceneName.Equals("Game Over")){
			datacont.GetComponent<DataController>().fadeInComplete();
		}
	}
}