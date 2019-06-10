using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotWheelScript : MonoBehaviour
{

    public float wheelSpeed;

    float remainingRotation;

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
    }

    public void startSpin() {
        remainingRotation += 360;
    }
}
