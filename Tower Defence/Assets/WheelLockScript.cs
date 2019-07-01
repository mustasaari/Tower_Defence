using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelLockScript : MonoBehaviour
{

    public int lockNumber;
    public bool wheelLocked;

    float startPosY;

    private GameObject lockPart2;

    // Start is called before the first frame update
    void Start()
    {
        wheelLocked = false;
        lockPart2 = transform.GetChild(0).gameObject;
        startPosY = lockPart2.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (wheelLocked && lockPart2.transform.position.y >= startPosY -0.5f) { //Down
            lockPart2.transform.Translate(Vector3.up * Time.deltaTime * -20f, Space.World);
        } 
        else if (!wheelLocked && lockPart2.transform.position.y <= startPosY +2f) {  //up
            lockPart2.transform.Translate(Vector3.up * Time.deltaTime * +20f, Space.World);
        }

        else if (!wheelLocked && lockPart2.transform.position.y >= startPosY + 2.2f) {
            lockPart2.transform.Translate(Vector3.up * Time.deltaTime * -5f, Space.World);
        }
    }

    private void OnMouseOver() {
        if(Input.GetMouseButtonDown(0)) {
            GetComponentInParent<SlotMachineScript>().lockPressed(lockNumber);
            GetComponent<AudioSource>().Play();
            if (wheelLocked) {
                wheelLocked = false;
            }
            else {
                wheelLocked = true;
            }
        }
    }

    public void setLock(bool wheel) {
        wheelLocked = wheel;
    }
}
