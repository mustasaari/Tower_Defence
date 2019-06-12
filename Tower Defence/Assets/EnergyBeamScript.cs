using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBeamScript : MonoBehaviour
{
    public GameObject target;

    float duration;
    float distance;

    // Start is called before the first frame update
    void Start()
    {
        duration = 100;
        GetComponent<LineRenderer>().SetPosition(0, transform.position);
        GetComponent<LineRenderer>().SetPosition(1, transform.position +new Vector3(10,10,-20));
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(transform.position, target.transform.position);
        GetComponent<LineRenderer>().material.mainTextureScale = new Vector2(distance * 2, 1);
        GetComponent<LineRenderer>().material.mainTextureOffset = new Vector2(-1 * Time.time, 1);

        GetComponent<LineRenderer>().SetPosition(1, target.transform.position - transform.position);

        duration -= 1 * Time.deltaTime;

        if ( duration < 0) {
            Destroy(gameObject);
        }
    }

    public void endPosition(GameObject endPos) {

    }
}
