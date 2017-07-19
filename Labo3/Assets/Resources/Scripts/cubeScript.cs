using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubeScript : MonoBehaviour {

    public CubeParent cube;

	// Use this for initialization
	void Start () {
        cube = new CubeParent(Manager.Instance.getUniqueCubeName(), gameObject);
        Manager.Instance.rootCubes.Add(cube);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
