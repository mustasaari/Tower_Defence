using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasScript : MonoBehaviour
{
    private Text wave;
    private Text life;
    private Text money;
    private Text towers;

    // Start is called before the first frame update
    void Start()
    {
        wave = GameObject.Find("Wave").GetComponent<Text>();
        life = GameObject.Find("Life").GetComponent<Text>();
        money = GameObject.Find("Money").GetComponent<Text>();
        towers = GameObject.Find("Towers").GetComponent<Text>();

        wave.text= "Wave: " + GameManagerScript.getWave();
        life.text= "Life: " + GameManagerScript.getLeafHP();
        money.text= "Money: " + GameManagerScript.getMoney();
        towers.text = "Buildable Towers: " + GameManagerScript.getTowers();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateWave(int w){
        wave.text= "Wave: " + w;
    }
    public void updateLife(int l){
        life.text= "Life: " + l;
    }
    public void updateMoney(int m){
        money.text= "Money: " + m;
    }
    public void updateTowers(int t){
        towers.text = "Buildable Towers: " + t;
    }
    public void enablePauseObjects(bool b){
        transform.GetChild(8).gameObject.SetActive(b);  //PausePanel
        transform.GetChild(7).gameObject.SetActive(b);  //AlphaLayer
        if(GameManagerScript.getGamePhase().Equals("Build")){
            transform.GetChild(6).gameObject.SetActive(!b);  //Launchwave
        }
        if(transform.GetChild(9).gameObject.active){
            transform.GetChild(9).gameObject.SetActive(b);
        }
        if(GameManagerScript.getGamePhase().Equals("Attack")){
            transform.GetChild(10).gameObject.SetActive(!b);
            transform.GetChild(10).GetChild(0).gameObject.GetComponent<Image>().color = Color.white;
            transform.GetChild(10).GetChild(1).gameObject.GetComponent<Image>().color = Color.white;
            transform.GetChild(10).GetChild(2).gameObject.GetComponent<Image>().color = Color.white;
            if(!b){
                int speed = Mathf.FloorToInt(GameManagerScript.getDesiredGameSpeed()) - 1;
                // transform.GetChild(10).GetChild(speed).gameObject.SetActive(true);
                transform.GetChild(10).GetChild(speed).gameObject.GetComponent<Image>().color = Color.green;
                Debug.Log("GameSpeed: " + speed);
            }
        }
    }

    public void setAttackPhase(bool phas) {
        if (phas) {
            transform.GetChild(6).gameObject.SetActive(false);
            transform.GetChild(10).gameObject.SetActive(true);
            transform.GetChild(10).GetChild(0).gameObject.GetComponent<Image>().color = Color.green;
        }
        else {
            transform.GetChild(6).gameObject.SetActive(true);
            transform.GetChild(10).gameObject.SetActive(false);
        }
    }
    public void setSelectedButton(float b){
        transform.GetChild(10).GetChild(0).gameObject.GetComponent<Image>().color = Color.white;
        transform.GetChild(10).GetChild(1).gameObject.GetComponent<Image>().color = Color.white;
        transform.GetChild(10).GetChild(2).gameObject.GetComponent<Image>().color = Color.white;

        int select = Mathf.FloorToInt(b) - 1;
        transform.GetChild(10).GetChild(select).gameObject.GetComponent<Image>().color = Color.green;
    }
}