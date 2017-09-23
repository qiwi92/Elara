﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets;

public class RobotController : MonoBehaviour {

    public List<GameObject> Cubes;

    private float currentTime = 0f;
    private float velocity = 24f;
    private float timeToMove;
    private float restTime = 1f;
    private float tempTime = 0f;

    private Direction direction;

    private float gridCellSize = 16f;
    private float restTimeBetween = 0.1f;
	// Use this for initialization
	void Start () {
        direction = Direction.up;
        timeToMove = gridCellSize / velocity;
        Cubes[0].transform.position = new Vector3(180, 50, 0);

        Cubes[1].transform.position = Cubes[0].transform.position + new Vector3(16, 0, 0);
        Cubes[2].transform.position = Cubes[0].transform.position + new Vector3(0, -16, 0);
        Cubes[3].transform.position = Cubes[0].transform.position + new Vector3(16, -16, 0);
        QuantizeCubes();
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
            List<int> moveGroupIndices = direction.BackMoveGroup();
            MoveGroup(direction, translate, moveGroupIndices);
        }
        if (SecondCubeMoveTimeExeeded)
        {
            QuantizeCubes();
        }
        if (SecondCubeRestTimeExeeded)
        {
            int decider = UnityEngine.Random.Range(0, 3);
            if (decider == 0)
            {
                direction = direction.TurnLeft();
            }
            else if(decider == 1)
            {
                direction = direction.TurnRight();
            }
            currentTime = 0;
        }


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