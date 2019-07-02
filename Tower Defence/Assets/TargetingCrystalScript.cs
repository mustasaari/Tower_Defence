using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetingCrystalScript : MonoBehaviour
{

    public string setTargetingTo;
    public string toolTipText;

    private string oldInfoStatusText;

    GameObject infoDisplay;

    // Start is called before the first frame update
    void Start()
    {
        infoDisplay = GameObject.FindGameObjectWithTag("RollCostInfoDisplay");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMouseOver() {
        if (Input.GetMouseButtonDown(0)) {
            GameObject.FindGameObjectWithTag("SlotMachine").GetComponent<SlotMachineScript>().setTargetingFromCrystal(setTargetingTo);
            GameObject.FindGameObjectWithTag("TargetingCrystalSelector").transform.position = transform.position;
        }
    }

    public void OnMouseEnter() {
        oldInfoStatusText = infoDisplay.GetComponent<TextMesh>().text;
        infoDisplay.GetComponent<TextMesh>().text = toolTipText;
    }

    public void OnMouseExit() {
        infoDisplay.GetComponent<TextMesh>().text = oldInfoStatusText;
    }
}
