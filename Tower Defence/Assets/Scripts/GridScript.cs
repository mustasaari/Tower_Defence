using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridScript : MonoBehaviour
{

    public List<GameObject> tiles;
	bool validRoute;

    public GameObject nonBuildableTileGameobject;
    public GameObject occupiedTileGameobject;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform) {
            tiles.Add(child.gameObject);
        }

        spawnObstacles();

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

    public void spawnObstacles() {

        //Generate nonbuildable tiles
        int rounds = 10;
        for (int i = 0; i < rounds; i++) {
            int rndTile = Random.Range(0, 100);
            GameObject spawnTo = tiles[rndTile];
            if (spawnTo.GetComponent<TileScript>().status.Equals("NonBuildable") || spawnTo.GetComponent<TileScript>().isBottomRowTile || spawnTo.GetComponent<TileScript>().isTopRowTile) {
                rounds++;
            }
            else {
                spawnTo.GetComponent<TileScript>().setStatus("NonBuildable");
                Instantiate(nonBuildableTileGameobject, spawnTo.transform.position, spawnTo.transform.rotation);
            }
        }

        //Generate Occupied tiles
        for (int i = 0; i < rounds; i++) {
            int rndTile = Random.Range(0, 100);
            GameObject spawnTo = tiles[rndTile];
            if (spawnTo.GetComponent<TileScript>().status.Equals("NonBuildable") || spawnTo.GetComponent<TileScript>().isBottomRowTile || spawnTo.GetComponent<TileScript>().isTopRowTile || spawnTo.GetComponent<TileScript>().status.Equals("Occupied")) {
                rounds++;
            }
            else {
                spawnTo.GetComponent<TileScript>().setStatus("Occupied");
                Instantiate(occupiedTileGameobject, spawnTo.transform.position, spawnTo.transform.rotation);
            }
        }
    }
}
