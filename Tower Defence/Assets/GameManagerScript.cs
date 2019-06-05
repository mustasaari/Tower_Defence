using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{

public static string gamePhase;
public static int wave;

public GameObject spawn1;
public GameObject spawn2;
public GameObject spawn3;
public GameObject spawn4;
public GameObject spawn5;
public GameObject spawn6;
public GameObject spawn7;
public GameObject spawn8;
public GameObject spawn9;
public GameObject spawn10;

public GameObject enemy1;

int sleep = 10;

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
        spawnMinions();
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

    public void spawnMinions() {
        if (gamePhase.Equals("Attack") && sleep < 1) {
            int rnd = Random.Range(1,11); // 1-10

            if (rnd == 1) {
                Instantiate(enemy1, spawn1.transform.position , Quaternion.identity);
                sleep = 90;
            }
            else if (rnd == 2) {
                Instantiate(enemy1, spawn2.transform.position , Quaternion.identity);
                sleep = 90;
            }
            else if (rnd == 3) {
                Instantiate(enemy1, spawn3.transform.position , Quaternion.identity);
                sleep = 90;
            }
            else if (rnd == 4) {
                Instantiate(enemy1, spawn4.transform.position , Quaternion.identity);
                sleep = 90;
            }
            else if (rnd == 5) {
                Instantiate(enemy1, spawn5.transform.position , Quaternion.identity);
                sleep = 90;
            }
            else if (rnd == 6) {
                Instantiate(enemy1, spawn6.transform.position , Quaternion.identity);
                sleep = 90;
            }
            else if (rnd == 7) {
                Instantiate(enemy1, spawn7.transform.position , Quaternion.identity);
                sleep = 90;
            }
            else if (rnd == 8) {
                Instantiate(enemy1, spawn8.transform.position , Quaternion.identity);
                sleep = 90;
            }
            else if (rnd == 9) {
                Instantiate(enemy1, spawn9.transform.position , Quaternion.identity);
                sleep = 90;
            }
            else if (rnd == 10) {
                Instantiate(enemy1, spawn10.transform.position , Quaternion.identity);
                sleep = 90;
            }
        }

        sleep--;
    } 
}
