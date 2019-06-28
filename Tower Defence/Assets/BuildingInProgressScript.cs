using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingInProgressScript : MonoBehaviour
{

    float size;

    // Start is called before the first frame update
    void Start()
    {
        size = 0f;

        for (int i = 0; i < transform.childCount; i++) {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

        //if (size > 0) {
         //   size -= Time.deltaTime * 45f;
        //}

        for (int i = 0; i < transform.childCount; i++) {
            if (size > i) {
                transform.GetChild(i).gameObject.SetActive(true);
            }
        }

        transform.GetChild(5).position = new Vector3(transform.GetChild(5).position.x, size/5.3f, transform.GetChild(5).position.z);

        //transform.localScale = new Vector3(3, size / 10, 3);
    }

    public void increase() {
        size += Time.deltaTime * 80;
    }

    public void setPosition(Transform pos) {
        transform.position = pos.position;
    }

    public void setSizeToZero() {
        size = 0;
        for (int i = 0; i < transform.childCount; i++) {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}
