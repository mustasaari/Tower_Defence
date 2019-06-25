using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHPIndicator : MonoBehaviour
{

    float childyscale;
    float childzscale;
    float childxscale;
    float newalpha;

    // Start is called before the first frame update
    void Start()
    {
        childyscale = transform.GetChild(0).gameObject.transform.localScale.y;
        childzscale = transform.GetChild(0).gameObject.transform.localScale.z;
        childxscale = transform.GetChild(0).gameObject.transform.localScale.x;
        newalpha = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (newalpha > 0) {
            newalpha -= 0.05f;  //reduce component visibility

            GetComponent<Image>().color = new Color(1, 1, 1, newalpha);
            transform.GetChild(1).gameObject.GetComponent<Text>().color = new Color(0, 0, 0, newalpha);
            transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color(1, 0, 0, newalpha);
        }
    }

    public void show(int hp, int maxhp, string name) {
        Debug.Log("HP : " + hp);
        transform.GetChild(1).gameObject.GetComponent<Text>().text = name + " : " +hp + " / " + maxhp +" hp";
        transform.GetChild(0).gameObject.transform.localScale = new Vector3( (float) hp / (float)maxhp * childxscale, childyscale, childzscale);

        if (newalpha < 1) { //make components more visible
            newalpha += 0.1f;
        }
    }
}
