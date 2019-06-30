using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotWheelScript : MonoBehaviour
{

    public float wheelSpeed;

    float remainingRotation;

    int myFutureSymbol;

    bool rotationReady;

    // Start is called before the first frame update
    void Start()
    {
        rotationReady = true;
        remainingRotation = 0;
        myFutureSymbol = 2000;
    }

    // Update is called once per frame
    void Update()
    {

        float rotation = wheelSpeed * Time.deltaTime * 75f;

        if (remainingRotation > rotation && rotationReady == false) {
            remainingRotation -= rotation;
            //remainingRotation -= wheelSpeed;
            transform.Rotate(0, 0, -rotation, Space.Self);
            //transform.Rotate(0, 0, -wheelSpeed, Space.Self);
        }
        else {
            if (!rotationReady) {
                GetComponent<AudioSource>().Play();
            }

            rotationReady = true;
        }

        if (remainingRotation < 180 && myFutureSymbol < 1000) {

            int childCount = transform.GetChild(0).gameObject.transform.childCount;

            for (int i = 0; i < childCount; i++) {
                transform.GetChild(0).gameObject.transform.GetChild(i).gameObject.SetActive(false);
            }

            //Debug.Log("MyFutureSymbol  : " + myFutureSymbol);
            transform.GetChild(0).gameObject.transform.GetChild(myFutureSymbol -1).gameObject.SetActive(true);
            myFutureSymbol = 2000;
        }
    }

    public void startSpin(int symbol) {
        remainingRotation += 360;
        myFutureSymbol = symbol;
        rotationReady = false;
    }

    //set symbols instantly when existing tower is opened
    public void setSymbolNow(int symbol) {

        //Debug.Log("SYMBOL " + symbol);

        //HIDE SYMBOLS ALL
        int childCount = transform.GetChild(0).gameObject.transform.childCount;
        for (int i = 0; i < childCount; i++) {
            transform.GetChild(0).gameObject.transform.GetChild(i).gameObject.SetActive(false);
        }

        transform.GetChild(0).gameObject.transform.GetChild(symbol -1).gameObject.SetActive(true);
    }

    public bool getRotationReady() {
        return rotationReady;
    }
}
