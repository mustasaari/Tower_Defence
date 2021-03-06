﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{

    public string name;
	public string direction;
	float calculator;
    public int health = 10;
    public float speed;
    public int cost;
    private float rotSpeed;

    public GameObject aoeEffectPrefab1;
    public GameObject aoeEffectPrefab2;
    public GameObject aoeEffectPrefab3;
    public GameObject poisonEffectPrefab;
    public GameObject dmgText;

    public bool isEliteSpeedFly;
    public bool isEliteBumblebee;
    public bool isEliteLadybug;
    public bool isEliteBeetle;

    private int maxHP;

    private int poisonStacks = 0;

    private bool hasFinished;

    public int xp;

    // Start is called before the first frame update
    void Start()
    {
		direction = "Down";
        hasFinished = false;
		calculator = 10f;
		transform.rotation = Quaternion.LookRotation(Vector3.back);

        if (isEliteBeetle) {
            health += ((GameManagerScript.getWave() * GameManagerScript.getWave()) / 4 );
        }
        if (isEliteSpeedFly) {
            health += GameManagerScript.getWave() * 2;
            speed += (float)GameManagerScript.getWave() / 20f;
        }
        if (isEliteLadybug) {
            health += GameManagerScript.getWave() * 2;
            speed += (float) GameManagerScript.getWave() / 20f;
        }

        maxHP = health;
        //Debug.Log(gameObject.transform.name);
        if(gameObject.transform.name == "SpeedFly(Clone)"){
            rotSpeed = 3f;
        }
        else if(gameObject.transform.name == "LadyBug(Clone)"){
            rotSpeed = 1.5f;
        }
        else {
            rotSpeed = speed / 3;
        }

        InvokeRepeating("CalculateDamageOverTimeEffects", 1, 2);
    }

    // Update is called once per frame
    void Update()
    {
		RaycastHit hit;

		if (calculator <= 0) {

			if (Physics.Raycast(transform.position +new Vector3(0,0.1f,0), transform.TransformDirection(Vector3.down), out hit, 10f)) {
                if (hit.transform.gameObject.tag == "GridTile") {
                    direction = hit.transform.gameObject.GetComponent<TileScript>().getEnemyDirection();
                }
				//Debug.Log("Did Hit " +hit.transform.gameObject.name +"  " +direction);
			}
			calculator = 10f;
            // Debug.Log("Scanned");
		}

		calculator -= 25 * Time.deltaTime;
		transform.Translate(Vector3.forward * Time.deltaTime * speed);
        Vector3 newDir = new Vector3(0,0,0);
		
		if (direction.Equals("Down")) {
            newDir = Vector3.RotateTowards(transform.forward, Vector3.back, rotSpeed * Time.deltaTime, rotSpeed * Time.deltaTime);
        }
		if (direction.Equals("Up")) {
            newDir = Vector3.RotateTowards(transform.forward, Vector3.forward, rotSpeed * Time.deltaTime, rotSpeed * Time.deltaTime);
        }
		if (direction.Equals("Left")) {
            newDir = Vector3.RotateTowards(transform.forward, Vector3.left, rotSpeed * Time.deltaTime, rotSpeed * Time.deltaTime);
		}
		if (direction.Equals("Right")) {
            newDir = Vector3.RotateTowards(transform.forward, Vector3.right, rotSpeed * Time.deltaTime, rotSpeed * Time.deltaTime);
        }

        //elitespeedfly goes down
        if (isEliteBumblebee) {
            if (!hasFinished) { //lennä suoraan kunnes lopussa
                newDir = Vector3.RotateTowards(transform.forward, Vector3.back, rotSpeed * Time.deltaTime, rotSpeed * Time.deltaTime);
            }

            if (transform.position.y < 15 && !hasFinished) {
                transform.Translate(Vector3.up * Time.deltaTime *10f);
                //GetComponent<BoxCollider>().center = new Vector3(0, -transform.position.y +2.5f, 0);
            }
            else if (transform.position.y > 0.5 && hasFinished) {
                transform.Translate(Vector3.down * Time.deltaTime *10f);
                //GetComponent<BoxCollider>().center = new Vector3(0, -transform.position.y +2.5f, 0);
            }

        }

        transform.rotation = Quaternion.LookRotation(newDir);

        //if minion has reached it's goal, change minion state
        if (transform.position.z < -50 && !hasFinished) {
            GameManagerScript.eatLeafHP();
            //destroyMinion();
            hasFinished = true;
            GameManagerScript.removeMinionFromField();
        }

        if (transform.position.z < -100 && hasFinished) {
            destroyMinion();
        }

    }

    public void destroyMinion() {

        if (!hasFinished) {
            GameManagerScript.increaseExp(xp);
            GameManagerScript.removeMinionFromField();
        }
        Destroy(gameObject);
    }

    public void takeDMG(int dmg, int aoe, int poison, Color debugColor)
    {
        if (aoe > 0) {  //AOE stuff begins

            RaycastHit[] objects = Physics.SphereCastAll(transform.position, aoe/2, transform.up, 0f);

            foreach(RaycastHit hit in objects) {
                if (hit.transform.gameObject.tag == "Enemy" && hit.transform.gameObject != this.gameObject) {
                    hit.transform.gameObject.GetComponent<EnemyScript>().takeDMG(dmg, 0, poison, debugColor);   //do not pass aoe on
                }
            }

            if (aoe > 0 && aoe < 11) {  //play correct graphics based on radius
                Instantiate(aoeEffectPrefab1, transform.position, transform.rotation);
            }
            else if (aoe >= 11 && aoe <21) {       //play correct graphics based on radius
                Instantiate(aoeEffectPrefab2, transform.position, transform.rotation);
            }
            else if (aoe >= 21) {
                Instantiate(aoeEffectPrefab3, transform.position, transform.rotation);
            }
        }               //AOE stuff ends

        poisonStacks += poison;
        GameObject txt = Instantiate(dmgText, transform.position, transform.rotation);
        txt.GetComponent<dmgTextScript>().setNumber(dmg, debugColor);
        health -= dmg;
        checkHealth();
    }

    public void checkHealth()
    {
        if(health <= 0)
        {
            destroyMinion();
        }
    }
    public int getCost(){
        return this.cost;
    }

    public void OnMouseOver() {
        GameManagerScript.showEnemyHP(health, maxHP, name);
    }

    public int getHealth() {
        return health;
    }

    public bool hasMinionFinished() {
        return hasFinished;
    }

    void CalculateDamageOverTimeEffects() {
        if (poisonStacks > 0 && !hasFinished) {

            if (poisonStacks >= health) {
                Instantiate(poisonEffectPrefab, transform.position, transform.rotation);
            }

            health -= poisonStacks;

            GameObject txt = Instantiate(dmgText, transform.position, transform.rotation);
            txt.GetComponent<dmgTextScript>().setNumber(poisonStacks, Color.green);

            checkHealth();
        }
        //elite beetle health regen
        int regen = GameManagerScript.getWave() - 10;
        if (regen < 0) regen = 0;
        if (health + regen <= maxHP && isEliteBeetle) {
            health += regen;
        }
    }
}
