using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{

	public string direction;
	int calculator;

    // Start is called before the first frame update
    void Start()
    {
		direction = "Down";
		calculator = 10;
		transform.rotation = Quaternion.LookRotation(Vector3.back);
    }

    // Update is called once per frame
    void Update()
    {
		RaycastHit hit;

		if (calculator == 0) {

			if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 10f)) {
				direction = hit.transform.gameObject.GetComponent<TileScript>().getEnemyDirection();
				//Debug.Log("Did Hit " +hit.transform.gameObject.name +"  " +direction);
			}
			calculator = 10;

		}

		calculator--;
		transform.Translate(Vector3.forward * Time.deltaTime * 4f);
        Vector3 newDir = new Vector3(0,0,0);

		
		if (direction.Equals("Down")) {
            newDir = Vector3.RotateTowards(transform.forward, Vector3.back, 0.01f, 0.01f);
        }
		if (direction.Equals("Up")) {
            newDir = Vector3.RotateTowards(transform.forward, Vector3.forward, 0.01f, 0.01f);
        }
		if (direction.Equals("Left")) {
        newDir = Vector3.RotateTowards(transform.forward, Vector3.left, 0.01f, 0.01f);
		}
		if (direction.Equals("Right")) {
            newDir = Vector3.RotateTowards(transform.forward, Vector3.right, 0.01f, 0.01f);
        }

        transform.rotation = Quaternion.LookRotation(newDir);

        if (transform.position.z < -70) {
            destroyMinion();
        }

    }

    public void destroyMinion() {
        GameManagerScript.removeMinionFromField();
        Destroy(gameObject);
    }
}
