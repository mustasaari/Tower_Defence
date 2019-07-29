using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class DataController : MonoBehaviour 
{
    public GameObject hsPanel;
    public GameObject expPanel;
    private bool newHighScore;
    private int submittedWave;
    private static bool newHS;
    private static bool fadeComp;

    void Start()
    {
        // Create a temporary reference to the current scene.
        Scene currentScene = SceneManager.GetActiveScene();
        // Retrieve the name of this scene.s
        string sceneName = currentScene.name;

        if(sceneName.Equals("Main Menu")){
            initHSpanel();
        }
        if(sceneName.Equals("Game Over")){
            hsPanel.SetActive(true);
            hsPanel.transform.parent.GetChild(0).gameObject.SetActive(false);
            hsPanel.transform.GetChild(0).gameObject.GetComponent<Text>().text = "Game Over";
            hsPanel.transform.GetChild(1).GetChild(0).gameObject.GetComponent<Text>().text = "You killed " + GameManagerScript.getKills() + " enemies and survived to wave "+ (PlayerPrefs.GetInt("survivedWaves"));
            
            if(newHS){
                setnewHSBool(false);
                newHighScoreTrigger();
            }
            hsPanel.transform.parent.GetChild(2).GetChild(0).gameObject.GetComponent<Text>().text = PlayerPrefs.GetInt("Experience", 0).ToString();
            hsPanel.transform.parent.GetChild(2).GetChild(1).gameObject.GetComponent<Text>().text = "0";
        }
    }

    public void SubmitNewPlayerScore(int newScore)
    {
        newHighScore = true;
        PlayerPrefs.SetInt("survivedWaves", newScore);
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
                    PlayerPrefs.SetString("top1Date", System.DateTime.Now.ToString("dd/MM/yyyy"));
                }
                else{
                    //top2 to top3
                    if(PlayerPrefs.HasKey("top2")){
                        SavePlayerProgress("top3", PlayerPrefs.GetInt("top2"));
                        PlayerPrefs.SetString("top3Date", PlayerPrefs.GetString("top2Date"));
                    }
                    SavePlayerProgress("top2", newScore);
                    PlayerPrefs.SetString("top2Date", System.DateTime.Now.ToString("dd/MM/yyyy"));
                }
            }
            else{
                SavePlayerProgress("top3", newScore);
                PlayerPrefs.SetString("top3Date", System.DateTime.Now.ToString("dd/MM/yyyy"));
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

    public int GetHighestPlayerScore()
    {
        return PlayerPrefs.GetInt("top1");
    }

    // This function could be extended easily to handle any additional data we wanted to store in our PlayerProgress object
    private void SavePlayerProgress(string place, int score)
    {
        // eg. Save the value playerProgress.top1 to PlayerPrefs, with a key of "top1"
        PlayerPrefs.SetInt(place, score - 1);
    }

    private void initHSpanel(){
        hsPanel.transform.GetChild(1).GetChild(0).gameObject.GetComponent<Text>().text = PlayerPrefs.GetString("top1Date");
        hsPanel.transform.GetChild(1).GetChild(1).gameObject.GetComponent<Text>().text = PlayerPrefs.GetInt("top1").ToString();

        hsPanel.transform.GetChild(2).GetChild(0).gameObject.GetComponent<Text>().text = PlayerPrefs.GetString("top2Date");
        hsPanel.transform.GetChild(2).GetChild(1).gameObject.GetComponent<Text>().text = PlayerPrefs.GetInt("top2").ToString();

        hsPanel.transform.GetChild(3).GetChild(0).gameObject.GetComponent<Text>().text = PlayerPrefs.GetString("top3Date");
        hsPanel.transform.GetChild(3).GetChild(1).gameObject.GetComponent<Text>().text = PlayerPrefs.GetInt("top3").ToString();
    }

    private void newHighScoreTrigger(){
        hsPanel.transform.GetChild(0).gameObject.GetComponent<Text>().text = "New Highscore!";
        newHighScore = false;
    }

    public void reInitSubmittedWave(){
        PlayerPrefs.SetInt("survivedWaves", 0);
    }

    public void setnewHSBool(bool a){
        newHS = a;
    }
    public void fadeInComplete(){

        Scene currentScene = SceneManager.GetActiveScene();
        // Retrieve the name of this scene.s
        string sceneName = currentScene.name;

        if(sceneName.Equals("Game Over") && GameManagerScript.getExp() > 0){

            expPanel.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("SFXvolume", 0.5f);
            //get all exp
            int a = PlayerPrefs.GetInt("Experience", 0);
            //set all exp + roundxp to prefs
            PlayerPrefs.SetInt("Experience", GameManagerScript.getExp() + a);
            
            StartCoroutine(startXPcounterRoutine(a));
        }
    }

    public void startRoundXPCount(int roundXP){
        expPanel.GetComponent<AudioSource>().Play();
        expPanel.transform.GetChild(1).GetComponent<SlidingNumbers>().setCountNumbers(0, (PlayerPrefs.GetInt("Experience", 0) - roundXP));
        expPanel.transform.GetChild(1).transform.GetComponent<SlidingNumbers>().setCountBool(true);
    }

    public void startallXPCount(int roundXP){
        expPanel.GetComponent<AudioSource>().Play();
        expPanel.transform.GetChild(0).GetComponent<SlidingNumbers>().setCountNumbers(roundXP, PlayerPrefs.GetInt("Experience", 0));
        expPanel.transform.GetChild(0).transform.GetComponent<SlidingNumbers>().setCountBool(true);
    }

    IEnumerator startXPcounterRoutine(int a){
        startRoundXPCount(a);
        //wait first xp counter
        yield return new WaitForSeconds(3);
        startallXPCount(a);
    }

    //------------------------------WAIT TEST------------------------------------------------
    // IEnumerator startXPcounterRoutine(){
    //     int a = PlayerPrefs.GetInt("Experience", 0);
    //     //set all exp + roundxp to prefs
    //     PlayerPrefs.SetInt("Experience", GameManagerScript.getExp() + a);
    //     //roundXp
    //     expPanel.transform.GetChild(1).GetComponent<SlidingNumbers>().setCountNumbers(0, (PlayerPrefs.GetInt("Experience", 0) - a));
    //     expPanel.transform.GetChild(1).transform.GetComponent<SlidingNumbers>().setCountBool(true);

    //     //wait first xp counter
    //     yield return new WaitForSeconds(2);

    //     //start allxp count when roundXp is ready
    //     if(expPanel.transform.GetChild(1).GetComponent<SlidingNumbers>().getCountBool()){
    //         //AllXp
    //         expPanel.transform.GetChild(0).GetComponent<SlidingNumbers>().setCountNumbers(a, PlayerPrefs.GetInt("Experience", 0));
    //         expPanel.transform.GetChild(0).transform.GetComponent<SlidingNumbers>().setCountBool(true);
    //     }
    // }
}