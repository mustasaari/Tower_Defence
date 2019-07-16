using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class DataController : MonoBehaviour 
{
    // private PlayerProgress playerProgress;

    public GameObject hsPanel;

    void Start()
    {
        SubmitNewPlayerScore(2);
        SubmitNewPlayerScore(1);
        SubmitNewPlayerScore(3);

        // Create a temporary reference to the current scene.
        Scene currentScene = SceneManager.GetActiveScene();
        // Retrieve the name of this scene.s
        string sceneName = currentScene.name;

        if(sceneName.Equals("Main Menu")){
            initHSpanel();
        }
    }

    public void SubmitNewPlayerScore(int newScore)
    {
        // If newScore is greater than playerProgress.top1, update playerProgress with the new value and call SavePlayerProgress()
        if(newScore > PlayerPrefs.GetInt("top3")){
            if(newScore > PlayerPrefs.GetInt("top2")){
                if(newScore > PlayerPrefs.GetInt("top1")){
                    //top1 to top2
                    if(PlayerPrefs.HasKey("top1")){
                        //top2 to top3 if top2 already exist
                        if(PlayerPrefs.HasKey("top2")){
                        SavePlayerProgress("top3", PlayerPrefs.GetInt("top2"));
                        PlayerPrefs.SetString("top3Date", PlayerPrefs.GetString("top2Date"));
                        }

                        SavePlayerProgress("top2", PlayerPrefs.GetInt("top1"));
                        PlayerPrefs.SetString("top2Date", PlayerPrefs.GetString("top1Date"));
                    }
                    SavePlayerProgress("top1", newScore);
                    PlayerPrefs.SetString("top1Date", System.DateTime.Now.ToString("MM/dd/yyyy"));
                }
                else{
                    //top2 to top3
                    if(PlayerPrefs.HasKey("top2")){
                        SavePlayerProgress("top3", PlayerPrefs.GetInt("top2"));
                        PlayerPrefs.SetString("top3Date", PlayerPrefs.GetString("top2Date"));
                        Debug.Log("top2 siirto -> 3");
                    }
                    SavePlayerProgress("top2", newScore);
                    PlayerPrefs.SetString("top2Date", System.DateTime.Now.ToString("MM/dd/yyyy"));
                }
            }
            else{
                SavePlayerProgress("top3", newScore);
                PlayerPrefs.SetString("top3Date", System.DateTime.Now.ToString("MM/dd/yyyy"));
            }
        }
        PlayerPrefs.Save();
    }

    public bool checkIfNewHighScore(int checkScore){
        if(checkScore > PlayerPrefs.GetInt("top3")){
            return true;
        }
        return false;
    }

    public int [] GetHighScores(){
        int [] topList = new int [2];
        topList[0] = PlayerPrefs.GetInt("top1");
        topList[1] = PlayerPrefs.GetInt("top2");
        topList[2] = PlayerPrefs.GetInt("top3");
        return topList;
    }

    public int GetHighestPlayerScore()
    {
        return PlayerPrefs.GetInt("top1");
    }

    // This function could be extended easily to handle any additional data we wanted to store in our PlayerProgress object
    private void SavePlayerProgress(string place, int score)
    {
        // eg. Save the value playerProgress.top1 to PlayerPrefs, with a key of "top1"
        PlayerPrefs.SetInt(place, score);
    }

    private void initHSpanel(){
        hsPanel.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Text>().text = PlayerPrefs.GetString("top1Date");
        hsPanel.transform.GetChild(0).GetChild(1).gameObject.GetComponent<Text>().text = PlayerPrefs.GetInt("top1").ToString();

        hsPanel.transform.GetChild(1).GetChild(0).gameObject.GetComponent<Text>().text = PlayerPrefs.GetString("top2Date");
        hsPanel.transform.GetChild(1).GetChild(1).gameObject.GetComponent<Text>().text = PlayerPrefs.GetInt("top2").ToString();

        hsPanel.transform.GetChild(2).GetChild(0).gameObject.GetComponent<Text>().text = PlayerPrefs.GetString("top3Date");
        hsPanel.transform.GetChild(2).GetChild(1).gameObject.GetComponent<Text>().text = PlayerPrefs.GetInt("top3").ToString();
    }
}