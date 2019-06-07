using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotMachineScript : MonoBehaviour
{
    GameObject towerToBeEdited;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
            towerToBeEdited.GetComponent<TowerScript>().setRange(40);
            closeSlotMachine();
        }
    }

    public void closeSlotMachine() {
        GetComponentInChildren<Camera>().enabled = false;
    }
}
