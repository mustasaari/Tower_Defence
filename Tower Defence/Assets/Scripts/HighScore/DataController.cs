using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class DataController : MonoBehaviour 
{
    // private PlayerProgress playerProgress;

    public Text nickNameInput;

    void Start()
    {
        // Create a temporary reference to the current scene.
        Scene currentScene = SceneManager.GetActiveScene();
        // Retrieve the name of this scene.s
        string sceneName = currentScene.name;

        if(sceneName.Equals("Game Over") && checkIfNewHighScore(PlayerPrefs.GetInt("holder"))){
            
        }
    }

    public void SubmitNewPlayerScore(int newScore)
    {
        // If newScore is greater than playerProgress.top1, update playerProgress with the new value and call SavePlayerProgress()
        if(newScore > PlayerPrefs.GetInt("top3")){
            if(newScore > PlayerPrefs.GetInt("top2")){
                if(newScore > PlayerPrefs.GetInt("top1")){
                    SavePlayerProgress("top1", newScore);
                }
                else{
                    SavePlayerProgress("top2", newScore);
                }
            }
            else{
                SavePlayerProgress("top3", newScore);
            }
        }
        PlayerPrefs.Save();
    }

    public void askPlayerNickName(){
        string nn = nickNameInput.text;
        //holder for wave nro.
        SubmitNewPlayerScore(PlayerPrefs.GetInt("holder"));
    }

    public bool checkIfNewHighScore(int checkScore){
        if(checkScore > PlayerPrefs.GetInt("top3")){
            return true;
        }
        return false;
    }

    public int GetHighestPlayerScore()
    {
        return PlayerPrefs.GetInt("top1");
    }

    // This function could be extended easily to handle any additional data we wanted to store in our PlayerProgress object
    // private void LoadPlayerProgress()
    // {
    //     // Create a new PlayerProgress object
    //     playerProgress = new PlayerProgress();

    //     // If PlayerPrefs contains a key called "top1", set the value of playerProgress.highestScore using the value associated with that key
    //     if(PlayerPrefs.HasKey("top1"))
    //     {
    //         playerProgress.top1 = PlayerPrefs.GetInt("top1");
    //     }
    //     if(PlayerPrefs.HasKey("top2"))
    //     {
    //         playerProgress.top2 = PlayerPrefs.GetInt("top2");
    //     }
    //     if(PlayerPrefs.HasKey("top3"))
    //     {
    //         playerProgress.top3 = PlayerPrefs.GetInt("top3");
    //     }
    // }

    // This function could be extended easily to handle any additional data we wanted to store in our PlayerProgress object
    private void SavePlayerProgress(string place, int score)
    {
        // eg. Save the value playerProgress.top1 to PlayerPrefs, with a key of "top1"
        PlayerPrefs.SetInt(place, score);
    }
}