using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTooltipScript : MonoBehaviour
{
    [TextArea(3,10)]
    public string tooltiptext;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {

    }

    public void showTooltip(string tex) {

    }

    public void OnMouseEnter() {
        //GameObject.FindGameObjectWithTag("SkillTooltip").SetActive(true);
        GameObject go = GameObject.FindGameObjectWithTag("SkillTooltip");
        go.GetComponent<Renderer>().enabled = true;
        go.transform.GetChild(0).gameObject.GetComponent<TextMesh>().text = tooltiptext;
    }

    public void OnMouseExit() {
            //GameObject.FindGameObjectWithTag("SkillTooltip").SetActive(false);
            GameObject go = GameObject.FindGameObjectWithTag("SkillTooltip");
            go.GetComponent<Renderer>().enabled = false;
            go.transform.GetChild(0).gameObject.GetComponent<TextMesh>().text = "";
        }


}
