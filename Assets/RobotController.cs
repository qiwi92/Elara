using Debug = UnityEngine.Debug;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets;

public class RobotController : MonoBehaviour
{
    public List<GameObject> Cubes;

    private float velocity = 48f;
    private float timeToMove;
    private const float RestTime = 1f;
    private const float RestTimeBetween = 0.5f;

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
                direction = direction.TurnLeft();
            }
            else if (decider == 1)
            {
                direction = direction.TurnRight();
            }
        }
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
}
