using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dmgTextScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //transform.Rotate(new Vector3(0, 180, 0));
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.up * Time.deltaTime * 5);
        transform.Rotate(new Vector3(0, 2, 0));

        if (transform.position.y > 90) {
            Destroy(gameObject);
        }
    }

    public void setNumber(int nro, Color debugColor) {
        transform.GetChild(0).gameObject.GetComponent<TextMesh>().color = debugColor;
        transform.GetChild(0).gameObject.GetComponent<TextMesh>().text = nro.ToString();
    }
}
