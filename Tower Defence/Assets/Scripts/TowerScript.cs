using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerScript : MonoBehaviour
{
    public int moneyProduction = 0;
    public int attackDMG = 10;
    public float baseRange = 5f;
    public float range = 10f;
    public float reloadTimer = 100f;
    public float attackSpeedBonus = 10;
    int areaOfEffectRadius;

    public GameObject target;
    public GameObject laserPrefab;
    public GameObject laserPrefab2;

    public int[] wheels = new int[3];
    public bool[] lockedWheels;
    GameObject[] slotMachineWheels;

    ParticleSystem rangeParticle; 

    // Start is called before the first frame update
    void Start()
    {
        //GameObject slotmachine = GameObject.FindWithTag("SlotMachine");
        //slotmachine.GetComponent<SlotMachineScript>().firstTimeRandomization(this.gameObject);

        //setRange(10f);
        //GameObject.FindGameObjectWithTag("SlotMachine").GetComponent<SlotMachineScript>().firstTimeRandomization(gameObject);
        //Debug.Log("Tower created  my wheel is: " + wheels[0] + wheels[1] + wheels[2]);

        slotMachineWheels = GameObject.FindGameObjectsWithTag("SlotMachineWheel");
        rangeParticle = transform.GetChild(2).gameObject.GetComponent<ParticleSystem>();
        rangeParticle.Stop();

        lockedWheels = new bool[3];
        lockedWheels[0] = false;
        lockedWheels[1] = false;
        lockedWheels[2] = false;

        checkAppearance();
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
        float dist = Vector3.Distance(target.transform.position, this.transform.position);
        if(dist <= range)
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
        foreach(GameObject enemy in enemies)
        {
            //change this later plz!
            // Debug.Log("enemy set");
            target = enemy;
        }
    }

    public void shoot()
    {
        // Debug.Log(this + " Bang! " + target);
        reloadTimer = 100f;
        drawBullet();

        Color debugColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));  //DEBUG COLOR to see what tower does what

        target.GetComponent<EnemyScript>().takeDMG(attackDMG, areaOfEffectRadius, debugColor);
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
        ParticleSystem.ShapeModule shape = rangeParticle.shape;
        shape.radius = range + 1f;
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

    private void OnMouseEnter() {
        rangeParticle.Play();
    }

    private void OnMouseExit() {
        rangeParticle.Stop();
    }

    private void OnMouseOver() {
        if (Input.GetMouseButtonDown(0) && GetComponentInParent<TileScript>().status.Equals("Occupied") && GameManagerScript.gamePhase.Equals("Build")) {
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


            else {
                transform.GetChild(0).gameObject.transform.GetChild(i).gameObject.transform.GetChild(0).gameObject.SetActive(true);
            }
        }
    }
}
