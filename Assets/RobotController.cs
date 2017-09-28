using Debug = UnityEngine.Debug;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets;

public class RobotController : MonoBehaviour
{
    public List<GameObject> Cubes;

    private Grid StoneGrid = new Grid();

    private float velocity = 300f;
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

            //yield return new WaitForSeconds(1f);

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
        for (int i = 0; i < 2; i++)
        {
            int xPos = (int)GridTools.GridPosition(Cubes[moveGroupIndices[i]].transform.position).x;
            int yPos = (int)GridTools.GridPosition(Cubes[moveGroupIndices[i]].transform.position).y;
            int nextX =  (int)direction.DirectionToVector().x;
            int nextY =  (int)direction.DirectionToVector().y;
            Debug.Log("x:" + xPos + " + ("+ nextX + ") / y: " + yPos + " + (" + nextY + ")");
        }

        bool col = false;
        while (col == false)
        {

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
            //bool collision = CheckGridCollision(moveGroupIndicesAfterTurn, direction);
            bool collisionStone = Collision(moveGroupIndicesAfterTurn, direction);
            if (collisionStone == true) 
            {
                Debug.Log("Col");
            }

            else
            {
                col = true;
            }
            
        }


            

    }
    


    private bool Collision(List<int> moveGroupIndices, Direction direction)
    {
        for (int i = 0; i < moveGroupIndices.Count; i++)
        {
            int xPos = (int)GridTools.GridPosition(Cubes[moveGroupIndices[i]].transform.position).x;
            int yPos = (int)GridTools.GridPosition(Cubes[moveGroupIndices[i]].transform.position).y;
            int nextX = (int)direction.DirectionToVector().x;
            int nextY = (int)direction.DirectionToVector().y;

            if (xPos +nextX < Grid.gridWidth && xPos + nextX >= 0 && yPos + nextY < Grid.gridHeight && yPos + nextY >= 0)
            {
                for (int y = 0; y < Grid.gridHeight; y++)
                {
                    for (int x = 0; x < Grid.gridWidth; x++)
                    {
                        if (StoneGrid._grid[xPos + nextX, yPos + nextY] == 1)
                        {

                            return true;
                        }    
                    }
                }
            }
            else
            {
                return true;
            }
        }
        return false;
    }
}
