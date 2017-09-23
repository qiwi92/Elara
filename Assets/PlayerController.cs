using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets;



public class PlayerController : MonoBehaviour {

    private float velocity = 48f;
    private float pauseTime = 1f;
    private float currentTime = 0f;
    private int x = 1;

    private Direction direction;

    public GameObject CleanedPrefab;

    public GameObject FollowCube;


    void Start () {
        direction = Direction.up;
        transform.position = new Vector3(0, 0, 0);
        FollowCube.transform.position = new Vector3(0, -16, 0);

    }
	
	// Update is called once per frame
	void Update () {
        float t = Time.deltaTime;

        currentTime += Time.deltaTime;

        float translate = t * velocity;

        //Instantiate(CleanedPrefab,transform.position,transform.rotation);


        //transform.RotateAround(cubeOneRef.transform.postion,, 90);

        if (currentTime > pauseTime)
        {
            int decider = UnityEngine.Random.Range(0, 2);
            if (decider == 1)
            {
                direction = direction.TurnLeft();
            }
            else
            {
                direction = direction.TurnRight();
            }
            currentTime = 0f;
        }




        


        float separation = Mathf.Abs(transform.position.y - FollowCube.transform.position.y);
        //Debug.Log(separation);

        if (separation < 16 || separation > 32)
        {
            x = x*-1;
            //UnityEngine.Debug.Log(x);
            
        }
        if (x>0)
        {
            transform.position += translate * direction.DirectionToVector();
        }
        else if(x <0)
        {
            //UnityEngine.Debug.Log(FollowCube.transform.position);
            FollowCube.transform.position = FollowCube.transform.position + translate * direction.DirectionToVector();
        }
        





        if (transform.position.y >= 240 - 8  )
        {
            velocity = -velocity;
        }
        else if (transform.position.y <= -240 + 8 +16 )
        {
            velocity = -velocity;
        }

        if (transform.position.x >= 270 /2 - 8 )
        {
            velocity = -velocity;
        }
        else if (transform.position.x <= -270 / 2 + 8 )
        {
            velocity = -velocity;
        }

        



    }
}
