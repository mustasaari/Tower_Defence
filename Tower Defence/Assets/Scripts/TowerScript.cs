﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerScript : MonoBehaviour
{
    public int moneyProduction = 0;
    public int attackDMG = 10;
    public float baseRange = 5f;
    public float range = 10f;
    public float reloadTimer = 0f;
    public float attackSpeedBonus = 10;
    public float critical = 0;
    public int poison = 0;
    int areaOfEffectRadius;

    public GameObject target;
    public GameObject laserPrefab;
    public GameObject laserPrefab2;

    public GameObject shootSoundGameObject;

    public int[] wheels = new int[3];
    public bool[] lockedWheels;
    GameObject[] slotMachineWheels;

    ParticleSystem rangeParticle;
    float rangeIndicatorRange;

    public string targetingMode; //Default, Furthest, Nearest, Least HP, most HP

    public Material crystalMaterialDefault;
    public Material crystalMaterialLowHP;
    public Material crystalMaterialHighHP;
    public Material crystalMaterialNear;
    public Material crystalMaterialFar;

    float criticalDamageBonusModifier;

    // Start is called before the first frame update
    void Start()
    {
        //GameObject slotmachine = GameObject.FindWithTag("SlotMachine");
        //slotmachine.GetComponent<SlotMachineScript>().firstTimeRandomization(this.gameObject);

        //setRange(10f);
        //GameObject.FindGameObjectWithTag("SlotMachine").GetComponent<SlotMachineScript>().firstTimeRandomization(gameObject);
        //Debug.Log("Tower created  my wheel is: " + wheels[0] + wheels[1] + wheels[2]);
        GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("SFXvolume", 0.5f);

        targetingMode = "Default";

        slotMachineWheels = GameObject.FindGameObjectsWithTag("SlotMachineWheel");
        rangeParticle = transform.GetChild(2).gameObject.GetComponent<ParticleSystem>();
        rangeParticle.Stop();

        lockedWheels = new bool[3];
        lockedWheels[0] = false;
        lockedWheels[1] = false;
        lockedWheels[2] = false;

        checkAppearance();

        criticalDamageBonusModifier = (float) PlayerPrefs.GetInt("BonusCritDMG",0);
    }

    // Update is called once per frame
    void Update()
    {
        checkReload();

        //Update tower looks when all wheels have stopped :/
        if (slotMachineWheels[0].GetComponent<SlotWheelScript>().getRotationReady() && slotMachineWheels[1].GetComponent<SlotWheelScript>().getRotationReady() && slotMachineWheels[2].GetComponent<SlotWheelScript>().getRotationReady()) {
            checkAppearance();
        }

    }

    public void checkReload()
    {
        if(reloadTimer <= 0)
        {
            //timer 0 -> Ammu
            hasTarget();
        }
        else
        {
            //vähennä reloadTimer.
            reloadTimer -= attackSpeedBonus * Time.deltaTime;
        }
    }

    public void hasTarget()
    {
        // Debug.Log("Hastarget : " +target);
        if(target != null)
        {
            //target on -> onko range.
            targetInRange();
        }
        else
        {
            //haeUusi
            findNewTarget();
        }
    }

    public void targetInRange()
    {
        Vector3 targetPosition = new Vector3(target.transform.position.x, 0, target.transform.position.z);

        //float dist = Vector3.Distance(target.transform.position, this.transform.position);
        float dist = Vector3.Distance(targetPosition, this.transform.position);
        if(dist <= range && !target.GetComponent<EnemyScript>().hasMinionFinished())
        {
            //jos target oikealla etäisyydellä -> Ammu.
            shoot();
        }
        else
        {
            //haeUusi
            findNewTarget();
        }
    }

    public void findNewTarget()
    {
        // Debug.Log("findnewtarget method");
        RaycastHit [] objects = Physics.SphereCastAll(transform.position, range, transform.up, 0f);
        List<GameObject> enemies = new List<GameObject>();

        foreach(RaycastHit hit in objects)
        {
            // Debug.Log("LoopLoop" + objects.Length);
            if(hit.transform.gameObject.tag == "Enemy")
            {
                enemies.Add(hit.transform.gameObject);
                // Debug.Log("enemy found");
            }
        }
        //set first enemy from list as comparator
        if (enemies.Count > 0) {
            target = enemies[0];
        }

        foreach(GameObject enemy in enemies)
        {
            if (targetingMode.Equals("Default")) {
                target = enemy;
            }
            else if (targetingMode.Equals("LowestHP")) {
                if(enemy.GetComponent<EnemyScript>().getHealth() < target.GetComponent<EnemyScript>().getHealth()) {
                    target = enemy;
                }
            }
            else if (targetingMode.Equals("HighestHP")) {
                if (enemy.GetComponent<EnemyScript>().getHealth() > target.GetComponent<EnemyScript>().getHealth()) {
                    target = enemy;
                }
            }
            else if (targetingMode.Equals("Nearest")) {
                if (Vector3.Distance(transform.position, enemy.transform.position) < Vector3.Distance(transform.position, target.transform.position)) {
                    target = enemy;
                }
            }
            else if (targetingMode.Equals("Farthest")) {
                if (Vector3.Distance(transform.position, enemy.transform.position) > Vector3.Distance(transform.position, target.transform.position)) {
                    target = enemy;
                }
            }

            //change this later plz!
            // Debug.Log("enemy set");
        }
    }

    public void shoot()
    {
        // Debug.Log(this + " Bang! " + target);
        reloadTimer = 100f;
        drawBullet();
        Color debugColor = Color.white;

        int dmgToDeal = attackDMG;
        if (Random.Range(1,101) < critical) {
            float dmg = attackDMG * (2 + (criticalDamageBonusModifier /4));
            dmgToDeal = (int) dmg;
            debugColor = Color.red;
        }

        Instantiate(shootSoundGameObject, transform.position, transform.rotation);

        //debugColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));  //DEBUG COLOR to see what tower does what

        target.GetComponent<EnemyScript>().takeDMG(dmgToDeal, areaOfEffectRadius, poison, debugColor);
        drawDebugLine();
    }

    public void drawDebugLine()
    {
        Debug.DrawLine(transform.position + new Vector3(0,5,0), target.transform.position, Color.red, 2.5f);
    }

    public void drawLaser() {
        GameObject laser = Instantiate(laserPrefab, transform.position + new Vector3(0, 3, 0), transform.rotation);
        laser.GetComponent<EnergyBeamScript>().endPosition(target);
    }

    public void drawLaser2() {
        GameObject beam = Instantiate(laserPrefab2, transform.position + new Vector3(0, 3, 0), transform.rotation);
        beam.GetComponent<EnergyBeamTwoScript>().setTarget(target);
    }

    public void drawBullet() {
        GameObject beam = Instantiate(laserPrefab2, transform.position + new Vector3(0, 15, 0), transform.rotation);
        beam.GetComponent<BulletScript3>().setTarget(target);
    }

    public void setRange(float x)
    {
        //Debug.Log("RANGE SET");
        this.range = x + baseRange;

        //Tässä tuli null reference erroria varmaankin kun kutsuttiin slotmachinen kautta.
        //Tein floatin joka asetetaan tässä ja sitten rangeksi onMouseOver metodissa. t: mikko
        rangeIndicatorRange = x + 5f;
        //ParticleSystem.ShapeModule shape = rangeParticle.shape;
        //shape.radius = range + 15f;
    }

    public float getRange()
    {
        return this.range;
    }

    public void setAttackDMG(int x)
    {
        this.attackDMG = x;
    }

    public int getAttackDMG()
    {
        return this.attackDMG;
    }

    public void setAttackSpeedBonus(float x) {
        this.attackSpeedBonus = x;
    }

    public void setMoneyProduction(int x) {
        moneyProduction = x;
    }

    public int getMoneyProduction() {
        return moneyProduction;
    }

    public void setAoERadius(int aoe) {
        this.areaOfEffectRadius = aoe;
    }

    public void setCritical(float crt) {
        critical = crt;
    }

    public void setPoison(int psn) {
        poison = psn;
    }

    public void setWheels(int[] x) {
        wheels = x;
    }

    public int[] getWheels() {
        return wheels;
    }

    public bool[] getLockedWheels() {
        return lockedWheels;
    }

    public void setLockedWheels(bool[] lw) {
        lockedWheels = lw;
    }

    public void setTargetingMode(string mode) {
        targetingMode = mode;
        if (targetingMode.Equals("Default")) {
            transform.GetChild(1).GetChild(0).gameObject.GetComponent<Renderer>().material = crystalMaterialDefault;
        }
        else if (targetingMode.Equals("LowestHP")) {
            transform.GetChild(1).GetChild(0).gameObject.GetComponent<Renderer>().material = crystalMaterialLowHP;
        }
        else if (targetingMode.Equals("HighestHP")) {
            transform.GetChild(1).GetChild(0).gameObject.GetComponent<Renderer>().material = crystalMaterialHighHP;
        }
        else if (targetingMode.Equals("Nearest")) {
            transform.GetChild(1).GetChild(0).gameObject.GetComponent<Renderer>().material = crystalMaterialNear;
        }
        else if (targetingMode.Equals("Farthest")) {
            transform.GetChild(1).GetChild(0).gameObject.GetComponent<Renderer>().material = crystalMaterialFar;
        }

        transform.GetChild(1).GetChild(1).gameObject.GetComponent<Light>().color = transform.GetChild(1).GetChild(0).gameObject.GetComponent<Renderer>().material.color;

    }

    public string getTargetingMode() {
        return targetingMode;
    }

    private void OnMouseEnter() {
        rangeParticle.Play();
    }

    private void OnMouseExit() {
        rangeParticle.Stop();
    }

    private void OnMouseOver() {

        //asettaa rangeIndikaattori particlen rangen
        ParticleSystem.ShapeModule shape = rangeParticle.shape;
        shape.radius = rangeIndicatorRange;

        if (!EventSystem.current.IsPointerOverGameObject() && Input.GetMouseButtonDown(0) && GetComponentInParent<TileScript>().status.Equals("Occupied") && GameManagerScript.gamePhase.Equals("Build")) {
            GetComponentInParent<TileScript>().openSlotMachine();
        }
    }

    public void checkAppearance() {

        //disable all
        for (int i = 0; i < 3; i++) {

            //Ei disabloida sitä objektia minkä pitäisi tulla aktiivisesti koska scriptit ei suoritu
            if (wheels[i] != 1) {
                transform.GetChild(0).gameObject.transform.GetChild(i).gameObject.transform.GetChild(0).gameObject.SetActive(false);
            }
            if (wheels[i] != 2) {
                transform.GetChild(0).gameObject.transform.GetChild(i).gameObject.transform.GetChild(1).gameObject.SetActive(false);
            }
            if (wheels[i] != 3) {
                transform.GetChild(0).gameObject.transform.GetChild(i).gameObject.transform.GetChild(2).gameObject.SetActive(false);
            }
            if (wheels[i] != 4) {
                transform.GetChild(0).gameObject.transform.GetChild(i).gameObject.transform.GetChild(3).gameObject.SetActive(false);
            }
            if (wheels[i] != 5) {
                transform.GetChild(0).gameObject.transform.GetChild(i).gameObject.transform.GetChild(4).gameObject.SetActive(false);
            }
            if (wheels[i] != 6) {
                transform.GetChild(0).gameObject.transform.GetChild(i).gameObject.transform.GetChild(5).gameObject.SetActive(false);
            }


        }

        //tämä toimii mutta grafiikat puuttuu !
        /*
        for (int i = 0; i < 3; i++) {
            transform.GetChild(0).gameObject.transform.GetChild(i).gameObject.transform.GetChild(wheels[i]).gameObject.SetActive(true);
        }
        */


        //first test        ihan toimiva mutta yllä olis parempi
        
        for (int i = 0; i < 3; i++) {   //iterate through floors
            if (wheels[i] == 1) {   //Damage
                transform.GetChild(0).gameObject.transform.GetChild(i).gameObject.transform.GetChild(0).gameObject.SetActive(true);      //base     //floor     
            }
            else if (wheels[i] == 2) {   //Range
                transform.GetChild(0).gameObject.transform.GetChild(i).gameObject.transform.GetChild(1).gameObject.SetActive(true);      //base     //floor     
            }
            else if (wheels[i] == 3) {   //Speed
                transform.GetChild(0).gameObject.transform.GetChild(i).gameObject.transform.GetChild(2).gameObject.SetActive(true);      //base     //floor     
            }
            else if (wheels[i] == 4) {   //Money
                transform.GetChild(0).gameObject.transform.GetChild(i).gameObject.transform.GetChild(3).gameObject.SetActive(true);      //base     //floor     
            }
            else if (wheels[i] == 5) {   //Area of effect
                transform.GetChild(0).gameObject.transform.GetChild(i).gameObject.transform.GetChild(4).gameObject.SetActive(true);      //base     //floor     
            }
            else if (wheels[i] == 6) {   //Poison
                transform.GetChild(0).gameObject.transform.GetChild(i).gameObject.transform.GetChild(5).gameObject.SetActive(true);      //base     //floor     
            }


            else {
                transform.GetChild(0).gameObject.transform.GetChild(i).gameObject.transform.GetChild(0).gameObject.SetActive(true);
            }
        }
    }
}
