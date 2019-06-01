using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTileScript : MonoBehaviour
{

    public Material red;
    public Material green;
    public Material brown;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Renderer>().material = brown;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseOver() {
        GetComponent<Renderer>().material = green;
    }

    void OnMouseExit() {
        GetComponent<Renderer>().material = brown;
    }
}
