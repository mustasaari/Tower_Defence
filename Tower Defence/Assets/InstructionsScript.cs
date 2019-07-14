using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstructionsScript : MonoBehaviour
{

    int instructionsPhase = 1;

    bool hide = false;

    float transAlpha;
    float height = 1.5f;

    float wave;

    bool slotmachineOpened = false;
    bool slotmachineRolled = false;
    bool slotMachineClosed = false;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("calculateThings", 2, 1);
        transAlpha = 1;
    }

    // Update is called once per frame
    void Update()
    {
        wave += Time.deltaTime;
        //transAlpha = (Mathf.Sin(2f * wave) + 1.5f) / 1.5f;

        //transAlpha = (Mathf.Sin(2f * wave) + height) / 2f;

        //GetComponent<Text>().text = transAlpha.ToString();

        if (hide && height > -3 ) {
            height -= 0.05f;
        }
        else if (!hide && height < 1.5f) {
            height += 0.05f;
        }

        GetComponent<Text>().color = new Color(1, 1, 1, transAlpha + height);
    }

    void calculateThings() {

        if (GameManagerScript.getGamePhase().Equals("Attack")) {
            instructionsPhase = 100;
            hide = true;
        }

        if (instructionsPhase == 1) {
            int towers = GameObject.FindGameObjectsWithTag("Tower").Length;
            if (towers > 0) {
                instructionsPhase = 2;
                hide = true;
            }
        }
        else if (instructionsPhase < 5) {
            instructionsPhase++;
        }
        else if (instructionsPhase == 5) {
            if (!slotmachineOpened) {
                hide = false;
                GetComponent<Text>().text = "Click on towers to edit their abilities";
            }
            else {
                instructionsPhase++;
                hide = true;
            }
        }
        else if (instructionsPhase < 7) {
            instructionsPhase++;
        }
        else if (instructionsPhase == 7) {
            if (!slotmachineRolled && !slotMachineClosed) {
                hide = false;
                GetComponent<Text>().text = "Pull red lever to randomize towers properties. \n Zoom back to normal view by clicking on the tower again.";
            }
            else {
                hide = true;
                instructionsPhase++;
            }
        }
        else if (instructionsPhase < 8) {
            instructionsPhase++;
        }
        else if (instructionsPhase >= 8 && instructionsPhase <= 16) {
            if (slotMachineClosed) {
                hide = false;
                GetComponent<Text>().text = "When you are ready with building click \"Launch\" on the left to start the attack";
                instructionsPhase++;
            }
        }
        else if (instructionsPhase == 17) {
            hide = true;
        }
    }

    public void userHasOpenedSlotmachine() {
        slotmachineOpened = true;
    }

    public void userHasRolledSlotmachine() {
        slotmachineRolled = true;
    }

    public void userHasClosedSlotmachine() {
        slotMachineClosed = true;
    }
}
