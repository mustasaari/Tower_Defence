﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteTowerScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseOver() {
        if (Input.GetMouseButtonDown(0)) {
            gameObject.GetComponentInParent<SlotMachineScript>().deleteTower();
        }
    }

    private void OnMouseEnter() {
        transform.GetChild(0).gameObject.SetActive(true);
    }

    private void OnMouseExit() {
        transform.GetChild(0).gameObject.SetActive(false);
    }
}
