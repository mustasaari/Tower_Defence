using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
Old cameraScript which was done by saku and was shit.
New Scripts (CameraScriptM) is in use now.
 */
public class CameraScript : MonoBehaviour
{
    private float speedMod = 10.0f;//a speed modifier
    private Vector3 point;//the starting coord to the point where the camera looks at
    public float dragSpeed = 4;
    private Vector3 dragOrigin;

    public float panSpeed = 4.0f;
    private Vector3 mouseOrigin;
    private bool isPanning;

    float ZoomAmount = 0f; //With Positive and negative values
    float MaxToClamp = 10f;
    float ROTSpeed = 40f;

    void Start ()
    {
    point = new Vector3(0,0,-10);//get target's coords
    transform.LookAt(point);//makes the camera look to it
    }
   
    void Update ()
    {   
        //Left Mouse button drag move start
        if (Input.GetMouseButtonDown (1)) 
        {
            //right click was pressed    
            mouseOrigin = Input.mousePosition;
            isPanning = true;
        }
        
        
        // cancel on button release
        if (!Input.GetMouseButton (1)) 
        {
            isPanning = false;
        }
 
        //move camera on X & Y
        if (isPanning) 
        {
            Vector3 pos = Camera.main.ScreenToViewportPoint (Input.mousePosition - mouseOrigin);
 
            // update x and z but not y
            Vector3 move = new Vector3 (0- (pos.x * panSpeed), 0, 0 - (pos.y * panSpeed));
 
            transform.Translate (move, Space.World);
        }
        //Left Mouse button drag move end

        // ZoomAmount += Input.GetAxis("Mouse ScrollWheel");
        // ZoomAmount = Mathf.Clamp(ZoomAmount, -MaxToClamp, MaxToClamp);
        // translate = Mathf.Min(Mathf.Abs(Input.GetAxis("Mouse ScrollWheel")), MaxToClamp - Mathf.Abs(ZoomAmount));
        // gameObject.transform.Translate(0,0,translate * ROTSpeed * Mathf.Sign(Input.GetAxis("Mouse ScrollWheel")));

        // if(Input.GetKey("right")){
        //     //rotateRight();
        // }
        // if(Input.GetKey("left")){
        //     //rotateLeft();
        // }
        // if (Input.GetMouseButtonDown(0))
        // {
        //     Debug.Log("Drag start");
        //     dragOrigin = Input.mousePosition;
        //     if(dragOrigin.y < Input.mousePosition.y)
        //     {
        //         Debug.Log("Left");
        //         transform.RotateAround(point,new Vector3(0.0f,1.0f,0.0f),20 * Time.deltaTime * speedMod);

        //     }
        //     if(dragOrigin.y > Input.mousePosition.y)
        //     {
        //         Debug.Log("Right");
        //         transform.RotateAround(point,new Vector3(0.0f,-1.0f,0.0f),20 * Time.deltaTime * speedMod);
        //     }
        //     //if pos y - rotate left
        //     //if pos y + rotate right
        // }
    }
    
    void rotateLeft()
    {
        transform.RotateAround(point,new Vector3(0.0f,1.0f,0.0f),20 * Time.deltaTime * speedMod);
    }

    void rotateRight()
    {
        transform.RotateAround(point,new Vector3(0.0f,-1.0f,0.0f),20 * Time.deltaTime * speedMod);
    }
}
