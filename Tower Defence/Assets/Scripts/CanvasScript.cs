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

        wave.text= "" + GameManagerScript.getWave();
        life.text= "" + GameManagerScript.getLeafHP();
        money.text= "+1  " + GameManagerScript.getMoney();
        towers.text = "" + GameManagerScript.getTowers();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateWave(int w){
        wave.text= "" + w;
    }
    public void updateLife(int l){
        life.text= "" + l;
    }
    public void updateMoney(int m){
        money.text= "+" +GameManagerScript.moneyPerTurn +"  " +m;
    }
    public void updateTowers(int t){
        towers.text = "" + t;
    }
    public void enablePauseObjects(bool b){
        transform.GetChild(9).gameObject.SetActive(b);  //PausePanel
        transform.GetChild(8).gameObject.SetActive(b);  //AlphaLayer
        if(GameManagerScript.getGamePhase().Equals("Build")){
            // transform.GetChild(7).gameObject.SetActive(!b);  //Launchwave
        }
        if(transform.GetChild(10).gameObject.active){
            transform.GetChild(10).gameObject.SetActive(b);
        }
        if(GameManagerScript.getGamePhase().Equals("Attack")){
            // transform.GetChild(11).gameObject.SetActive(!b);
            int speed = Mathf.FloorToInt(GameManagerScript.getDesiredGameSpeed()) - 1;
            if(!b){
                transform.GetChild(11).GetChild(speed).gameObject.GetComponent<Image>().color = Color.green;
                transform.GetChild(11).GetChild(0).gameObject.GetComponent<Button>().interactable = true;
                transform.GetChild(11).GetChild(1).gameObject.GetComponent<Button>().interactable = true;
                transform.GetChild(11).GetChild(2).gameObject.GetComponent<Button>().interactable = true;
            }
            else{   //paused -> speed buttons not interactable.
                transform.GetChild(11).GetChild(0).gameObject.GetComponent<Button>().interactable = false;
                transform.GetChild(11).GetChild(1).gameObject.GetComponent<Button>().interactable = false;
                transform.GetChild(11).GetChild(2).gameObject.GetComponent<Button>().interactable = false;
                transform.GetChild(11).GetChild(0).gameObject.GetComponent<Image>().color = Color.white;
                transform.GetChild(11).GetChild(1).gameObject.GetComponent<Image>().color = Color.white;
                transform.GetChild(11).GetChild(2).gameObject.GetComponent<Image>().color = Color.white;
                transform.GetChild(11).GetChild(speed).gameObject.GetComponent<Image>().color = Color.green;
            }
        }
    }

    public void setAttackPhase(bool phas) {
        if (phas) {
            transform.GetChild(7).gameObject.SetActive(false);
            transform.GetChild(11).gameObject.SetActive(true);
            transform.GetChild(11).GetChild(0).gameObject.GetComponent<Image>().color = Color.green;
        }
        else {
            transform.GetChild(7).gameObject.SetActive(true);
            transform.GetChild(11).gameObject.SetActive(false);
        }
    }
    public void setSelectedButton(float b){
        transform.GetChild(11).GetChild(0).gameObject.GetComponent<Image>().color = Color.white;
        transform.GetChild(11).GetChild(1).gameObject.GetComponent<Image>().color = Color.white;
        transform.GetChild(11).GetChild(2).gameObject.GetComponent<Image>().color = Color.white;

        int select = Mathf.FloorToInt(b) - 1;
        transform.GetChild(11).GetChild(select).gameObject.GetComponent<Image>().color = Color.green;
    }
}