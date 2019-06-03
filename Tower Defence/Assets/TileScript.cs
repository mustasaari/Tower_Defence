using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{

	public GameObject TileToLeft;
	public GameObject TileToRight;
	public GameObject TileToTop;
	public GameObject TileToDown;

    // Start is called before the first frame update
    void Start()
    {
		RaycastHit[] objects = Physics.SphereCastAll(transform.position, 6f, transform.up, 0f);

		foreach (RaycastHit hit in objects) {
			Debug.Log(hit.transform.gameObject.name);

			if (hit.transform.gameObject.transform.position.x > transform.position.x && hit.transform.gameObject.transform.position.z == transform.position.z) {
				TileToRight = hit.transform.gameObject;
			}
			if (hit.transform.gameObject.transform.position.x < transform.position.x && hit.transform.gameObject.transform.position.z == transform.position.z) {
				TileToLeft = hit.transform.gameObject;
			}
			if (hit.transform.gameObject.transform.position.x == transform.position.x && hit.transform.gameObject.transform.position.z > transform.position.z) {
				TileToTop = hit.transform.gameObject;
			}
			if (hit.transform.gameObject.transform.position.x == transform.position.x && hit.transform.gameObject.transform.position.z < transform.position.z) {
				TileToDown = hit.transform.gameObject;
			}
		}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
