﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyingTowerScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++) {
            transform.GetChild(i).gameObject.SetActive(true);
            transform.GetChild(i).gameObject.transform.SetParent(null);
        }
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
