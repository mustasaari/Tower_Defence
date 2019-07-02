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
    private Button launcWave;
    private Image alphaImage;

    // Start is called before the first frame update
    void Start()
    {
        wave = GameObject.Find("Wave").GetComponent<Text>();
        life = GameObject.Find("Life").GetComponent<Text>();
        money = GameObject.Find("Money").GetComponent<Text>();
        towers = GameObject.Find("Towers").GetComponent<Text>();
        launcWave = GameObject.Find("LaunchWave").GetComponent<Button>();
        alphaImage = GameObject.Find("AlphaLayer").GetComponent<Image>();

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
    public void enableLayerImage(bool b){
        transform.GetChild(8).gameObject.SetActive(b);
        launcWave.gameObject.SetActive(!b);
        alphaImage.enabled = b;
    }
}
