using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTileScript : MonoBehaviour
{
    public int tileID;

    public Material red;
    public Material green;
    public Material brown;

    public GameObject tileToMyLeft;
    public GameObject tileToMyRight;
    public GameObject tileToMyTop;

    public bool bottomRowTile;

    public int distanceFromBottom;

    string status = "Free";

    // Start is called before the first frame update
    void Start()
    {
        status = "Free";
        GetComponent<Renderer>().material = brown;

        List<GameObject> tiles = GetComponentInParent<GridScript>().getAllTiles();

        foreach (GameObject tile in tiles) {
            if (tile.GetComponent<GridTileScript>().getID() == tileID -10 ) {
                tileToMyTop = tile;
            }
            if (tile.GetComponent<GridTileScript>().getID() == tileID +1 && tileID != 9 && tileID != 19 && tileID != 29 && tileID != 39 && tileID != 49 && tileID != 59 && tileID != 69 && tileID != 79 && tileID != 89 && tileID != 99) {
                tileToMyRight = tile;
            }
            if (tile.GetComponent<GridTileScript>().getID() == tileID -1 && tileID != 0 && tileID != 10 && tileID != 20 && tileID != 30 && tileID != 40 && tileID != 50 && tileID != 60 && tileID != 70 && tileID != 80 && tileID != 90) {
                tileToMyLeft = tile;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseEnter() {
        if (status.Equals("Free")) {
            GetComponent<Renderer>().material = green;
        }
    }

    private void OnMouseOver() {
        if (Input.GetMouseButtonDown(0) && status.Equals("Free")) {
            status = "Occupied";
            GetComponent<Renderer>().material = red;
        }
    }

    void OnMouseExit() {
        if (status.Equals("Free")) {
            GetComponent<Renderer>().material = brown;
        }
    }

    public int getDistanceFromBottom() {
        return distanceFromBottom;
    }

    public void setDistanceFromBottom(int x) {
        if (distanceFromBottom == 0 && status.Equals("Free")) {
            distanceFromBottom = x;
        }
    }

    public void setNeighborDistances(int dist) {
            if (tileToMyLeft != null) {
                tileToMyLeft.GetComponent<GridTileScript>().setDistanceFromBottom(dist);
            }
            if (tileToMyRight != null) {
                tileToMyRight.GetComponent<GridTileScript>().setDistanceFromBottom(dist);
            }
            if (tileToMyTop != null) {
                tileToMyTop.GetComponent<GridTileScript>().setDistanceFromBottom(dist);
            } 
    }

    public void setID(int id) {
        tileID = id;
    }

    public int getID() {
        return tileID;
    }

    public string getStatus() {
        return status;
    }

    public bool isBottomRowTile() {
        return bottomRowTile;
    }

    public void resetDistance() {
        if (bottomRowTile) {
            distanceFromBottom = 1;
        }
        else {
            distanceFromBottom = 0;
        }
    }
}
