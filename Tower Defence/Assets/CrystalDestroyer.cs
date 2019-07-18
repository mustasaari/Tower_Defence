using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalDestroyer : MonoBehaviour
{

    public GameObject crystalDebris;
    public GameObject blueSmoke;

    public int delay;

    bool todestroy = false;
    float countdown = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (todestroy && countdown <= 0) {
            Instantiate(crystalDebris, transform.position, transform.rotation);
            Instantiate(blueSmoke, transform.position + new Vector3(0,-10,+10), transform.rotation);
            Destroy(gameObject);
        }
        else {
            countdown = countdown - 1 * Time.deltaTime * 60;
        }
    }

    public void destroyCrystal() {
        todestroy = true;
        countdown = delay;
    }
}
