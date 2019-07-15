using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouteSpriteScript : MonoBehaviour
{

    public string direction = "Down";

    float startTime;

    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Quaternion.LookRotation(Vector3.back);

        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        float rotSpeed = 15f;
        float speed = 60f;

        if (Physics.Raycast(transform.position + new Vector3(0, 0.1f, 0), transform.TransformDirection(Vector3.down), out hit, 10f)) {
            if (hit.transform.gameObject.tag == "GridTile") {
                direction = hit.transform.gameObject.GetComponent<TileScript>().getEnemyDirection();
                //Debug.Log("Did Hit " +hit.transform.gameObject.name +"  " +direction);
            }
        }

        transform.Translate(Vector3.forward * Time.deltaTime * speed);
        Vector3 newDir = new Vector3(0, 0, 0);

        if (direction.Equals("Down")) {
            newDir = Vector3.RotateTowards(transform.forward, Vector3.back, rotSpeed * Time.deltaTime, rotSpeed * Time.deltaTime);
        }
        if (direction.Equals("Up")) {
            newDir = Vector3.RotateTowards(transform.forward, Vector3.forward, rotSpeed * Time.deltaTime, rotSpeed * Time.deltaTime);
        }
        if (direction.Equals("Left")) {
            newDir = Vector3.RotateTowards(transform.forward, Vector3.left, rotSpeed * Time.deltaTime, rotSpeed * Time.deltaTime);
        }
        if (direction.Equals("Right")) {
            newDir = Vector3.RotateTowards(transform.forward, Vector3.right, rotSpeed * Time.deltaTime, rotSpeed * Time.deltaTime);
        }

        transform.rotation = Quaternion.LookRotation(newDir);

        if (startTime + 10 < Time.time) {
            Destroy(gameObject);
        }
    }
}
