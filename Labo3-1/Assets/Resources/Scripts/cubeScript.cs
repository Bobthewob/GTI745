using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubeScript : MonoBehaviour {

    public CubeParent cube;
    public static int menuStatus = 0;
    public bool isPlaying = false;

    // Use this for initialization
    void Start () {
        cube = new CubeParent(Manager.Instance.getUniqueCubeName(), gameObject);
        Manager.Instance.rootCubes.Add(cube);
    }
	
	// Update is called once per frame
	void Update () {
       
	}

    public void OnMouseEnter()
    {
        GameObject glow = this.transform.Find("Glow Red").gameObject;
        glow.SetActive(true);
    }

    public void OnMouseExit()
    {
        GameObject glow = this.transform.Find("Glow Red").gameObject;
        glow.SetActive(false);
    }
}
