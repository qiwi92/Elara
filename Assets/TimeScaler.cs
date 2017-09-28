using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScaler : MonoBehaviour
{
    [Range(0.0001f,10f)]
    public float TimeScale;

	void Update ()
	{
	    if (TimeScale < 0.001f)
	    {
	        TimeScale = 0.001f;
	    }
	    Time.timeScale = TimeScale;
	}
}
