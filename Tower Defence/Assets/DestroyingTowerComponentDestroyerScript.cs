using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyingTowerComponentDestroyerScript : MonoBehaviour
{
    // Start is called before the first frame update
    int destroyMinDelay;

    void Start()
    {
        destroyMinDelay = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(GetComponent<Rigidbody>().velocity.y == 0 && destroyMinDelay > 10) {
            Destroy(gameObject);
        }

        destroyMinDelay++;
    }
}
