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
    public GameObject enemy3;

    public AudioClip textPhaseAnnounce;
    public static int moneyPerTurn = 1;

    //Specified in spawnMinions
    private GameObject spawnable;
    public float sleep = 0;
    int musteringPoints;
    static int activeMinionsOnField;
    bool pauseToggle;
    static float desiredgamespeed;
    static int exp;
    static int kills;

    List<EnemySpawnData> listOfEnemies;
    int spawnProgress;

    // Start is called before the first frame update
    void Start()
    {
        kills = 0;
        exp = 0;
        desiredgamespeed = 1;
        Time.timeScale = 1;
        uiCanvas = GameObject.FindWithTag("UI");
        gamePhase = "Build";
        wave = 1;
        leafHP = 10 + PlayerPrefs.GetInt("BonusLife", 0);
        activeMinionsOnField = 0;
        money = 10 + PlayerPrefs.GetInt("BonusMoney", 0);
        buildableTowers = 3;
        pauseToggle = false;
        InvokeRepeating("CalculateActiveMinionsOnFieldInvokeRepeating", 1, 2);
        //ToastText.Instance.Show3DTextToast("Text Message", 10);

        activatedSpawns = new GameObject[10];
        int rnd = Random.Range(0, 10);
        spawns[rnd].transform.GetChild(0).GetComponent<Digger>().activateDigger();
        activatedSpawns[0] = spawns[rnd];
        // Debug.Log("Test: " + transform.GetComponent<DataController>().GetHighestPlayerScore());
        listOfEnemies = new List<EnemySpawnData>(); //new spawnsystem
        listOfEnemies.Add(new EnemySpawnData(wave)); //new spawnsystem
        spawnProgress = 0; //new spawnsystem
    }

    // Update is called once per frame
    void Update()
    {
        if(leafHP == 0){

            leafHP--;

            if(transform.GetComponent<DataController>().checkIfNewHighScore(wave)){
                transform.GetComponent<DataController>().setnewHSBool(true);
                transform.GetComponent<DataController>().SubmitNewPlayerScore(wave);
            }

            GameObject.Find("LevelChanger").GetComponent<LevelChanger>().FadeToNextLevel();
            // uiCanvas.GetComponent<CanvasScript>().startPhaseOut();
            // transform.GetComponent<LoadSceneOnClick>().LoadByIndex(2);

            //End animations & explosions
            Time.timeScale = 1f;
            desiredgamespeed = 1f;
            GameObject.Find("LevelChanger").GetComponent<Animator>().speed = 0.2f;
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().enabled = false;
            GameObject.FindGameObjectWithTag("DeathCamera").GetComponent<Camera>().enabled = true;
            GameObject.FindGameObjectWithTag("EnvironmentGround").GetComponent<MeshCollider>().enabled = true;
            GameObject[] crystals = GameObject.FindGameObjectsWithTag("Crystal");
            foreach (GameObject crystal in crystals) {
                crystal.GetComponent<CrystalDestroyer>().destroyCrystal();
            }
        }

        if(Input.GetKeyDown(KeyCode.Alpha5)) {
            QualitySettings.SetQualityLevel(0, true);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6)) {
            QualitySettings.SetQualityLevel(1, true);
        }
        if (Input.GetKeyDown(KeyCode.Alpha7)) {
            QualitySettings.SetQualityLevel(2, true);
        }
        if (Input.GetKeyDown(KeyCode.Alpha8)) {
            QualitySettings.SetQualityLevel(3, true);
        }
        if (Input.GetKeyDown(KeyCode.Alpha9)) {
            QualitySettings.SetQualityLevel(4, true);
        }
        if (Input.GetKeyDown(KeyCode.Alpha0)) {
            QualitySettings.SetQualityLevel(5, true);
        }


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(pauseToggle)
            {
                if(gamePhase.Equals("Attack")){
                    Time.timeScale = desiredgamespeed;
                }
                else{
                    Time.timeScale = 1;
                }
                uiCanvas.GetComponent<CanvasScript>().enablePauseObjects(false);
                pauseToggle = !pauseToggle;
            }
            else if(!pauseToggle){
                Time.timeScale = 0;
                uiCanvas.GetComponent<CanvasScript>().enablePauseObjects(true);
                pauseToggle = !pauseToggle;

                AudioSource[] allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
                foreach (AudioSource audioS in allAudioSources) {
                    audioS.Pause(); // ???
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Return)) {
            startAttack();
        }
        spawnMinions();
        Debug.Log(gamePhase);

        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            buildableTowers += 5;
            money += 10;
            uiCanvas.GetComponent<CanvasScript>().updateMoney(money);
            uiCanvas.GetComponent<CanvasScript>().updateTowers(buildableTowers);

            //add also experiance
            int xpMoney = PlayerPrefs.GetInt("Experience", 0);
            xpMoney += 1000;
            PlayerPrefs.SetInt("Experience", xpMoney);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            Debug.Log("Active minions on field : " + activeMinionsOnField + "  Game phase : " + gamePhase + "  Mustering Points : " + musteringPoints);
        }

        if (!pauseToggle) {
            checkDesiredGameSpeed();
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

            uiCanvas.transform.GetChild(5).gameObject.GetComponent<TextAnnouncer>().startAnnounce("Wave " + wave + " incoming!");
            playAudio(textPhaseAnnounce, 0.65f);

            sleep = 500;    //some delay before first minion comes
            //musteringPoints = 10 + (wave * (wave + wave + wave) );
            musteringPoints = 5 + (wave * wave);
            Debug.Log("Mustering Points : " + musteringPoints + "   Wave is : " + wave);
            uiCanvas.GetComponent<CanvasScript>().setAttackPhase(true);
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

            //int maxSpawn = wave; 18.7
            //if(wave > 5 ) { //10 muutettu
            //    maxSpawn = 5;   //10 muutettu
            //} 18.7

            //int rnd = Random.Range(0, maxSpawn);

            //Testing Enemy spawngin. Needs to be reworked in future! -------------------------------------------------HOX HOX HOX!
            //int rndEnemy = Random.Range(1, 20 + wave);  //Starst from 1-20
            //SpeedFly level 1      1-15  
            /* 
            if(rndEnemy >= 1 && rndEnemy <= 12){
                spawndable = enemy1;
                sleep = 300;
            }
            //LadyBug level 1       16 - 30
            else if (rndEnemy >= 13 && rndEnemy <= 26){
                sleep = 600;
                spawndable = enemy2;
            } //Beetle level 1
            else if( rndEnemy >= 27 && rndEnemy <= 999 ) {
                sleep = 600;
                spawndable = enemy3;
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
            }*/
            //------------------------------Nopee korjaus------------------------------
            if (spawnProgress < listOfEnemies.Count) { //new spawnsystem
                if (listOfEnemies[spawnProgress].getEnemyNumber() == 1) {
                    spawnable = enemy1; //speedfly
                    sleep = 600f;
                } 
                else if (listOfEnemies[spawnProgress].getEnemyNumber() == 2) {
                    spawnable = enemy2; //ladybug
                    sleep = 600f;
                }
                            else if (listOfEnemies[spawnProgress].getEnemyNumber() == 3) {
                    spawnable = enemy3; //beetle
                    sleep = 600f;
                }

                Instantiate(spawnable, activatedSpawns[listOfEnemies[spawnProgress].getSpwnPoint()].transform.position, Quaternion.identity);
                spawnProgress++;
                activeMinionsOnField++; //new spawnsystem
            }
        }

        //Enemy Spawn speed over waves
        //HighScores :
        //2.7 Mikko - Wave 17 - vaikeus 2f
        if (gamePhase.Equals("Attack")){
            sleep -= (100 + (wave * wave * 2f)) * Time.deltaTime;
        }
        
        //Check for wave end condition
        if (gamePhase.Equals("Attack") && activeMinionsOnField == 0 && spawnProgress == listOfEnemies.Count) {

            spawnProgress = 0;  //new spawnsystem
            //for (int i = 0; i < wave ;i++) { //new spawnsystem
                listOfEnemies.Add(new EnemySpawnData(wave + 1)); //new spawnsystem
            //} //new spawnsystem

            gamePhase = "Build";
            if (leafHP > 0) {
                uiCanvas.transform.GetChild(5).gameObject.GetComponent<TextAnnouncer>().startAnnounce("Wave " + wave + " complete!");
                playAudio(textPhaseAnnounce, 0.8f);
            }
            wave++;
            //money += 50;
            buildableTowers += 1;
            money += 1;
            Debug.Log("Mustring : " + musteringPoints + "    Wave : " + wave);
            TileScript.cursorActive = true;

            GameObject[] towers = GameObject.FindGameObjectsWithTag("Tower");
            int income = 0;
            foreach (GameObject tower in towers) {
                income += tower.GetComponent<TowerScript>().getMoneyProduction();
            }
            Debug.Log("Towers found : " +towers.Length);
            money += income;

            uiCanvas.GetComponent<CanvasScript>().setAttackPhase(false);
            uiCanvas.GetComponent<CanvasScript>().updateWave(wave);
            uiCanvas.GetComponent<CanvasScript>().updateMoney(money);
            uiCanvas.GetComponent<CanvasScript>().updateTowers(buildableTowers);
            setGameSpeed(1);

            //update spawnpoint
            bool doUntil = false;

            if (wave < 6) { //11 kumitettu

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
        uiCanvas.transform.GetChild(6).gameObject.GetComponent<EnemyHPIndicator>().show(hp, maxhp, name);
    }

    void CalculateActiveMinionsOnFieldInvokeRepeating() {
        if (gamePhase.Equals("Attack")) {

            GameObject[] minions = GameObject.FindGameObjectsWithTag("Enemy");
            //activeMinionsOnField = GameObject.FindGameObjectsWithTag("Enemy").Length;
            activeMinionsOnField = minions.Length;

            //remove finished minions
            foreach (GameObject minion in minions) {
                if (minion.GetComponent<EnemyScript>().hasMinionFinished()) {
                    activeMinionsOnField--;
                }
            }
        }
    }

    public static void messageToUI(string message) {
        uiCanvas.transform.GetChild(5).gameObject.GetComponent<TextAnnouncer>().startAnnounce(message);
    }

    public static void messageToUIAlert(string message) {
        uiCanvas.transform.GetChild(5).gameObject.GetComponent<TextAnnouncer>().startAnnounceAlert(message);
    }

    public void setGameSpeed(float speed){
        desiredgamespeed = speed;
        uiCanvas.GetComponent<CanvasScript>().setSelectedButton(speed);
    }

    public void checkDesiredGameSpeed(){
        if(desiredgamespeed > Time.timeScale + 0.05f){
            Time.timeScale += 0.05f;
        }
        else if(desiredgamespeed < Time.timeScale - 0.05f){
            Time.timeScale -= 0.05f;
        }
    }

    public static float getDesiredGameSpeed(){
        return desiredgamespeed;
    }

    public void resumeGame(){
        Time.timeScale = desiredgamespeed;
        uiCanvas.GetComponent<CanvasScript>().enablePauseObjects(false);
        pauseToggle = !pauseToggle;
    }

    private void playAudio(AudioClip aud, float pitch) {
        GetComponent<AudioSource>().clip = aud;
        GetComponent<AudioSource>().pitch = pitch;
        GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("SFXvolume", 0.5f) /2f;
        GetComponent<AudioSource>().Play();
    }

    public static void calculateIncomePerTurn() {
        GameObject[] towers = GameObject.FindGameObjectsWithTag("Tower");
        int income = 0;
        foreach (GameObject tower in towers) {
            income += tower.GetComponent<TowerScript>().getMoneyProduction();
        }
        Debug.Log("Towers found : " + towers.Length);
        moneyPerTurn = income + 1;
        uiCanvas.GetComponent<CanvasScript>().updateMoney(money);
    }

    public static void increaseExp(int x){
        exp += x;
        kills++;
    }
    public static int getExp(){
        return exp;
    }
    public static int getKills(){
        return kills;
    }
}

public class EnemySpawnData {

    private int enemyType;  // 1=speedfly  2=ladybug   3=beetle  4=elite speedf  5=elite ladybug  6=elite beetle
    private int spawnNumber;

    public EnemySpawnData(int wave) {

        //determine spawnpoint
        int maxSpawn = wave;
        if( maxSpawn > 5 ) { //5 is max number of spawns, change if increase spawnpoints
            maxSpawn = 5;
        }
        spawnNumber = Random.Range(0, maxSpawn);

        //determine enemy
        int maxEnemyMod = wave;
        if (maxEnemyMod > 80) {
            maxEnemyMod = 80;
        }
        int enemyRoll = Random.Range(1, 20 + maxEnemyMod);
        if (enemyRoll >= 1 && enemyRoll <= 9) {
            enemyType = 1;
        }
        else if (enemyRoll >= 10 && enemyRoll <= 20) {
            enemyType = 2;
        }
        else if (enemyRoll >= 10 && enemyRoll <= 100) {
            enemyType = 3;
        }
    }

    public int getSpwnPoint() {
        return spawnNumber;
    }

    public int getEnemyNumber() {
        return enemyType;
    }
}
