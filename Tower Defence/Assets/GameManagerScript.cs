using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{

public static string gamePhase;
public static int wave;

    // Start is called before the first frame update
    void Start()
    {
        gamePhase = "Build";
        wave = 1;
    }

    // Update is called once per frame
    void Update()
    {
        startAttack();
        Debug.Log(gamePhase);
    }

    public static string getGamePhase() {
        return gamePhase;
    }

    public void startAttack() {
        if (Input.GetButtonDown("Jump") && gamePhase.Equals("Build")) {
            gamePhase = "Attack";
        }
    } 
}
