using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Assets;

public class RobotController : MonoBehaviour {

    public List<GameObject> Cubes;


    private float currentTime = 0f;
    private float velocity = 400f;
    private float timeToMove;
    private float restTime = 0.05f;
    private float tempTime = 0f;

    private Grid _grid = new Grid();

    private Direction direction;

    private float gridCellSize = 16f;
    private float restTimeBetween = 0.05f;
	// Use this for initialization
	void Start () {
        direction = Direction.up;
        timeToMove = gridCellSize / velocity;


        Cubes[0].transform.position = new Vector3(180, 40, 0);
        QuantizeCubes();
        Cubes[1].transform.position = Cubes[0].transform.position + new Vector3(16, 0, 0);
        Cubes[2].transform.position = Cubes[0].transform.position + new Vector3(0, -16, 0);
        Cubes[3].transform.position = Cubes[0].transform.position + new Vector3(16, -16, 0);
        

    }
	
	// Update is called once per frame
	void Update () {
        float t = Time.deltaTime;
        
        currentTime += t;

        float translate = t * velocity;
        



        if (currentTime < timeToMove)
        {
            List<int> moveGroupIndices = direction.FrontMoveGroup();
            MoveGroup(direction, translate, moveGroupIndices);

            

            
        }
        if (FirstCubeMoveTimeExeeded)
        {
            QuantizeCubes();
        }
        if (FirstCubeRestTimeExeeded)
        {
            List<int> moveGroupIndices = direction.Opposite().FrontMoveGroup();
            MoveGroup(direction, translate, moveGroupIndices);
        }
        if (SecondCubeMoveTimeExeeded)
        {
            QuantizeCubes();
        }
        if (SecondCubeRestTimeExeeded)
        {
            List<int> moveGroupIndices = direction.FrontMoveGroup();
            bool collision = CheckCollision(moveGroupIndices, direction);
            if (collision == true)
            {
                direction = direction.Opposite();
            }
            else
            {
                int decider = UnityEngine.Random.Range(0, 3);
                if (decider == 0)
                {
                    Debug.Log(decider);
                    direction = direction.TurnLeft();
                }
                else if (decider == 1)
                {
                    direction = direction.TurnRight();
                }
            }

            
            currentTime = 0;
        }


        
        
        //Debug.Log(_grid.IsInGrid(Cubes[0].transform.position));

    }


    private bool CheckCollision(List<int> moveGroupIndices,Direction direction)
    {
        bool isInGrid;
        for (int i = 0; i <= moveGroupIndices.Count; i++)
        {
            if (direction == Direction.up || direction == Direction.right)
            {
                isInGrid = _grid.IsInGrid(Cubes[i].transform.position + Grid.GridUnit * direction.DirectionToVector() + Grid.GridOffset);
            }
            else
            {
                isInGrid = _grid.IsInGrid(Cubes[i].transform.position + Grid.GridUnit * direction.DirectionToVector() );
            }


            if(isInGrid == false)
            {
                return true;
            }
        }
        return false;
        
    }

    private void QuantizeCubes()
    {
        for(int i=0; i <=3; i++)
        {
            Cubes[i].transform.position = Cubes[i].transform.position.QuantizePosition();
        }
    }

    private void MoveGroup(Direction direction, float translate, List<int> moveGroupIndices)
    {   
        Cubes[moveGroupIndices[0]].transform.position += translate * direction.DirectionToVector();
        Cubes[moveGroupIndices[1]].transform.position += translate * direction.DirectionToVector();
    }

    private bool FirstCubeMoveTimeExeeded
    {
        get { return timeToMove < currentTime && currentTime < timeToMove + restTimeBetween; }
    }

    private bool FirstCubeRestTimeExeeded
    {
        get { return timeToMove + restTimeBetween < currentTime && currentTime < 2 * timeToMove + restTimeBetween; }
    }

    private bool SecondCubeMoveTimeExeeded
    {
        get { return 2 * timeToMove + restTimeBetween < currentTime; }
    }

    private bool SecondCubeRestTimeExeeded
    {
        get { return 2 * timeToMove + restTimeBetween + restTime < currentTime; }
    }
}
