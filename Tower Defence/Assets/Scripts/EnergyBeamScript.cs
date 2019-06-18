using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBeamScript : MonoBehaviour
{
    public GameObject target;
    public Transform targetPos;

    float duration;
    float distance;

    // Start is called before the first frame update
    void Start()
    {
        duration = 100;
        GetComponent<LineRenderer>().SetPosition(0, transform.position);
        //GetComponent<LineRenderer>().SetPosition(1, transform.position +new Vector3(0,20,0));

        //line = GetComponent<LineRenderer>();
        //LineRenderer line;
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null) {
            distance = Vector3.Distance(transform.position, target.transform.position);
        }

        GetComponent<LineRenderer>().material.mainTextureScale = new Vector2(0.1f * distance, 1);
        GetComponent<LineRenderer>().material.mainTextureOffset = new Vector2(-3f * Time.time, 1);

        //line.material.mainTextureOffset = new Vector2(Time.time * -5f, 0);    //olisi pitänyt toimia
        //line.material.mainTextureScale = new Vector2(0.3f, 1f);


        //jos target on yhä elossa, päivitä location targettiin
        if (target != null) {
            GetComponent<LineRenderer>().SetPosition(1, target.transform.position + new Vector3(0, 1, 0));
        }

        //death timer
        duration -= 50 * Time.deltaTime;
        if ( duration < 0) {
            Destroy(gameObject);
        }
    }

    public void endPosition(GameObject endPos) {
        target = endPos;
        targetPos = endPos.transform; //tallennetaan tieto myös positiosta joka ei muutu jos gameobject katoaa
        GetComponent<LineRenderer>().SetPosition(1, targetPos.position + new Vector3(0, 1, 0));
    }
}
