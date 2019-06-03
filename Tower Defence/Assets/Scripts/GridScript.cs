using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridScript : MonoBehaviour
{

    public List<GameObject> tiles;
	bool validRoute;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform) {
            tiles.Add(child.gameObject);
        }

        generateDistances();
    }

    // Update is called once per frame
    void Update()
    {
        //generateDistances();
    }

    public void generateDistances() {

		validRoute = false;

        foreach (GameObject tile in tiles) {
            tile.GetComponent<TileScript>().resetDistance();
        }
			
        for (int i = 1; i < 90; i++) {

            foreach (GameObject tile in tiles) {
                if (tile.GetComponent<TileScript>().getDistanceFromBottom() == i) {
                    tile.GetComponent<TileScript>().setNeighborDistances(i + 1);
                }
            }
        }
    }

    public List<GameObject> getAllTiles() {
        return tiles;
    }

	public void setValidRoute(bool val) {
		validRoute = val;
	}

	public bool getValidRoute() {
		return validRoute;
	}
}
