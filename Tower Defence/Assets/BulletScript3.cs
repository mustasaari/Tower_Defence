using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript3 : MonoBehaviour
{

    private Transform target;
    private GameObject targer;
    public float speed;

    float x;
    float y;
    float z;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Debug.Log("X " + x + " Y " + y + " Z " + z);

        //transform.LookAt(new Vector3(x,y,z));
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(x, y, z), speed * Time.deltaTime);


        if (transform.position == new Vector3(x,y,z)) {
            Destroy(gameObject);
        }
    }

    public void setTarget(GameObject targ) {
        target = targ.transform;
        x = targ.transform.position.x;
        y = targ.transform.position.y;
        z = targ.transform.position.z;

        //transform.LookAt(target.position);

    }
}
