using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridScript : MonoBehaviour
{

    public List<GameObject> tiles;

    // Start is called before the first frame update
    void Start()
    {
        int tilesId = 0;

        foreach (Transform child in transform) {
            tiles.Add(child.gameObject);
            child.GetComponent<GridTileScript>().setID(tilesId);
            tilesId++;
        }

        generateDistances();
    }

    // Update is called once per frame
    void Update()
    {
        generateDistances();
    }

    void generateDistances() {

        foreach (GameObject tile in tiles) {
            tile.GetComponent<GridTileScript>().resetDistance();
        }

        for (int i = 1; i < 90; i++) {

            foreach (GameObject tile in tiles) {
                if (tile.GetComponent<GridTileScript>().getDistanceFromBottom() == i) {
                    tile.GetComponent<GridTileScript>().setNeighborDistances(i + 1);
                }
            }
        }
    }

    public List<GameObject> getAllTiles() {
        return tiles;
    }
}
