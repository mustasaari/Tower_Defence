using System.Collections;
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
        InvokeRepeating("CalculateActiveMinionsOnFieldInvokeRepeating", 1, 2);
        ToastText.Instance.Show3DTextToast("Text Message", 10);

        activatedSpawns = new GameObject[10];
        int rnd = Random.Range(0, 10);
        spawns[rnd].transform.GetChild(0).GetComponent<Digger>().activateDigger();
        activatedSpawns[0] = spawns[rnd];
    }

    // Update is called once per frame
    void Update()
    {
        startAttack();
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
        if (Input.GetButtonDown("Jump") && gamePhase.Equals("Build")) {
            TileScript.cursorActive = false;
            gamePhase = "Attack";
            uiCanvas.transform.GetChild(4).gameObject.GetComponent<TextAnnouncer>().startAnnounce("Wave " + wave + " incoming!");
            sleep = 500;    //some delay before first minion comes
            //musteringPoints = 10 + (wave * (wave + wave + wave) );
            musteringPoints = 10 + (wave * wave);
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
                sleep = 500;
            }
            else if (rndEnemy >= 50 && rndEnemy <= 100){
                sleep = 300;
                spawndable = enemy2;
            }

            if (musteringPoints > 0) {

                Debug.Log("Spawnpoint RND was : " + rnd);
                Instantiate(spawndable, activatedSpawns[rnd].transform.position, Quaternion.identity);

                musteringPoints -= spawndable.GetComponent<EnemyScript>().getCost();
                activeMinionsOnField++;

                
                /*
                if (rnd == 1) {
                    Instantiate(spawndable, spawn1.transform.position, Quaternion.identity);

                    musteringPoints -= spawndable.GetComponent<EnemyScript>().getCost();
                    activeMinionsOnField++;
                }
                else if (rnd == 2) {
                    Instantiate(spawndable, spawn2.transform.position, Quaternion.identity);

                    musteringPoints -= spawndable.GetComponent<EnemyScript>().getCost();
                    activeMinionsOnField++;
                }
                else if (rnd == 3) {
                    Instantiate(spawndable, spawn3.transform.position, Quaternion.identity);

                    musteringPoints -= spawndable.GetComponent<EnemyScript>().getCost();
                    activeMinionsOnField++;
                }
                else if (rnd == 4) {
                    Instantiate(spawndable, spawn4.transform.position, Quaternion.identity);

                    musteringPoints -= spawndable.GetComponent<EnemyScript>().getCost();
                    activeMinionsOnField++;
                }
                else if (rnd == 5) {
                    Instantiate(spawndable, spawn5.transform.position, Quaternion.identity);

                    musteringPoints -= spawndable.GetComponent<EnemyScript>().getCost();
                    activeMinionsOnField++;
                }
                else if (rnd == 6) {
                    Instantiate(spawndable, spawn6.transform.position, Quaternion.identity);

                    musteringPoints -= spawndable.GetComponent<EnemyScript>().getCost();
                    activeMinionsOnField++;
                }
                else if (rnd == 7) {
                    Instantiate(spawndable, spawn7.transform.position, Quaternion.identity);

                    musteringPoints -= spawndable.GetComponent<EnemyScript>().getCost();
                    activeMinionsOnField++;
                }
                else if (rnd == 8) {
                    Instantiate(spawndable, spawn8.transform.position, Quaternion.identity);

                    musteringPoints -= spawndable.GetComponent<EnemyScript>().getCost();
                    activeMinionsOnField++;
                }
                else if (rnd == 9) {
                    Instantiate(spawndable, spawn9.transform.position, Quaternion.identity);

                    musteringPoints -= spawndable.GetComponent<EnemyScript>().getCost();
                    activeMinionsOnField++;
                }
                else if (rnd == 10) {
                    Instantiate(spawndable, spawn10.transform.position, Quaternion.identity);

                    musteringPoints -= spawndable.GetComponent<EnemyScript>().getCost();
                    activeMinionsOnField++;
                } */



            }
        }

        if (gamePhase.Equals("Attack")){
            sleep -= (100 + (wave * wave/1.5f)) * Time.deltaTime;
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

    public static void reduceMoney(){
        money -= 1;
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
}
