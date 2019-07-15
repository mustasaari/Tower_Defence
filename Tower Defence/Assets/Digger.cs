using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Digger : MonoBehaviour
{

    bool activated = false;

    public GameObject routeSprite;

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

        if(activated && transform.position.y >= 0) {
            transform.GetChild(2).Translate(Vector3.down * Time.deltaTime * 2, Space.World);
        }
    }

    public void activateDigger() {
        activated = true;
        transform.parent.transform.GetChild(1).GetComponent<ParticleSystem>().Play();
        GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("SFXvolume", 0.5f);
        GetComponent<AudioSource>().Play();
        InvokeRepeating("routeSprites", 3, 3);
    }

    public bool getActivated() {
        return activated;
    }

    void routeSprites() {
        if (GameManagerScript.getGamePhase().Equals("Build")) {
            Instantiate(routeSprite, transform.position, transform.rotation);
        }
    }
}
