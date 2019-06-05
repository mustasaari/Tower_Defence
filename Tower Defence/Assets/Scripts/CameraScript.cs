using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private float speedMod = 10.0f;//a speed modifier
    private Vector3 point;//the coord to the point where the camera looks at
   
    void Start ()
    {
    point = new Vector3(0,0,-10);//get target's coords
    transform.LookAt(point);//makes the camera look to it
    }
   
    void Update ()
    {   
        if(Input.GetKey("right")){
            rotateRight();
        }
        if(Input.GetKey("left")){
            rotateLeft();
        }
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
