using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetLineTarget : MonoBehaviour {

    public GameObject Target;
    public LineRenderer lineRenderer;

	void Update()
    {
        lineRenderer.SetPosition(1, Target.transform.position);
	}
}
