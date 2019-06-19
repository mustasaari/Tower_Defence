using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextAnnouncer : MonoBehaviour
{

    Text textComp;
    bool announcing;
    bool fadein;

    float a = 0;

    // Start is called before the first frame update
    void Start()
    {
        textComp = GetComponent<Text>();
        //textComp.color = new Color(1, 0, 0, 0.5f);
        //startAnnounce("Wave incoming");
    }

    // Update is called once per frame
    void Update()
    {
        if (announcing) {
            transform.localScale += transform.localScale * 0.1f * Time.deltaTime;

            if (fadein) {
                a += 1f * Time.deltaTime;
            }
            else {
                a -= 1f * Time.deltaTime;
            }

            if (fadein && a >= 2f) {
                fadein = false;
            }
            if (a < 0) {
                announcing = false;
            }

            textComp.color = new Color(textComp.color.r, textComp.color.g, textComp.color.b, a);
        }
    }

    public void startAnnounce(string t) {
        announcing = true;
        fadein = true;
        a = 0;
        textComp.text = t;
        transform.localScale = new Vector3(1,1,1);
    }
}
