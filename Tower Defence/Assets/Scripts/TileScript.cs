using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{
	public int DistanceFromBottom;
	public string status;

	public GameObject TileToLeft;
	public GameObject TileToRight;
	public GameObject TileToTop;
	public GameObject TileToDown;

	public bool isBottomRowTile;
	public bool isTopRowTile;

	public Material red;
	public Material green;
	public Material brown;
	public Material black;

    // Start is called before the first frame update
    void Start()
    {
		status = "Free";
		GetComponent<Renderer>().material = brown;

		RaycastHit[] objects = Physics.SphereCastAll(transform.position, 6f, transform.up, 0f);

		foreach (RaycastHit hit in objects) {
			//Debug.Log(hit.transform.gameObject.name);

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

	public void resetDistance() {
		if (isBottomRowTile) {
			DistanceFromBottom = 1;
		} else {
			DistanceFromBottom = 0; // unset
		}
	}

	public int getDistanceFromBottom() {
		return DistanceFromBottom;
	}

	public void setNeighborDistances(int dist) {
		if (TileToLeft != null) {
			TileToLeft.GetComponent<TileScript>().setDistanceFromBottom(dist);
		}
		if (TileToRight != null) {
			TileToRight.GetComponent<TileScript>().setDistanceFromBottom(dist);
		}
		if (TileToTop != null) {
			TileToTop.GetComponent<TileScript>().setDistanceFromBottom(dist);
		} 
		if (TileToDown != null) {
			TileToDown.GetComponent<TileScript>().setDistanceFromBottom(dist);
		} 
	}

	public void setDistanceFromBottom(int x) {
		if (DistanceFromBottom == 0 && status.Equals("Free")) {
			DistanceFromBottom = x;
		}
	}

	void OnMouseEnter() {
		if (status.Equals("Free")) {
			GetComponent<Renderer>().material = green;
		}
	}

	private void OnMouseOver() {
		if (Input.GetMouseButtonDown(0) && status.Equals("Free")) {
			status = "Occupied";
			GetComponent<Renderer>().material = black;
		}
	}

	void OnMouseExit() {
		if (status.Equals("Free")) {
			GetComponent<Renderer>().material = brown;
		}
	}
}
