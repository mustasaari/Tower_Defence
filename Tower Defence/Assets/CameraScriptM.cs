using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScriptM : MonoBehaviour
{

    private float yaw = 0.0f;
    private float pitch = 0.0f;
    // Start is called before the first frame update
    private float startHeight;

    void Start()
    {
        startHeight = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1)) {
            Cursor.lockState = CursorLockMode.Locked;
        }
        if (Input.GetMouseButtonUp(1)) {
            Cursor.lockState = CursorLockMode.None;
        }

        if (Input.GetMouseButton(1)) {
            yaw += 3f * Input.GetAxis("Mouse X");
            pitch -= 3f * Input.GetAxis("Mouse Y");
            transform.eulerAngles = new Vector3(0f, yaw, 0.0f);
        }


        transform.Translate(Vector3.right * Input.GetAxis("Horizontal"));
        transform.Translate(Vector3.forward *Input.GetAxis("Vertical"));
        transform.Translate(Vector3.down * Input.mouseScrollDelta *2f);

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
        if (transform.position.y >90) {
            transform.position = new Vector3(transform.position.x, 90, transform.position.z);
        }
        if (transform.position.y < 10) {
            transform.position = new Vector3(transform.position.x, 10, transform.position.z);
        }

        this.gameObject.transform.GetChild(0).gameObject.transform.eulerAngles = new Vector3(55 + (transform.position.y - startHeight) / 1.5f, yaw, 0.0f);
    }
}
