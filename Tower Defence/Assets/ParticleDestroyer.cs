using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDestroyer : MonoBehaviour
{

    ParticleSystem pasy;
    // Start is called before the first frame update
    void Start()
    {
        pasy = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!pasy.isPlaying) {
            Destroy(gameObject);
        }
    }
}
