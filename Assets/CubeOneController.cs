using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets;

public enum Corner
{
    UpperLeft = 1,
    UpperRight = 2,
    LowerLeft = 3,
    LowerRight = 4
}





public class CubeOneController : MonoBehaviour {


    public GameObject cubeTwo;
    public GameObject cubeThree;
    public GameObject cubeFour;

    public GameObject vertBarLeft;
    public GameObject vertBarRight;

    private float velocity = 24f;
    private float pauseTime = 1f;
    private float currentTime = 0f;


    

    private int isActive = 1;


    private Direction direction;

    // Use this for initialization
    void Start () {
        direction = Direction.up;

        transform.position = new Vector3(50, 50, 0);
        cubeTwo.transform.position = transform.position + new Vector3(16, 0, 0); 
        cubeThree.transform.position = transform.position + new Vector3(16, -16, 0); 
        cubeFour.transform.position = transform.position + new Vector3(0, -16, 0);

        vertBarLeft.transform.position = transform.position + new Vector3(1, -8, 0);
        vertBarRight.transform.position = transform.position + new Vector3(15, -8, 0);

    }
	
	// Update is called once per frame
	void Update () {
        float t = Time.deltaTime;

        currentTime += Time.deltaTime;

        float translate = t * velocity;

        var penis = Grid.QuantizePosition(transform.position);


        float separation = Vector3.Distance(transform.position,cubeFour.transform.position);

        if (separation < 16 || separation > 32)
        {
            isActive *= -1;
        }

        if (isActive > 0)
        {

            transform.position += translate*direction.DirectionToVector();
            cubeTwo.transform.position += translate * direction.DirectionToVector();
            //vertBarLeft.transform.position += 0.5f*translate * direction.DirectionToVector();
            //vertBarRight.transform.position += 0.5f*translate * direction.DirectionToVector();

        }
        else if (isActive < 0)
        {
            cubeThree.transform.position += translate * direction.DirectionToVector();
            cubeFour.transform.position += translate * direction.DirectionToVector();
            //vertBarLeft.transform.position += 0.5f * translate * direction.DirectionToVector();
            //vertBarRight.transform.position += 0.5f * translate * direction.DirectionToVector();
        }



        
        


        

        if (transform.position.y >= 480 - 8)
        {
            velocity = -velocity;
        }
        else if (transform.position.y <= 0 + 8 + 16)
        {
            velocity = -velocity;
        }

        if (transform.position.x >= 270  - 8)
        {
            velocity = -velocity;
        }
        else if (transform.position.x <= 0  + 8)
        {
            velocity = -velocity;
        }

    }
}
