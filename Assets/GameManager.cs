using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets;

namespace Assets
{
public class GameManager : MonoBehaviour {

    public Grid MyGrid = new Grid();

    public List<GameObject> StonePrefabs;
    public Dictionary<Vector2, GameObject> StoneInstances = new Dictionary<Vector2, GameObject>();

        void Start () {
        for (int y = 0; y < Grid.gridHeight; y++)
        {
            for (int x = 0; x < Grid.gridWidth; x++)
            {
                if(MyGrid.Cells[x, y] == 1)
                {
                    
                    GameObject tempStoneInstance = Instantiate(StonePrefabs[0],new Vector3(Grid.GridXOffset+x*Grid.GridUnit,y*Grid.GridUnit,0),transform.rotation);
                    StoneInstances.Add(new Vector2(x, y), tempStoneInstance);
                }
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

}
