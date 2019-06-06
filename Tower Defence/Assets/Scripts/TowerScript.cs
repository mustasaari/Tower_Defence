using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerScript : MonoBehaviour
{
    public int attackDMG = 1;
    public float baseRange = 5f;
    public float range = 10f;
    public float reloadTimer = 100f;
    public GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        setRange(10f);
        
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
            reloadTimer -= 10 * Time.deltaTime;
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
        target.GetComponent<EnemyScript>().takeDMG(1);
        drawDebugLine();
    }

    public void drawDebugLine()
    {
        Debug.DrawLine(transform.position + new Vector3(0,5,0), target.transform.position, Color.red, 2.5f);
    }

    public void setRange(float x)
    {
        this.range = x + baseRange;
    }

    public float getRange()
    {
        return this.range;
    }

    public void setAttack(float x)
    {
        this.range = x + baseRange;
    }

    public int getAttack()
    {
        return this.attackDMG;
    }
}
