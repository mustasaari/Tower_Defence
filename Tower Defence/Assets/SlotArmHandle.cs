using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotArmHandle : MonoBehaviour
{

    private GameObject slotmachine;

    // Start is called before the first frame update
    void Start()
    {
        slotmachine = GameObject.FindWithTag("SlotMachine");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseOver() {
        if (Input.GetMouseButtonDown(0)) {
            slotmachine.GetComponent<SlotMachineScript>().rollTheWheel();
            GameObject.FindGameObjectWithTag("RollCostInfoDisplay").GetComponent<RollCostScript>().bounce();
        }
    }
}
