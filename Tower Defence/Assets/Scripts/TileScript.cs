using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
	public Material defaultMaterial;
	public Material black;
	public static bool cursorActive;

    float buildDelay;
    bool buildDragPrevent;

    // Start is called before the first frame update
    void Start()
    {
		cursorActive = true;
        if (!status.Equals("NonBuildable") && !status.Equals("Occupied")) {
            status = "Free";
        }
        defaultMaterial = GetComponent<Renderer>().material;

        buildDelay = 0;
        buildDragPrevent = false;
		//GetComponent<Renderer>().material = defaultMaterial;

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
        if (Input.GetMouseButtonUp(0)) {
            buildDelay = 0;
            GameObject.FindGameObjectWithTag("BuildingInProgress").GetComponent<BuildingInProgressScript>().setSizeToZero();
        }
    }

	public void resetDistance() {
		if (isBottomRowTile) {
			DistanceFromBottom = 1;
			//enemyWalkDirection = "Down";
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
		if (DistanceFromBottom == 0 && (status.Equals("Free") || status.Equals("NonBuildable"))) {
			DistanceFromBottom = x;
			enemyWalkDirection = direction;
		}
		if (isTopRowTile) {
			transform.parent.GetComponent<GridScript>().setValidRoute(true);
		}
	}

	void OnMouseEnter() {

        
		//if (!EventSystem.current.IsPointerOverGameObject() && cursorActive){		//IF NOT UI ELEMENT Check!  1.7
			if (status.Equals("Free") && cursorActive && GameManagerScript.gamePhase.Equals("Build") && !isBottomRowTile && !isTopRowTile) {
				status = "Selected";
			}
				
			transform.parent.GetComponent<GridScript>().generateDistances();

			if (status.Equals("Selected") && transform.parent.GetComponent<GridScript>().getValidRoute() && GameManagerScript.gamePhase.Equals("Build")) {
				GetComponent<Renderer>().material = green;
			}
			if (status.Equals("Selected") && !transform.parent.GetComponent<GridScript>().getValidRoute() && GameManagerScript.gamePhase.Equals("Build")) {
				GetComponent<Renderer>().material = red;
			}
		//}     1.7
	}

	private void OnMouseOver() {

        //transform.parent.GetComponent<GridScript>().generateDistances();    // 1.7 generate distances in each update because mouse might come over without OnEnter from UI

        //if (!EventSystem.current.IsPointerOverGameObject() && cursorActive){		//IF NOT UI ELEMENT Check! -> then normal behaviour 1.7
			if (Input.GetMouseButtonDown(0)) {
				buildDragPrevent = true;
			}
			if (Input.GetMouseButtonUp(0)) {
				buildDragPrevent = false;
			}

			if (Input.GetMouseButton(0) && status.Equals("Selected") && transform.parent.GetComponent<GridScript>().getValidRoute() 
			&& !isBottomRowTile && !isTopRowTile && GameManagerScript.getTowers() > 0 && buildDragPrevent && !EventSystem.current.IsPointerOverGameObject()) {  //1.7 vaihtoehto2 ispointerover lisätty

				if (buildDelay > 100) {
					GameObject.FindGameObjectWithTag("BuildingInProgress").GetComponent<BuildingInProgressScript>().setSizeToZero();
					status = "Occupied";
					GetComponent<Renderer>().material = defaultMaterial;
					CreateTower();
				}
				else {
					buildDelay += Time.deltaTime * 100f;
					GameObject.FindGameObjectWithTag("BuildingInProgress").GetComponent<BuildingInProgressScript>().increase();
					GameObject.FindGameObjectWithTag("BuildingInProgress").GetComponent<BuildingInProgressScript>().setPosition(transform);
				}
			}

			//childcount added to if-statemant because clicking occupied-mushroom-tile caused trying to open slotmachine
			else if (Input.GetMouseButtonDown(0) && status.Equals("Occupied")  && GameManagerScript.gamePhase.Equals("Build") && transform.childCount > 0 ) {
				openSlotMachine();
			}
			else if (GameManagerScript.gamePhase.Equals("Attack") && status.Equals("Selected")){
				GetComponent<Renderer>().material = defaultMaterial;
				status = "Free";
				transform.parent.GetComponent<GridScript>().generateDistances();
			}

            //1.7
			//SetTile selected if when leaving from UI Element.
			//if (!GetComponent<Renderer>().material.name.Equals("green (Instance)") && !status.Equals("Occupied") 
			//&& !status.Equals("NonBuildable") && GameManagerScript.gamePhase.Equals("Build") && !isBottomRowTile && !isTopRowTile) {
            //    GetComponent<Renderer>().material = green;
            //    status = "Selected";
            //}

            //1.7 UI hommeleiden jälkeen ei ruutu muutu punaiseksi niin tämä IF-lause kopioitu onMouseEnteristä 
            //ei valid routea
            //if (status.Equals("Selected") && !transform.parent.GetComponent<GridScript>().getValidRoute() && GameManagerScript.gamePhase.Equals("Build")) {
            //    GetComponent<Renderer>().material = red;
            //}

        //}
		//else if(!status.Equals("NonBuildable") && !status.Equals("Occupied")){
		//	GetComponent<Renderer>().material = defaultMaterial;
		//} //1.7 loppuu

        //1.7 vaihtoehto2
        //jos ruutu (oletetusti) vihreänä ja hiiri menee UI:n päälle vaihda väri defaultiin
        if (status.Equals("Selected") && EventSystem.current.IsPointerOverGameObject()) {
            GetComponent<Renderer>().material = defaultMaterial;
        } //jos ruutu valittuna ja ei-ui:n päällä ja route on ok niin vihreäksi väri laita
        else if (status.Equals("Selected") && !EventSystem.current.IsPointerOverGameObject() && transform.parent.GetComponent<GridScript>().getValidRoute()) {
            GetComponent<Renderer>().material = green;
        }   // jos valittuna mutta ei reittiä ole niin sitten punainen
        else if (status.Equals("Selected") && !transform.parent.GetComponent<GridScript>().getValidRoute()) {
            GetComponent<Renderer>().material = red;
        }
    }

	void OnMouseExit() {
        GameObject.FindGameObjectWithTag("BuildingInProgress").GetComponent<BuildingInProgressScript>().setSizeToZero();
        buildDelay = 0;
        buildDragPrevent = false;

        if (status.Equals("Selected")) {
			GetComponent<Renderer>().material = defaultMaterial;
			status = "Free";
		}
	}

	void CreateTower(){
		Vector3 newPos = new Vector3(this.transform.position.x, this.transform.position.y +0.5f, this.transform.position.z);
		GameObject octo = Instantiate(towerPrefab, newPos, Quaternion.identity) as GameObject;
		//octo.transform.Rotate(-90,0,0);
		octo.transform.SetParent(this.transform);

        GameObject slotmachine = GameObject.FindWithTag("SlotMachine");
        slotmachine.GetComponent<SlotMachineScript>().firstTimeRandomization(transform.GetChild(0).gameObject);

        GameManagerScript.reduceTowers();
	}

	public string getEnemyDirection() {
		return enemyWalkDirection;
	}

    public void openSlotMachine() {
        //Debug.Log("Open Slot MAchine");
        GameObject slotmachine = GameObject.FindWithTag("SlotMachine");
        slotmachine.GetComponent<SlotMachineScript>().openSlotMachine(transform.GetChild(0).gameObject);
    }

    public void setStatus(string stat) {
        status = stat;
    }
}
