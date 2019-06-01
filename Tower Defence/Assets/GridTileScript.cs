using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTileScript : MonoBehaviour
{

    public Material red;
    public Material green;
    public Material brown;

    string status;

    // Start is called before the first frame update
    void Start()
    {
        status = "Free";
        GetComponent<Renderer>().material = brown;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseEnter() {
        if (status.Equals("Free")) {
            GetComponent<Renderer>().material = green;
        }
    }

    private void OnMouseOver() {
        if (Input.GetMouseButtonDown(0) && status.Equals("Free")) {
            status = "Occupied";
            GetComponent<Renderer>().material = red;
        }
    }

    void OnMouseExit() {
        if (status.Equals("Free")) {
            GetComponent<Renderer>().material = brown;
        }
    }
}
