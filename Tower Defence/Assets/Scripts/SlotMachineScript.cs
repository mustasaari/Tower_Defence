using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotMachineScript : MonoBehaviour
{
    GameObject towerToBeEdited;

    int amountOfWheelSymbolsInGame = 5;     // 1damage 2range 3speed 4money  5AoE
    int[] wheels = new int[3];
    bool cameraEnabled;

    // Start is called before the first frame update
    void Start()
    {
        cameraEnabled = false;
        GetComponentInChildren<Camera>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) {
            if(GameManagerScript.getMoney() >= 1){
                Debug.Log("R key was pressed for reroll.");
                rollAll();
            }
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
    }

    public void openSlotMachine(GameObject tower) {
        //Debug.Log("Slotmachine opens");
        towerToBeEdited = tower;
        //Debug.Log("Tower : " + towerToBeEdited);
        //GetComponentInChildren<Camera>().enabled = true;

        //enable left side return camera
        transform.GetChild(8).GetComponent<Camera>().enabled = true;

        wheels = tower.GetComponent<TowerScript>().getWheels();
        transform.GetChild(1).gameObject.GetComponent<SlotWheelScript>().setSymbolNow(wheels[0]);
        transform.GetChild(2).gameObject.GetComponent<SlotWheelScript>().setSymbolNow(wheels[1]);
        transform.GetChild(3).gameObject.GetComponent<SlotWheelScript>().setSymbolNow(wheels[2]);

        applyResults();

        cameraEnabled = true;

        //send zoom command to camera
        GameObject camera = GameObject.FindGameObjectWithTag("CameraRig");
        camera.GetComponent<CameraScriptM>().towerZoomIn(towerToBeEdited);
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
    }

    public void rollAll() {

        for (int i = 0; i < 3; i++) {   //set wheels to random values
            wheels[i] = Random.Range(1, amountOfWheelSymbolsInGame + 1);     // 1dmf 2range 3speed 4money    and +1 because rnd cant get that far
            transform.GetChild(i + 1).gameObject.GetComponent<SlotWheelScript>().startSpin(wheels[i]);
        }

        //transform.GetChild(1).gameObject.GetComponent<SlotWheelScript>().startSpin(wheels[]);
        //transform.GetChild(2).gameObject.GetComponent<SlotWheelScript>().startSpin();
        //transform.GetChild(3).gameObject.GetComponent<SlotWheelScript>().startSpin();

        Debug.Log("Slot machine result : " + wheels[0] + " " + wheels[1] + " " + wheels[2]);
        //towerToBeEdited.GetComponent<TowerScript>().setAttackDMG(10); //reset dmg to base
        applyResults();
        GameManagerScript.reduceMoney();
    }

    public void applyResults() {
        float attackToSend = 10;
        float rangeToSend = 10;
        float attackSpeedToSend = 10;
        int moneyToSend = 0;
        int aoeToSend = 0;

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
            }

            //send to tower
            towerToBeEdited.GetComponent<TowerScript>().setAttackDMG((int)attackToSend);   // float to int
            towerToBeEdited.GetComponent<TowerScript>().setRange(rangeToSend);
            towerToBeEdited.GetComponent<TowerScript>().setAttackSpeedBonus(attackSpeedToSend);
            towerToBeEdited.GetComponent<TowerScript>().setMoneyProduction(moneyToSend);
            towerToBeEdited.GetComponent<TowerScript>().setWheels(wheels);
            towerToBeEdited.GetComponent<TowerScript>().setAoERadius(aoeToSend);

            GetComponentInChildren<TextMesh>().text = "Damage : " +(int)attackToSend +"\nSpeed : " +(int)attackSpeedToSend +"\nRange : " +(int)rangeToSend +"\nMoney per wave : " +moneyToSend;

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
}
