using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotMachineScript : MonoBehaviour
{
    GameObject towerToBeEdited;

    public GameObject towerDestroyAnimation;

    int amountOfWheelSymbolsInGame = 6;     // 1damage 2range 3speed 4money  5AoE   6Poison
    int[] wheels = new int[3];
    bool[] lockedWheels;
    bool cameraEnabled;

    float attackToSend;
    float rangeToSend ;
    float attackSpeedToSend ;
    int moneyToSend;
    int aoeToSend;
    int critToSend;
    int poisonToSend;

    int rollCost;

    // Start is called before the first frame update
    void Start()
    {
        cameraEnabled = false;
        GetComponentInChildren<Camera>().enabled = true;
        rollCost = 1;
        //checkRollCost();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) {
            rollTheWheel();
        }

        Vector3 newDir = new Vector3(0, 0, 0);
        if (cameraEnabled) {
            newDir = Vector3.RotateTowards(transform.GetChild(4).gameObject.transform.forward, Vector3.forward, 0.06f, 0.06f);
            transform.GetChild(4).gameObject.transform.rotation = Quaternion.LookRotation(newDir);
        }
        else {
            newDir = Vector3.RotateTowards(transform.GetChild(4).gameObject.transform.forward, Vector3.left, 0.06f, 0.06f);
            transform.GetChild(4).gameObject.transform.rotation = Quaternion.LookRotation(newDir);
        }

        if (transform.GetChild(1).gameObject.transform.GetComponent<SlotWheelScript>().getRotationReady() &&
            transform.GetChild(2).gameObject.transform.GetComponent<SlotWheelScript>().getRotationReady() &&
            transform.GetChild(3).gameObject.transform.GetComponent<SlotWheelScript>().getRotationReady()) {
            updateUI();
        }
    }

    public void openSlotMachine(GameObject tower) {
        //Debug.Log("Slotmachine opens");
        towerToBeEdited = tower;
        //Debug.Log("Tower : " + towerToBeEdited);
        //GetComponentInChildren<Camera>().enabled = true;
        setTargetingCrystalSelectorToPlace();
        //enable left side return camera
        transform.GetChild(8).GetComponent<Camera>().enabled = true;

        wheels = tower.GetComponent<TowerScript>().getWheels();
        transform.GetChild(1).gameObject.GetComponent<SlotWheelScript>().setSymbolNow(wheels[0]);
        transform.GetChild(2).gameObject.GetComponent<SlotWheelScript>().setSymbolNow(wheels[1]);
        transform.GetChild(3).gameObject.GetComponent<SlotWheelScript>().setSymbolNow(wheels[2]);

        //get lockedwheels data
        lockedWheels = tower.GetComponent<TowerScript>().getLockedWheels();
        //should set lock gaphics to correspond
        transform.GetChild(10).gameObject.GetComponent<WheelLockScript>().setLock(lockedWheels[0]);
        transform.GetChild(11).gameObject.GetComponent<WheelLockScript>().setLock(lockedWheels[1]);
        transform.GetChild(12).gameObject.GetComponent<WheelLockScript>().setLock(lockedWheels[2]);

        checkRollCost();
        applyResults();

        cameraEnabled = true;

        //send zoom command to camera
        GameObject camera = GameObject.FindGameObjectWithTag("CameraRig");
        camera.GetComponent<CameraScriptM>().towerZoomIn(towerToBeEdited);

        //tell instruction component that user has opened slot machine
        GameObject.FindGameObjectWithTag("Instructions").GetComponent<InstructionsScript>().userHasOpenedSlotmachine();
    }

    private void OnMouseOver() {
        //Debug.Log("Mouse on slotmachine");
        if (Input.GetMouseButtonDown(0)) {
            //towerToBeEdited.GetComponent<TowerScript>().setRange(40);
            closeSlotMachine();
        }
    }

    public void closeSlotMachine() {
        //GetComponentInChildren<Camera>().enabled = false;
        cameraEnabled = false;

        //close left-side return-camera
        transform.GetChild(8).GetComponent<Camera>().enabled = false;

        //disable came tower zoom
        GameObject camera = GameObject.FindGameObjectWithTag("CameraRig");
        camera.GetComponent<CameraScriptM>().towerZoomOut();

        //Tell instructions component that user has closed slotmachine
        GameObject.FindGameObjectWithTag("Instructions").GetComponent<InstructionsScript>().userHasClosedSlotmachine();
    }

    public void rollAll() {

        GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("SFXvolume", 0.5f) /1.5f;
        GetComponent<AudioSource>().Play();

        for (int i = 0; i < 3; i++) {   //set wheels to random values

            if (lockedWheels[i] == false) {
                wheels[i] = Random.Range(1, amountOfWheelSymbolsInGame + 1);     // 1dmf 2range 3speed 4money 5aoe 6poison    and +1 because rnd cant get that far
                transform.GetChild(i + 1).gameObject.GetComponent<SlotWheelScript>().startSpin(wheels[i]);
                GameObject.FindGameObjectWithTag("SlotMachineArm").GetComponent<Animator>().Play("SlotArmAnimation", 0, 0.0f);
            }
        }

        //transform.GetChild(1).gameObject.GetComponent<SlotWheelScript>().startSpin(wheels[]);
        //transform.GetChild(2).gameObject.GetComponent<SlotWheelScript>().startSpin();
        //transform.GetChild(3).gameObject.GetComponent<SlotWheelScript>().startSpin();

        Debug.Log("Slot machine result : " + wheels[0] + " " + wheels[1] + " " + wheels[2]);
        //towerToBeEdited.GetComponent<TowerScript>().setAttackDMG(10); //reset dmg to base
        applyResults();
        GameManagerScript.reduceMoney(rollCost);

        //Tell instructions component that user has rolled slotmachine
        GameObject.FindGameObjectWithTag("Instructions").GetComponent<InstructionsScript>().userHasRolledSlotmachine();
    }

    public void applyResults() {
        attackToSend = 10;
        rangeToSend = 10;
        attackSpeedToSend = 10;
        moneyToSend = 0;
        aoeToSend = 0;
        critToSend = 0;
        poisonToSend = 0;

        //wheels[0] = 5; //set wheels manually for testing
        //wheels[1] = 4;
        //wheels[2] = 4;

        if ( towerToBeEdited != null) {

            for (int i = 0; i <= 2; i++) {
                if (wheels[i] == 1) {   //   1 == ATTACK DMG
                    attackToSend = attackToSend * 2f;
                }
                if (wheels[i] == 2) {   //   2 == RANGE
                    rangeToSend = rangeToSend * 2f;
                    if (critToSend == 0) {
                        critToSend = 10;
                    }
                    else if (critToSend == 10) {
                        critToSend = 30;
                    }
                    else if(critToSend == 30) {
                        critToSend = 90;
                    }
                }
                if (wheels[i] == 3) {   //   3 == SPEED
                    attackSpeedToSend = attackSpeedToSend * 2f;
                }
                if (wheels[i] == 4) {   //  4 == money
                    if (moneyToSend == 0) {     //money level 1
                        moneyToSend = 1;
                    }
                    else if (moneyToSend == 1) {    //money level 2
                        moneyToSend = 3;
                    }
                    else if(moneyToSend == 3) {     //money level 3
                        moneyToSend = 6;
                    }
                }
                if (wheels[i] == 5) {
                    aoeToSend += 10;
                }
                if (wheels[i] == 6) {
                    if (poisonToSend == 0) {
                        poisonToSend = 1;
                    }
                    else if (poisonToSend == 1) {
                        poisonToSend = 3;
                    }
                    else if (poisonToSend == 3) {
                        poisonToSend = 6;
                    }
                }
            }

            //send to tower
            towerToBeEdited.GetComponent<TowerScript>().setAttackDMG((int)attackToSend);   // float to int
            towerToBeEdited.GetComponent<TowerScript>().setRange(rangeToSend);
            towerToBeEdited.GetComponent<TowerScript>().setAttackSpeedBonus(attackSpeedToSend);
            towerToBeEdited.GetComponent<TowerScript>().setMoneyProduction(moneyToSend);
            towerToBeEdited.GetComponent<TowerScript>().setWheels(wheels);
            towerToBeEdited.GetComponent<TowerScript>().setAoERadius(aoeToSend);
            towerToBeEdited.GetComponent<TowerScript>().setCritical(critToSend);
            towerToBeEdited.GetComponent<TowerScript>().setPoison(poisonToSend);

            //update ui removed to own method

        }
    }

    public void firstTimeRandomization(GameObject towerx) {  //Full of fail

        //Debug.Log("FTR start");
        int[] wheelRND = new int[3];
        wheelRND[0] = 4;
        wheelRND[1] = 2;
        wheelRND[2] = 3;

        towerToBeEdited = towerx;

        /*
        for (int i = 0; i < 3; i++) {   //set wheels to random values
            wheels[i] = Random.Range(1, amountOfWheelSymbolsInGame + 1);     // 1dmf 2range 3speed 4money    and +1 because rnd cant get that far
        }*/

        for (int i = 0; i < 3; i++) {   //set wheels to random values
            wheelRND[i] = Random.Range(1, amountOfWheelSymbolsInGame + 1);     // 1dmf 2range 3speed 4money    and +1 because rnd cant get that far
        }


        //Debug.Log("Slot machine result : " + wheels[0] + " " + wheels[1] + " " + wheels[2]);
        //Debug.Log("Slot machine result 2 : " + wheelRND[0] + " " + wheelRND[1] + " " + wheelRND[2]);

        towerToBeEdited.GetComponent<TowerScript>().setWheels(wheelRND);
        wheels = wheelRND;
        applyResults();
        //towerToBeEdited.GetComponent<TowerScript>().setRange(20);
        //towerToBeEdited.GetComponent<TowerScript>().setAttackSpeedBonus(20);
        //towerToBeEdited.GetComponent<TowerScript>().setMoneyProduction(1);
    }

    public void updateUI() {
        GetComponentInChildren<TextMesh>().text = "Damage : " + (int)attackToSend + "\nSpeed : " + (int)attackSpeedToSend + "\nRange : " + (int)rangeToSend + "\nMoney per wave : " + moneyToSend +"\nAoE diameter : " +aoeToSend +"\nCritical : " +critToSend +"\n poison : " +poisonToSend;
    }

    public void deleteTower() {
        if (GameManagerScript.getMoney() >=  1) {
            GameManagerScript.reduceMoney(1);
            Instantiate(towerDestroyAnimation, towerToBeEdited.transform.position, towerToBeEdited.transform.rotation);
            towerToBeEdited.gameObject.GetComponentInParent<TileScript>().setStatus("Free");
            GameManagerScript.addTowers();
            Destroy(towerToBeEdited);
            closeSlotMachine();
        }
        else {
            GameManagerScript.messageToUI("Not enough resources");
        }
    }

    public void lockPressed(int lck) {
        Debug.Log("Lock " + lck + " pressed");
        if (lockedWheels[lck] == false ) {
            lockedWheels[lck] = true;
        }
        else if (lockedWheels[lck] == true) {
            lockedWheels[lck] = false;
        }
        checkRollCost();
    }

    public void rollTheWheel() {
        if (GameManagerScript.getMoney() >= rollCost) {
            Debug.Log("R key was pressed for reroll.");
            if (transform.GetChild(1).gameObject.transform.GetComponent<SlotWheelScript>().getRotationReady() &&
                transform.GetChild(2).gameObject.transform.GetComponent<SlotWheelScript>().getRotationReady() &&
                transform.GetChild(3).gameObject.transform.GetComponent<SlotWheelScript>().getRotationReady()) {

                //if all wheels are locked then cannot roll
                if(!(lockedWheels[0] && lockedWheels[1] && lockedWheels[2])) {
                    rollAll();
                }
            }
        }
        else {
            GameManagerScript.messageToUIAlert("Not enough resources");
        }
    }

    public void checkRollCost() {

        int numberOfLockedWheels = 0;

        for (int i = 0; i < 3; i++) {
            if (lockedWheels[i] == true) {
                numberOfLockedWheels++;
            }
        }

        if (numberOfLockedWheels == 0) {
            transform.GetChild(14).GetComponent<TextMesh>().text = "Roll cost : 1";
            rollCost = 1;
        }
        else if (numberOfLockedWheels == 1) {
            transform.GetChild(14).GetComponent<TextMesh>().text = "Roll cost : 2";
            rollCost = 2;
        }
        else if (numberOfLockedWheels == 2) {
            transform.GetChild(14).GetComponent<TextMesh>().text = "Roll cost : 3";
            rollCost = 3;
        }
        else if (numberOfLockedWheels == 3) {
            transform.GetChild(14).GetComponent<TextMesh>().text = "All Wheels Locked";
        }
  
    }

    public void setTargetingFromCrystal(string mode) {
        towerToBeEdited.GetComponent<TowerScript>().setTargetingMode(mode);
    }

    public void setTargetingCrystalSelectorToPlace() {
        if (towerToBeEdited.GetComponent<TowerScript>().getTargetingMode().Equals("Default")) {
            GameObject.FindGameObjectWithTag("TargetingCrystalSelector").transform.position = transform.GetChild(0).GetChild(0).position;
        }
        if (towerToBeEdited.GetComponent<TowerScript>().getTargetingMode().Equals("HighestHP")) {
            GameObject.FindGameObjectWithTag("TargetingCrystalSelector").transform.position = transform.GetChild(0).GetChild(1).position;
        }
        if (towerToBeEdited.GetComponent<TowerScript>().getTargetingMode().Equals("LowestHP")) {
            GameObject.FindGameObjectWithTag("TargetingCrystalSelector").transform.position = transform.GetChild(0).GetChild(2).position;
        }
        if (towerToBeEdited.GetComponent<TowerScript>().getTargetingMode().Equals("Farthest")) {
            GameObject.FindGameObjectWithTag("TargetingCrystalSelector").transform.position = transform.GetChild(0).GetChild(3).position;
        }
        if (towerToBeEdited.GetComponent<TowerScript>().getTargetingMode().Equals("Nearest")) {
            GameObject.FindGameObjectWithTag("TargetingCrystalSelector").transform.position = transform.GetChild(0).GetChild(4).position;
        }
    }
}
