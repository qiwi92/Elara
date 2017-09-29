using Debug = UnityEngine.Debug;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets;

public class RobotController : MonoBehaviour
{
    public List<GameObject> Cubes;

    private float velocity = 300f;
    private float timeToMove;
    private const float RestTime = 0.1f;
    private const float RestTimeBetween = 0.05f;

    public LeanTweenType TweenType;
    public  GameManager MyGameManager;

    private Direction _direction;

	private void Start()
    {
        
        _direction = Direction.up;
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
            

            yield return StartCoroutine(MineStone());

            ChooseNextDirection();

            yield return StartCoroutine(MoveParts(_direction.FrontMoveGroup(), timeToMove));

            yield return new WaitForSeconds(RestTimeBetween);


            // Later maybe check whether to move second part based on collisions
            yield return StartCoroutine(MoveParts(_direction.Opposite().FrontMoveGroup(), timeToMove));

            yield return new WaitForSeconds(RestTime);
            
            

            //yield return new WaitForSeconds(1f);

        }
    }
    
    private IEnumerator MoveParts(List<int> moveGroupIndices, float duration)
    {
        LeanTween.move(Cubes[moveGroupIndices[0]], Cubes[moveGroupIndices[0]].transform.position + _direction.DirectionToVector() * Grid.GridUnit, duration).setEase(TweenType);
        LeanTween.move(Cubes[moveGroupIndices[1]], Cubes[moveGroupIndices[1]].transform.position + _direction.DirectionToVector() * Grid.GridUnit, duration).setEase(TweenType);

        yield return new WaitForSeconds(duration);
    }

    private IEnumerator MineStone()
    {
        List<int> moveGroupIndices = _direction.FrontMoveGroup();



        List<Direction> minePatternDirection;


        for (int i = 0; i < moveGroupIndices.Count; i++)
        {
            CollisionType obstacle = MyCollision.Check(Cubes[moveGroupIndices[i]], _direction,MyGameManager.MyGrid);
            if (obstacle == CollisionType.Stone)
            {
                int nextX = (int)_direction.DirectionToVector().x + (int)GridTools.GridPosition(Cubes[moveGroupIndices[i]].transform.position).x;
                int nextY = (int)_direction.DirectionToVector().y + (int)GridTools.GridPosition(Cubes[moveGroupIndices[i]].transform.position).y;

                if (i == 0)
                {
                    minePatternDirection = new List<Direction> { _direction.TurnLeft(), _direction.TurnLeft().TurnRight(), _direction.TurnLeft().TurnRight().TurnLeft() };
                }
                else
                {
                    minePatternDirection= new List<Direction> { _direction.TurnRight(), _direction.TurnRight().TurnLeft(), _direction.TurnRight().TurnLeft().TurnRight() };
                }

                _direction = minePatternDirection[0];
                yield return StartCoroutine(MoveParts(_direction.FrontMoveGroup(), timeToMove));

                _direction = minePatternDirection[1];
                yield return StartCoroutine(MoveParts(_direction.FrontMoveGroup(), timeToMove));

                yield return StartCoroutine(MoveParts(_direction.Opposite().FrontMoveGroup(), timeToMove));

                yield return StartCoroutine(MoveParts(_direction.FrontMoveGroup(), timeToMove));

                yield return new WaitForSeconds(2f);


                Destroy(MyGameManager.StoneInstances[new Vector2(nextX, nextY)]);
                MyGameManager.MyGrid.Cells[nextX, nextY] = 0;

                yield return StartCoroutine(MoveParts(_direction.Opposite().FrontMoveGroup(), timeToMove));

                _direction =  minePatternDirection[2];
                yield return StartCoroutine(MoveParts(_direction.Opposite().FrontMoveGroup(), timeToMove));




            }

        }

        yield return null;

    }

    private void ChooseNextDirection()
    {
        bool col = true;
        while (col == true)
        {

            _direction = GetRandomDirection(_direction);

            List<int> moveGroupIndicesAfterTurn = _direction.FrontMoveGroup();

            int collisionCount = 0;

            for (int i = 0; i < moveGroupIndicesAfterTurn.Count; i++)
            {
                CollisionType obstacle = MyCollision.Check(Cubes[moveGroupIndicesAfterTurn[i]], _direction,MyGameManager.MyGrid);
                if (obstacle == CollisionType.GridEdge || obstacle == CollisionType.Stone)
                {
                    Debug.Log("Collided with " + obstacle);
                    collisionCount++;
                }
            }

            if(collisionCount == 0)
            {
                col = false;
            }
        }
    }

    private Direction GetRandomDirection(Direction direction)
    {
        int decider = UnityEngine.Random.Range(0, 7);
        if (decider == 0)
        {
            direction = direction.TurnLeft();
        }
        else if (decider == 1)
        {
            direction = direction.TurnRight();
        }
        return direction;
    }


}
