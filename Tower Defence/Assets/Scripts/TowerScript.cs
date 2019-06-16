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

    public GameObject target;
    public GameObject laserPrefab;
    public GameObject laserPrefab2;

    public int[] wheels = new int[3];

    // Start is called before the first frame update
    void Start()
    {
        //GameObject slotmachine = GameObject.FindWithTag("SlotMachine");
        //slotmachine.GetComponent<SlotMachineScript>().firstTimeRandomization(this.gameObject);

        //setRange(10f);
        //GameObject.FindGameObjectWithTag("SlotMachine").GetComponent<SlotMachineScript>().firstTimeRandomization(gameObject);
        //Debug.Log("Tower created  my wheel is: " + wheels[0] + wheels[1] + wheels[2]);
    }

    // Update is called once per frame
    void Update()
    {
        checkReload();
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
        target.GetComponent<EnemyScript>().takeDMG(attackDMG);
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
        GameObject beam = Instantiate(laserPrefab2, transform.position + new Vector3(0, 3, 0), transform.rotation);
        beam.GetComponent<BulletScript3>().setTarget(target);
    }

    public void setRange(float x)
    {
        //Debug.Log("RANGE SET");
        this.range = x + baseRange;
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

    public void setWheels(int[] x) {
        wheels = x;
    }

    public int[] getWheels() {
        return wheels;
    }

    private void OnMouseOver() {
        if (Input.GetMouseButtonDown(0) && GetComponentInParent<TileScript>().status.Equals("Occupied")) {
            GetComponentInParent<TileScript>().openSlotMachine();
        }
    }
}
