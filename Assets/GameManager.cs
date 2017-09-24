using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets;

public class GameManager : MonoBehaviour {

    public Grid Grid = new Grid();

    public List<GameObject> Stones;

	void Start () {
        for (int y = 0; y < Grid.gridHeight; y++)
        {
            for (int x = 0; x < Grid.gridWidth; x++)
            {
                if(Grid._grid[x, y] == 1)
                {
                    Instantiate(Stones[0],new Vector3(Grid.GridXOffset+x*Grid.GridUnit,y*Grid.GridUnit,0),transform.rotation);
                }
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
