using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollCostScript : MonoBehaviour
{

    bool scale = false;

    float startScale;
    float startScaley;
    float startScalez;
    float howMuchToScale = 0.01f;

    // Start is called before the first frame update
    void Start()
    {
        startScale = transform.localScale.x;
        startScaley = transform.localScale.y;
        startScalez = transform.localScale.z;
    }

    // Update is called once per frame
    void Update()
    {

        if (GameManagerScript.getGamePhase().Equals("Build")) {

            if (scale && transform.localScale.x < startScale + howMuchToScale) {
                transform.localScale = transform.localScale * 1.02f;
            }
            else if (scale && transform.localScale.x >= startScale + howMuchToScale) {
                scale = false;
            }
            else if (!scale && transform.localScale.x > startScale) {
                transform.localScale = transform.localScale * 0.98f;
            }
            else if (transform.localScale.x < startScale) {
                transform.localScale = new Vector3(startScale, startScaley, startScalez);
            }
        }
    }

    public void bounce() {
        scale = true;
    }
}
