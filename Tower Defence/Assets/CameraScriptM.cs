using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScriptM : MonoBehaviour
{

    private float yaw = 0.0f;
    private float pitch = 0.0f;
    // Start is called before the first frame update
    private float startHeight;

    bool towerZoomEnabled;
    GameObject towerZoomGameObject;

    Vector3 positionBeforeZoom;
    Quaternion rotationBeforeZoom;

    void Start()
    {
        startHeight = transform.position.y;
        towerZoomEnabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!towerZoomEnabled) {

            if (Input.GetMouseButtonDown(1)) {
                Cursor.lockState = CursorLockMode.Locked;
                TileScript.cursorActive = false;
            }
            if (Input.GetMouseButtonUp(1)) {
                Cursor.lockState = CursorLockMode.None;
                TileScript.cursorActive = true;
            }

            if (Input.GetMouseButton(1)) {
                yaw += 3f * Input.GetAxis("Mouse X");
                pitch -= 3f * Input.GetAxis("Mouse Y");
                transform.eulerAngles = new Vector3(0f, yaw, 0.0f);
            }


            transform.Translate(Vector3.right * Input.GetAxis("Horizontal"));
            transform.Translate(Vector3.forward * Input.GetAxis("Vertical"));
            transform.Translate(Vector3.down * Input.mouseScrollDelta * 2f);

            if (transform.position.x > 70) {
                transform.position = new Vector3(70, transform.position.y, transform.position.z);
            }
            if (transform.position.x < -70) {
                transform.position = new Vector3(-70, transform.position.y, transform.position.z);
            }
            if (transform.position.z > 70) {
                transform.position = new Vector3(transform.position.x, transform.position.y, 70);
            }
            if (transform.position.z < -70) {
                transform.position = new Vector3(transform.position.x, transform.position.y, -70);
            }
            if (transform.position.y > 90) {
                transform.position = new Vector3(transform.position.x, 90, transform.position.z);
            }
            if (transform.position.y < 10) {
                transform.position = new Vector3(transform.position.x, 10, transform.position.z);
            }

            this.gameObject.transform.GetChild(0).gameObject.transform.eulerAngles = new Vector3(55 + (transform.position.y - startHeight) / 1.5f, yaw, 0.0f);
        }
        //Tower Zoom
        else {
            GameObject child = this.gameObject.transform.GetChild(0).gameObject;

            if (Vector3.Distance(child.transform.position, towerZoomGameObject.transform.position) > 20 ) {
                transform.position = Vector3.MoveTowards(transform.position, towerZoomGameObject.transform.position, 1f);
            }

            //child.transform.LookAt(towerZoomGameObject.transform.position);
            //child.transform.rotation = Quaternion.RotateTowards(child.transform.rotation, towerZoomGameObject.transform.rotation, 0.2f);
            //Vector3 direction = towerZoomGameObject.transform.position;
            //Quaternion toRotation = Quaternion.FromToRotation(child.transform.forward, direction);
            //child.transform.rotation = Quaternion.Lerp(child.transform.rotation, toRotation, 1f * Time.deltaTime);

            // fast rotation
            //float rotSpeed = 360f;
            // distance between target and the actual rotating object
            //Vector3 D = towerZoomGameObject.transform.position - child.transform.position;
            // calculate the Quaternion for the rotation
            //Quaternion rot = Quaternion.Slerp(child.transform.rotation, Quaternion.LookRotation(D), rotSpeed * Time.deltaTime);
            //Apply the rotation 
            //child.transform.rotation = rot;
            // put 0 on the axys you do not want for the rotation object to rotate
            //child.transform.eulerAngles = new Vector3(0, child.transform.eulerAngles.z, 0 );
        }
    }

    public void towerZoomIn(GameObject tower) {
        towerZoomGameObject = tower;
        towerZoomEnabled = true;
        positionBeforeZoom = transform.position;
        rotationBeforeZoom = this.gameObject.transform.GetChild(0).gameObject.transform.rotation;

    }

    public void towerZoomOut() {
        towerZoomEnabled = false;
    }
}
