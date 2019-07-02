﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    private static GameObject uiCanvas;
    public static string gamePhase;
    public static int wave;
    public static int leafHP;
    private static int money;
    public static int buildableTowers;

    //public GameObject spawn1; now in array
    //public GameObject spawn2;
    //public GameObject spawn3;
    //public GameObject spawn4;
    //public GameObject spawn5;
    //public GameObject spawn6;
    //public GameObject spawn7;
    //public GameObject spawn8;
    //public GameObject spawn9;
    //public GameObject spawn10;

    public GameObject[] spawns;
    public GameObject[] activatedSpawns;

    public GameObject enemy1;
    public GameObject enemy2;

    //Specified in spawnMinions
    private GameObject spawndable;
    public float sleep = 0;
    int musteringPoints;
    static int activeMinionsOnField;
    bool pauseToggle;

    // Start is called before the first frame update
    void Start()
    {
        uiCanvas = GameObject.FindWithTag("UI");
        gamePhase = "Build";
        wave = 1;
        leafHP = 100;
        activeMinionsOnField = 0;
        money = 10;
        buildableTowers = 3;
        pauseToggle = false;
        InvokeRepeating("CalculateActiveMinionsOnFieldInvokeRepeating", 1, 2);
        //ToastText.Instance.Show3DTextToast("Text Message", 10);

        activatedSpawns = new GameObject[10];
        int rnd = Random.Range(0, 10);
        spawns[rnd].transform.GetChild(0).GetComponent<Digger>().activateDigger();
        activatedSpawns[0] = spawns[rnd];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(pauseToggle)
            {
                Time.timeScale = 1;
                uiCanvas.GetComponent<CanvasScript>().enablePauseObjects(false);
                pauseToggle = !pauseToggle;
            }
            else if(!pauseToggle){
                Time.timeScale = 0;
                uiCanvas.GetComponent<CanvasScript>().enablePauseObjects(true);
                pauseToggle = !pauseToggle;
            }
        }
        if (Input.GetButtonDown("Jump")) {
            startAttack();
        }
        spawnMinions();
        Debug.Log(gamePhase);

        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            buildableTowers += 5;
            money += 10;
            uiCanvas.GetComponent<CanvasScript>().updateMoney(money);
            uiCanvas.GetComponent<CanvasScript>().updateTowers(buildableTowers);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            Debug.Log("Active minions on field : " + activeMinionsOnField + "  Game phase : " + gamePhase + "  Mustering Points : " + musteringPoints);
        }

    }

    public static string getGamePhase() {
        return gamePhase;
    }

    public void startAttack() {
        //if (Input.GetButtonDown("Jump") && gamePhase.Equals("Build")) {   //removed because of ui button
        if (gamePhase.Equals("Build")) {
            TileScript.cursorActive = false;
            gamePhase = "Attack";
            uiCanvas.transform.GetChild(4).gameObject.GetComponent<TextAnnouncer>().startAnnounce("Wave " + wave + " incoming!");
            sleep = 500;    //some delay before first minion comes
            //musteringPoints = 10 + (wave * (wave + wave + wave) );
            musteringPoints = 5 + (wave * wave);
            Debug.Log("Mustering Points : " + musteringPoints + "   Wave is : " + wave);
        }
    }

    public void spawnMinions() {
        if (gamePhase.Equals("Attack") && sleep < 1) {

            /*
            int maxSpawn = wave + 1;
            if (maxSpawn > 11) {
                maxSpawn = 11;
            }

            int rnd = Random.Range(1,maxSpawn); // 1-10

            spawns[maxSpawn-2].transform.GetChild(0).GetComponent<Digger>().activateDigger(); //play digger animation
            */

            int maxSpawn = wave;
            if(wave > 10 ) {
                maxSpawn = 10;
            }

            int rnd = Random.Range(0, maxSpawn);

            //Testing Enemy spawngin. Needs to be reworked in future! -------------------------------------------------HOX HOX HOX!
            int rndEnemy = Random.Range(1, 101);
            if(rndEnemy > 0 && rndEnemy < 50){
                spawndable = enemy1;
                sleep = 600;
            }
            else if (rndEnemy >= 50 && rndEnemy <= 100){
                sleep = 300;
                spawndable = enemy2;
            }

            if (musteringPoints >= spawndable.GetComponent<EnemyScript>().getCost()) {

                Debug.Log("Spawnpoint RND was : " + rnd);
                Instantiate(spawndable, activatedSpawns[rnd].transform.position, Quaternion.identity);

                musteringPoints -= spawndable.GetComponent<EnemyScript>().getCost();
                activeMinionsOnField++;
            }
            //------------------------------Nopee korjaus------------------------------
            else if(musteringPoints >= enemy2.GetComponent<EnemyScript>().getCost()){
                Instantiate(enemy2, activatedSpawns[rnd].transform.position, Quaternion.identity);

                musteringPoints -= enemy2.GetComponent<EnemyScript>().getCost();
                activeMinionsOnField++;
                musteringPoints = 0;
            }
            else{
                musteringPoints = 0;
            }
            //------------------------------Nopee korjaus------------------------------
        }

        //Enemy Spawn speed over waves
        //HighScores :
        //2.7 Mikko - Wave 17 - vaikeus 2f
        if (gamePhase.Equals("Attack")){
            sleep -= (100 + (wave * wave * 2f)) * Time.deltaTime;
        }
        
        //Check for wave end condition
        if (musteringPoints < 1 && gamePhase.Equals("Attack") && activeMinionsOnField == 0) {
            gamePhase = "Build";
            uiCanvas.transform.GetChild(4).gameObject.GetComponent<TextAnnouncer>().startAnnounce("Wave " + wave + " complete!");
            wave++;
            //money += 50;
            buildableTowers += 1;
            Debug.Log("Mustring : " + musteringPoints + "    Wave : " + wave);
            TileScript.cursorActive = true;

            GameObject[] towers = GameObject.FindGameObjectsWithTag("Tower");
            int income = 0;
            foreach (GameObject tower in towers) {
                income += tower.GetComponent<TowerScript>().getMoneyProduction();
            }
            Debug.Log("Towers found : " +towers.Length);
            money += income;

            uiCanvas.GetComponent<CanvasScript>().updateWave(wave);
            uiCanvas.GetComponent<CanvasScript>().updateMoney(money);
            uiCanvas.GetComponent<CanvasScript>().updateTowers(buildableTowers);

            //update spawnpoint
            bool doUntil = false;

            if (wave < 11) {

                while (!doUntil) {
                    int rnd = Random.Range(0, 10); // 0-9

                    if (!spawns[rnd].transform.GetChild(0).GetComponent<Digger>().getActivated()) {
                        spawns[rnd].transform.GetChild(0).GetComponent<Digger>().activateDigger();
                        activatedSpawns[wave - 1] = spawns[rnd];
                        doUntil = true;
                    }

                }
            }
        }
    } 

    public static void eatLeafHP() {   //player hitpoints
        leafHP--;
        uiCanvas.GetComponent<CanvasScript>().updateLife(leafHP);
    }

    public static void removeMinionFromField() {
        activeMinionsOnField--;
    }

    public static void reduceMoney(int amount){
        money -= amount;
        uiCanvas.GetComponent<CanvasScript>().updateMoney(money);
    }

    public static void addTowers() {
        buildableTowers++;
        uiCanvas.GetComponent<CanvasScript>().updateTowers(buildableTowers);
    }

    public static void reduceTowers(){
        buildableTowers--;
        uiCanvas.GetComponent<CanvasScript>().updateTowers(buildableTowers);
    }

    public static int getLeafHP(){
        return leafHP;
    }

    public static int getMoney(){
        return money;
    }
    
    public static int getWave(){
        return wave;
    }
    public static int getTowers(){
        return buildableTowers;
    }

    public static void showEnemyHP(int hp, int maxhp, string name) {
        uiCanvas.transform.GetChild(5).gameObject.GetComponent<EnemyHPIndicator>().show(hp, maxhp, name);
    }

    void CalculateActiveMinionsOnFieldInvokeRepeating() {
        if (gamePhase.Equals("Attack")) {
            activeMinionsOnField = GameObject.FindGameObjectsWithTag("Enemy").Length;
        }
    }

    public static void messageToUI(string message) {
        uiCanvas.transform.GetChild(4).gameObject.GetComponent<TextAnnouncer>().startAnnounce(message);
    }
    public void resumeGame(){
        Time.timeScale = 1;
        uiCanvas.GetComponent<CanvasScript>().enablePauseObjects(false);
        pauseToggle = !pauseToggle;
    }
}
