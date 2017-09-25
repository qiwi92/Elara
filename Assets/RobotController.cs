using Debug = UnityEngine.Debug;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets;

public class RobotController : MonoBehaviour
{
    public List<GameObject> Cubes;

    private Grid StoneGrid = new Grid();

    private float velocity = 400f;
    private float timeToMove;
    private const float RestTime = 0.1f;
    private const float RestTimeBetween = 0.05f;

    public LeanTweenType TweenType;

    private Grid _grid = new Grid();
    private Direction direction;

	private void Start()
    {
        direction = Direction.up;
        timeToMove = Grid.GridUnit / velocity;

        Cubes[0].transform.position = new Vector3(Grid.GridXOffset + Grid.GridUnit * 3, Grid.GridUnit*3, 0);
        Cubes[1].transform.position = Cubes[0].transform.position + new Vector3(16, 0, 0);
        Cubes[2].transform.position = Cubes[0].transform.position + new Vector3(0, -16, 0);
        Cubes[3].transform.position = Cubes[0].transform.position + new Vector3(16, -16, 0);

        Application.targetFrameRate = 60;
        StartCoroutine(MoveRoutine());

        
    }

    private IEnumerator MoveRoutine()
    {
        while (true)
        {
            yield return StartCoroutine(MoveParts(direction.FrontMoveGroup(), timeToMove));

            yield return new WaitForSeconds(RestTimeBetween);

            // Later maybe check whether to move second part based on collisions
            yield return StartCoroutine(MoveParts(direction.Opposite().FrontMoveGroup(), timeToMove));

            yield return new WaitForSeconds(RestTime);
            
            ChooseNextDirection();

        }
    }
    
    private IEnumerator MoveParts(List<int> moveGroupIndices, float duration)
    {
        LeanTween.move(Cubes[moveGroupIndices[0]], Cubes[moveGroupIndices[0]].transform.position + direction.DirectionToVector() * Grid.GridUnit, duration).setEase(TweenType);
        LeanTween.move(Cubes[moveGroupIndices[1]], Cubes[moveGroupIndices[1]].transform.position + direction.DirectionToVector() * Grid.GridUnit, duration).setEase(TweenType);

        yield return new WaitForSeconds(duration);
    }
    
    private void ChooseNextDirection()
    {
        List<int> moveGroupIndices = direction.FrontMoveGroup();
       

        int decider = UnityEngine.Random.Range(0, 3);
        if (decider == 0)
        {
            direction = direction.TurnLeft();
        }
        else if (decider == 1)
        {
            direction = direction.TurnRight();
        }

        List<int> moveGroupIndicesAfterTurn = direction.FrontMoveGroup();
        bool collision = CheckGridCollision(moveGroupIndicesAfterTurn, direction);
        bool collisionStone = StoneCollision(moveGroupIndicesAfterTurn, direction);
        if (collision == true) 
        {
            direction = direction.Opposite();
        }
        else if (collisionStone == true ) 
        {
            direction = direction.Opposite();
        }

    }
    
    private bool CheckGridCollision(List<int> moveGroupIndices,Direction direction)
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

    private bool StoneCollision(List<int> moveGroupIndices, Direction direction)
    {
        for (int i = 0; i <= moveGroupIndices.Count; i++)
        {
            int xPos = (int)GridTools.GridPosition(Cubes[i].transform.position).x;
            int yPos = (int)GridTools.GridPosition(Cubes[i].transform.position).y;
            int nextX = (int)direction.DirectionToVector().x;
            int nextY = (int)direction.DirectionToVector().y;

            if (xPos +nextX < Grid.gridWidth && xPos + nextX > 0 && yPos + nextY < Grid.gridHeight && yPos + nextY > 0)
            {
                if (direction == Direction.up || direction == Direction.right)
                {
                    
                    for (int y = 0; y < Grid.gridHeight; y++)
                    {
                        for (int x = 0; x < Grid.gridWidth; x++)
                        {
                            if (StoneGrid._grid[xPos +  nextX, yPos + nextY] == 1)
                            {
                                Debug.Log("Next x:" + xPos + "-> " + xPos + nextX + " / Next y: " + yPos + "-> " + yPos + nextY);
                                return true;
                            }
                        }
                    }
                }
                else
                {
                    
                    for (int y = 0; y < Grid.gridHeight; y++)
                    {
                        for (int x = 0; x < Grid.gridWidth; x++)
                        {
                            if (StoneGrid._grid[xPos + nextX, yPos + nextY] == 1)
                            {
                                Debug.Log("Next x:" + xPos + "-> " + xPos + nextX + " / Next y: " + yPos + "-> " + yPos + nextY);
                                return true;
                            }
                        }
                    }
                }
            }
            //var bla = StoneGrid._grid[x, y];

        }
        return false;
    }
}
