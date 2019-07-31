using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroTextAppear : MonoBehaviour
{

    float startTime;
    public float appearTime;
    public float disappearTime;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        GetComponent<TextMesh>().color = new Color(GetComponent<TextMesh>().color.r, GetComponent<TextMesh>().color.g, GetComponent<TextMesh>().color.b, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > appearTime && Time.time < disappearTime && GetComponent<TextMesh>().color.a < 1f) {
            GetComponent<TextMesh>().color = new Color(GetComponent<TextMesh>().color.r, GetComponent<TextMesh>().color.g, GetComponent<TextMesh>().color.b, GetComponent<TextMesh>().color.a + 1.01f * Time.deltaTime);
        }
        else if (Time.time > disappearTime) {
            GetComponent<TextMesh>().color = new Color(GetComponent<TextMesh>().color.r, GetComponent<TextMesh>().color.g, GetComponent<TextMesh>().color.b, GetComponent<TextMesh>().color.a - 0.99f * Time.deltaTime);
        }
    }
}
