using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotMachineScript : MonoBehaviour
{
    GameObject towerToBeEdited;

    int[] wheels = new int[5];

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) {
            Debug.Log("R key was pressed for reroll.");
            rollAll();
        }
    }

    public void openSlotMachine(GameObject tower) {
        Debug.Log("Slotmachine opens");
        towerToBeEdited = tower;
        Debug.Log("Tower : " + towerToBeEdited);
        GetComponentInChildren<Camera>().enabled = true;
    }

    private void OnMouseOver() {
        Debug.Log("Mouse on slotmachine");
        if (Input.GetMouseButtonDown(0)) {
            //towerToBeEdited.GetComponent<TowerScript>().setRange(40);
            closeSlotMachine();
        }
    }

    public void closeSlotMachine() {
        GetComponentInChildren<Camera>().enabled = false;
    }

    public void rollAll() {

        for (int i = 0; i < 5; i++) {   //set wheels to random values
            wheels[i] = Random.Range(1, 4);
        }
        Debug.Log("Slot machine result : " + wheels[0] + " " + wheels[1] + " " + wheels[2] + " " + wheels[3] + " " + wheels[4]);
        //towerToBeEdited.GetComponent<TowerScript>().setAttackDMG(10); //reset dmg to base
        applyResults();
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
