using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OccupiedTerrainRandomizerScript : MonoBehaviour
{

    public int numberOfChildrenToActivate;

    // Start is called before the first frame update
    void Start()
    {
        int rnd;

        for (int i = 0; i < numberOfChildrenToActivate; i++) {
            rnd = Random.Range(0, transform.childCount);
            transform.GetChild(rnd).gameObject.SetActive(true);
        }

        rnd = Random.Range(0, 360);
        transform.Rotate(new Vector3(0, rnd, 0));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
