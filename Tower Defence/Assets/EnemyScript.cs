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
    }

    // Update is called once per frame
    void Update()
    {
		RaycastHit hit;

		if (calculator == 0) {

			if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 10f)) {
				direction = hit.transform.gameObject.GetComponent<TileScript>().getEnemyDirection();
				Debug.Log("Did Hit " +hit.transform.gameObject.name +"  " +direction);
			}
			calculator = 10;

		}

		calculator--;

		Vector3 newDir = Vector3.RotateTowards(transform.forward, Vector3.left, 0.01f,0.01f);
		transform.rotation = Quaternion.LookRotation(newDir);
		transform.Translate(Vector3.forward * Time.deltaTime * 2f);

		/**
		if (direction.Equals("Down")) {
			transform.Translate(Vector3.forward * Time.deltaTime * 2f);
		}
		if (direction.Equals("Up")) {
			transform.Translate(Vector3.forward * Time.deltaTime * -2f);
		}
		if (direction.Equals("Left")) {
			transform.Translate(Vector3.left * Time.deltaTime * -2f);
		}
		if (direction.Equals("Right")) {
			transform.Translate(Vector3.right * Time.deltaTime * -2f);
		}*/

    }
}
