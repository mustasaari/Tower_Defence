using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{
	public int DistanceFromBottom;
	public string status;
	public string enemyWalkDirection = "Down";

	public GameObject TileToLeft;
	public GameObject TileToRight;
	public GameObject TileToTop;
	public GameObject TileToDown;
	public GameObject towerPrefab; 	//tower
	public bool isBottomRowTile;
	public bool isTopRowTile;
	public Material red;
	public Material green;
	public Material brown;
	public Material black;
	public static bool cursorActive;

    // Start is called before the first frame update
    void Start()
    {
		cursorActive = true;
		status = "Free";
		GetComponent<Renderer>().material = brown;

		RaycastHit[] objects = Physics.SphereCastAll(transform.position, 6f, transform.up, 0f);

		foreach (RaycastHit hit in objects) {
			//Debug.Log("SphereCast hit " + hit.transform.gameObject.name);

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
			enemyWalkDirection = "Down";
		} else {
			DistanceFromBottom = 0; // unset
		}
	}

	public int getDistanceFromBottom() {
		return DistanceFromBottom;
	}

	public void setNeighborDistances(int dist) {
		if (TileToLeft != null) {
			TileToLeft.GetComponent<TileScript>().setDistanceFromBottom(dist, "Right");
		}
		if (TileToRight != null) {
			TileToRight.GetComponent<TileScript>().setDistanceFromBottom(dist, "Left");
		}
		if (TileToTop != null) {
			TileToTop.GetComponent<TileScript>().setDistanceFromBottom(dist, "Down");
		} 
		if (TileToDown != null) {
			TileToDown.GetComponent<TileScript>().setDistanceFromBottom(dist, "Up");
		} 
	}

	public void setDistanceFromBottom(int x, string direction) {
		if (DistanceFromBottom == 0 && status.Equals("Free")) {
			DistanceFromBottom = x;
			enemyWalkDirection = direction;
		}
		if (isTopRowTile) {
			transform.parent.GetComponent<GridScript>().setValidRoute(true);
		}
	}

	void OnMouseEnter() {

		if (status.Equals("Free") && cursorActive && GameManagerScript.gamePhase.Equals("Build")) {
			status = "Selected";
		}
			

		transform.parent.GetComponent<GridScript>().generateDistances();

		if (status.Equals("Selected") && transform.parent.GetComponent<GridScript>().getValidRoute() && cursorActive && GameManagerScript.gamePhase.Equals("Build")) {
			GetComponent<Renderer>().material = green;
		}
		if (status.Equals("Selected") && !transform.parent.GetComponent<GridScript>().getValidRoute() && cursorActive && GameManagerScript.gamePhase.Equals("Build")) {
			GetComponent<Renderer>().material = red;
		}
	}

	private void OnMouseOver() {
		if (Input.GetMouseButtonDown(0) && status.Equals("Selected") && transform.parent.GetComponent<GridScript>().getValidRoute() 
		&& !isBottomRowTile && !isTopRowTile) {
			status = "Occupied";
			GetComponent<Renderer>().material = brown;
			CreateTower();
		}
        else if (Input.GetMouseButtonDown(0) && status.Equals("Occupied") && cursorActive  && GameManagerScript.gamePhase.Equals("Build")) {
            openSlotMachine();
        }
		else if (GameManagerScript.gamePhase.Equals("Attack") && status.Equals("Selected")){
			GetComponent<Renderer>().material = brown;
			status = "Free";
			transform.parent.GetComponent<GridScript>().generateDistances();
		}
	}

	void OnMouseExit() {
		if (status.Equals("Selected")) {
			GetComponent<Renderer>().material = brown;
			status = "Free";
		}
	}

	void CreateTower(){
		Vector3 newPos = new Vector3(this.transform.position.x, this.transform.position.y +0.5f, this.transform.position.z);
    	GameObject octo = Instantiate(towerPrefab, newPos, Quaternion.identity) as GameObject;
		//octo.transform.Rotate(-90,0,0);
		octo.transform.SetParent(this.transform);
	}

	public string getEnemyDirection() {
		return enemyWalkDirection;
	}

    public void openSlotMachine() {
        Debug.Log("Open Slot MAchine");
        GameObject slotmachine = GameObject.FindWithTag("SlotMachine");
        slotmachine.GetComponent<SlotMachineScript>().openSlotMachine(transform.GetChild(0).gameObject);
    }
}
