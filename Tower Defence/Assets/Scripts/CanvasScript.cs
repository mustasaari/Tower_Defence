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
        transform.GetChild(6).gameObject.SetActive(!b);  //Launchwave
        if(transform.GetChild(9).gameObject.active){
            transform.GetChild(9).gameObject.SetActive(b);
        }
    }

    public void setAttackPhase(bool phas) {
        if (phas) {
            transform.GetChild(6).gameObject.SetActive(false);
            transform.GetChild(10).gameObject.SetActive(true);
        }
        else {
            transform.GetChild(6).gameObject.SetActive(true);
            transform.GetChild(10).gameObject.SetActive(false);
        }
    }
}
