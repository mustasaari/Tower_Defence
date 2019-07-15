using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour {

    void Start()
    {
        Time.timeScale = 1;
    }
    public void LoadByIndex(int sceneIndex)
    {
        SceneManager.LoadScene (sceneIndex);
    }

    public void ExitByClick(){
        Application.Quit();
    }
}