using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey) {
            Debug.Log("A key or mouse click has been detected");
            GameObject.Find("LevelChanger").GetComponent<LevelChanger>().FadeToLevel(1);
        }
    }
}
