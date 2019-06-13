using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotMachineScript : MonoBehaviour
{
    GameObject towerToBeEdited;

    int[] wheels = new int[5];
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
            if(GameManagerScript.getMoney() >= 10){
                Debug.Log("R key was pressed for reroll.");
                rollAll();
            }
        }

        Vector3 newDir = new Vector3(0, 0, 0);
        if (cameraEnabled) {
            newDir = Vector3.RotateTowards(transform.GetChild(6).gameObject.transform.forward, Vector3.forward, 0.06f, 0.06f);
            transform.GetChild(6).gameObject.transform.rotation = Quaternion.LookRotation(newDir);
        }
        else {
            newDir = Vector3.RotateTowards(transform.GetChild(6).gameObject.transform.forward, Vector3.left, 0.06f, 0.06f);
            transform.GetChild(6).gameObject.transform.rotation = Quaternion.LookRotation(newDir);
        }
    }

    public void openSlotMachine(GameObject tower) {
        Debug.Log("Slotmachine opens");
        towerToBeEdited = tower;
        Debug.Log("Tower : " + towerToBeEdited);
        //GetComponentInChildren<Camera>().enabled = true;
        cameraEnabled = true;

        //send zoom command to camera
        GameObject camera = GameObject.FindGameObjectWithTag("CameraRig");
        camera.GetComponent<CameraScriptM>().towerZoomIn(towerToBeEdited);
    }

    private void OnMouseOver() {
        Debug.Log("Mouse on slotmachine");
        if (Input.GetMouseButtonDown(0)) {
            //towerToBeEdited.GetComponent<TowerScript>().setRange(40);
            closeSlotMachine();
        }
    }

    public void closeSlotMachine() {
        //GetComponentInChildren<Camera>().enabled = false;
        cameraEnabled = false;

        //disable came tower zoom
        GameObject camera = GameObject.FindGameObjectWithTag("CameraRig");
        camera.GetComponent<CameraScriptM>().towerZoomOut();
    }

    public void rollAll() {

        transform.GetChild(1).gameObject.GetComponent<SlotWheelScript>().startSpin();
        transform.GetChild(2).gameObject.GetComponent<SlotWheelScript>().startSpin();
        transform.GetChild(3).gameObject.GetComponent<SlotWheelScript>().startSpin();
        transform.GetChild(4).gameObject.GetComponent<SlotWheelScript>().startSpin();
        transform.GetChild(5).gameObject.GetComponent<SlotWheelScript>().startSpin();

        for (int i = 0; i < 5; i++) {   //set wheels to random values
            wheels[i] = Random.Range(1, 4);
        }
        Debug.Log("Slot machine result : " + wheels[0] + " " + wheels[1] + " " + wheels[2] + " " + wheels[3] + " " + wheels[4]);
        //towerToBeEdited.GetComponent<TowerScript>().setAttackDMG(10); //reset dmg to base
        applyResults();
        GameManagerScript.reduceMoney();
    }

    public void applyResults() {
        float attackToSend = 10;
        float rangeToSend = 10;
        float attackSpeedToSend = 10;

        if ( towerToBeEdited != null) {

            for (int i = 0; i <= 4; i++) {
                if (wheels[i] == 1) {   //   1 == ATTACK DMG
                    attackToSend = attackToSend * 1.5f;
                }
                if (wheels[i] == 2) {   //   2 == RANGE
                    rangeToSend = rangeToSend * 1.5f;
                }
                if (wheels[i] == 3) {   //   3 == SPEED
                    attackSpeedToSend = attackSpeedToSend * 1.5f;
                }
            }

            //send to tower
            towerToBeEdited.GetComponent<TowerScript>().setAttackDMG((int)attackToSend);   // float to int
            towerToBeEdited.GetComponent<TowerScript>().setRange(rangeToSend);
            towerToBeEdited.GetComponent<TowerScript>().setAttackSpeedBonus(attackSpeedToSend);

            GetComponentInChildren<TextMesh>().text = "Attack : " +(int)attackToSend +"\nSpeed : " +(int)attackSpeedToSend +"\nRange : " +(int)rangeToSend; ;

        }
    }
}
