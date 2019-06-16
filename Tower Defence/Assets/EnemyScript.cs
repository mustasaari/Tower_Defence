using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{

	public string direction;
	float calculator;
    public int health = 10;
    public float speed;
    public int cost;
    private float rotSpeed;

    // Start is called before the first frame update
    void Start()
    {
		direction = "Down";
		calculator = 10f;
		transform.rotation = Quaternion.LookRotation(Vector3.back);
        //Debug.Log(gameObject.transform.name);
        if(gameObject.transform.name == "SpeedFly(Clone)"){
            rotSpeed = 2.5f * Time.deltaTime;
        }
        else if(gameObject.transform.name == "LadyBug(Clone)"){
            rotSpeed = 1 * Time.deltaTime;
        }
    }

    // Update is called once per frame
    void Update()
    {
		RaycastHit hit;

		if (calculator <= 0) {

			if (Physics.Raycast(transform.position +new Vector3(0,0.1f,0), transform.TransformDirection(Vector3.down), out hit, 10f)) {
				direction = hit.transform.gameObject.GetComponent<TileScript>().getEnemyDirection();
				//Debug.Log("Did Hit " +hit.transform.gameObject.name +"  " +direction);
			}
			calculator = 10f;
            Debug.Log("Scanned");
		}

		calculator -= 25 * Time.deltaTime;
		transform.Translate(Vector3.forward * Time.deltaTime * speed);
        Vector3 newDir = new Vector3(0,0,0);
		
		if (direction.Equals("Down")) {
            newDir = Vector3.RotateTowards(transform.forward, Vector3.back, rotSpeed, rotSpeed);
        }
		if (direction.Equals("Up")) {
            newDir = Vector3.RotateTowards(transform.forward, Vector3.forward, rotSpeed, rotSpeed);
        }
		if (direction.Equals("Left")) {
            newDir = Vector3.RotateTowards(transform.forward, Vector3.left, rotSpeed, rotSpeed);
		}
		if (direction.Equals("Right")) {
            newDir = Vector3.RotateTowards(transform.forward, Vector3.right, rotSpeed, rotSpeed);
        }

        transform.rotation = Quaternion.LookRotation(newDir);

        if (transform.position.z < -70) {
            GameManagerScript.eatLeafHP();
            destroyMinion();
        }

    }

    public void destroyMinion() {
        GameManagerScript.removeMinionFromField();
        Destroy(gameObject);
    }

    public void takeDMG(int x)
    {
        health -= x;
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
}
