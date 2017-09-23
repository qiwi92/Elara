using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullCubeContoller : MonoBehaviour {

    public GameObject cubeOneRef;

    private float pauseTime = 2f;
    private float currentTime = 0f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        currentTime += Time.deltaTime;


        if (currentTime > pauseTime)
        {
            transform.RotateAround(cubeOneRef.transform.position,new Vector3 (0,0,1),90);
            currentTime = 0f;
        }
        
    }
}
