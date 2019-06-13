using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotWheelScript : MonoBehaviour
{

    public float wheelSpeed;

    float remainingRotation;

    int myFutureSymbol;

    // Start is called before the first frame update
    void Start()
    {
        remainingRotation = 0;
    }

    // Update is called once per frame
    void Update()
    {

        float rotation = wheelSpeed * Time.deltaTime;

        if (remainingRotation > rotation) {
            remainingRotation -= wheelSpeed;
            transform.Rotate(0, 0, -wheelSpeed, Space.Self);
        }

        if (remainingRotation < 180 && myFutureSymbol < 1000) {

            int childCount = transform.GetChild(0).gameObject.transform.childCount;

            for (int i = 0; i < childCount; i++) {
                transform.GetChild(0).gameObject.transform.GetChild(i).gameObject.SetActive(false);
            }

            Debug.Log("MyFutureSymbol  : " + myFutureSymbol);
            transform.GetChild(0).gameObject.transform.GetChild(myFutureSymbol -1).gameObject.SetActive(true);
            myFutureSymbol = 2000;
        }
    }

    public void startSpin(int symbol) {
        remainingRotation += 360;
        myFutureSymbol = symbol;
    }
}
