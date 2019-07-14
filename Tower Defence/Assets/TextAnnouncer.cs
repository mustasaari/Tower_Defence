using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextAnnouncer : MonoBehaviour
{

    public Color color1;
    public int size1;

    public Color color2;
    public int size2;

    Text textComp;
    bool announcing;
    bool fadein;

    float a = 0;
    float speed;

    // Start is called before the first frame update
    void Start()
    {
        textComp = GetComponent<Text>();
        textComp.color = new Color(0, 0, 0, 0);
        //textComp.color = new Color(1, 0, 0, 0.5f);
        //startAnnounce("Wave incoming");
    }

    // Update is called once per frame
    void Update()
    {
        if (announcing) {
            transform.localScale += transform.localScale * 0.1f * Time.deltaTime *speed;

            if (fadein) {
                a += 1f * Time.deltaTime * speed;
            }
            else {
                a -= 1f * Time.deltaTime * speed;
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
        textComp.color = color1;
        speed = 1;
        textComp.fontSize = size1;
        announcing = true;
        fadein = true;
        a = 0;
        textComp.text = t;
        transform.localScale = new Vector3(1,1,1);
        
    }

    public void startAnnounceAlert(string t) {
        textComp.color = color2;
        speed = 1.5f;
        textComp.fontSize = size2;
        announcing = true;
        fadein = true;
        a = 0;
        textComp.text = t;
        transform.localScale = new Vector3(1, 1, 1);
        GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("SFXvolume", 0.5f) /2f;
        GetComponent<AudioSource>().Play();
    }

}
