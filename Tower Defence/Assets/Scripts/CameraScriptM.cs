using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScriptM : MonoBehaviour
{

    public float angle;

    private float yaw = 0.0f;
    private float pitch = 0.0f;
    // Start is called before the first frame update
    private float startHeight;
    string cameraMode;

    GameObject towerZoomGameObject;
    float zoomYaw;
    float zoomDistance;

    Vector3 positionBeforeZoom;
    Quaternion rotationBeforeZoom;

    void Start()
    {
        cameraMode = "User";
        startHeight = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (cameraMode.Equals("User")) {

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


            transform.Translate(Vector3.right * Input.GetAxis("Horizontal") * Time.deltaTime * 80f);
            transform.Translate(Vector3.forward * Input.GetAxis("Vertical") * Time.deltaTime * 80f);
            transform.Translate(Vector3.down * Input.mouseScrollDelta * 2f * Time.deltaTime * 80f);

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

            this.gameObject.transform.GetChild(0).gameObject.transform.eulerAngles = new Vector3(angle + (transform.position.y - startHeight) / 1.5f, yaw, 0.0f);
        }
        //Tower Zoom
        if (cameraMode.Equals("Zoom")) {
            GameObject child = this.gameObject.transform.GetChild(0).gameObject;

            //if (Vector3.Distance(child.transform.position, towerZoomGameObject.transform.position) > 20 ) {
                transform.position = Vector3.MoveTowards(transform.position, towerZoomGameObject.transform.position +new Vector3(7,15,-15), zoomDistance/35);

            if (zoomYaw > 180 && zoomYaw < 360) {
                zoomYaw += (360 - zoomYaw) / zoomDistance * 5;
            }
            else if (zoomYaw < 180 && zoomYaw > 0) {
                zoomYaw -= (zoomYaw) / zoomDistance * 5;
            }

            transform.eulerAngles = new Vector3(0f, zoomYaw, 0.0f);
            this.gameObject.transform.GetChild(0).gameObject.transform.eulerAngles = new Vector3(angle + (transform.position.y - startHeight) / 1.5f, zoomYaw, 0.0f);
        }

        if (cameraMode.Equals("ZoomOut")) {
            transform.position = Vector3.MoveTowards(transform.position, positionBeforeZoom, zoomDistance/35);

            if (zoomYaw > yaw) {
                //zoomYaw -= 2;
                zoomYaw -= (zoomYaw - yaw) / zoomDistance * 5;
            }
            if (zoomYaw < yaw) {
                //zoomYaw += 2;
                zoomYaw += (yaw - zoomYaw) / zoomDistance * 5;
            }

            transform.eulerAngles = new Vector3(0f, zoomYaw, 0.0f);
            this.gameObject.transform.GetChild(0).gameObject.transform.eulerAngles = new Vector3(angle + (transform.position.y - startHeight) / 1.5f, zoomYaw, 0.0f);

            if (transform.position == positionBeforeZoom) {
                cameraMode = "User";
            }
        }
    }

    public void towerZoomIn(GameObject tower) {
        cameraMode = "Zoom";
        towerZoomGameObject = tower;
        //zoomYaw = yaw;
        //zoomYaw = zoomYaw % 360;
        yaw = yaw % 360;
        //Debug.Log("Yaw before : " + yaw);
        //Muuta yaw positiiviseksi
        if (yaw < 0) {
            yaw = 360 + yaw;
        }
        zoomYaw = yaw;
        //Debug.Log("Yaw after : " + yaw);
        positionBeforeZoom = transform.position;
        rotationBeforeZoom = this.gameObject.transform.GetChild(0).gameObject.transform.rotation;

        zoomDistance = Vector3.Distance(transform.position, towerZoomGameObject.transform.position);

    }

    public void towerZoomOut() {
        cameraMode = "ZoomOut";
    }
}
