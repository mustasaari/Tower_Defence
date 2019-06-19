using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Digger : MonoBehaviour
{

    bool activated = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (activated && transform.position.y < 0) {
            transform.Translate(transform.up * Time.deltaTime * 5);
        }
    }

    public void activateDigger() {
        activated = true;
    }

    public bool getActivated() {
        return activated;
    }
}
