using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript3 : MonoBehaviour
{

    public GameObject hitSoundGameObject;

    private Transform target;
    private GameObject targer;
    public float speed;

    float x;
    float y;
    float z;

    bool readyToDestroy = false;

    // Start is called before the first frame update
    void Start()
    {
        readyToDestroy = false;
    }

    // Update is called once per frame
    void Update()
    {

        //Debug.Log("X " + x + " Y " + y + " Z " + z);

        //transform.LookAt(new Vector3(x,y,z));
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(x, y + 3, z), speed * Time.deltaTime);


        if (transform.position == new Vector3(x,y + 3,z) && readyToDestroy == false) {
            //Destroy(gameObject);
            GetComponent<ParticleSystem>().Play();
            transform.GetChild(0).gameObject.GetComponent<Renderer>().enabled = false;  //hide sphere
            readyToDestroy = true;

            //play sound
            Instantiate(hitSoundGameObject, transform.position, transform.rotation);
            //GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("SFXvolume", 0.5f);
            //GetComponent<AudioSource>().Play();
        }

        if (GetComponent<ParticleSystem>().isPlaying == false && readyToDestroy) {
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
