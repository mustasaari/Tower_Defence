using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBeamTwoScript : MonoBehaviour
{

    public GameObject target;

    ParticleSystem pasy;

    bool destr = false;
    bool destr2 = false;

    // Start is called before the first frame update
    void Start()
    {
        pasy = GetComponent<ParticleSystem>();
        pasy.Stop();
    }

    // Update is called once per frame
    void Update() {

        if (destr && destr2) {
            pasy.Play();
            destr2 = false;
        }

        if (target != null) {
            pasy.startLifetime = Vector3.Distance(transform.position, target.transform.position) / pasy.startSpeed;
            transform.LookAt(target.transform.position + new Vector3(0f, 2f, 0f));
        }
        else {
            //Destroy(gameObject);
        }
 

        if (pasy.isStopped && destr) {
            Destroy(gameObject);
        }
    }

    public void setTarget(GameObject targ) {
        Debug.Log("YAYYYYYYYYYYYYYYYYYY");
        target = targ;
        destr = true;
        destr2 = true;
    }
}
