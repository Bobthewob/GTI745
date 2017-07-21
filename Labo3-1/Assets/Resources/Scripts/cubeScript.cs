using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubeScript : MonoBehaviour {

    public CubeParent cube;
    public static int menuStatus = 0;
    public bool isPlaying = false;

    private GlowScript red;
    private GlowScript yellow;

    // Use this for initialization
    void Start () {
        cube = new CubeParent(Manager.Instance.getUniqueCubeName(), gameObject);
        Manager.Instance.rootCubes.Add(cube);

        red = this.transform.Find("Glow Red").GetComponent<GlowScript>();
        yellow = this.transform.Find("Glow Yellow").GetComponent<GlowScript>();
    }
	
	// Update is called once per frame
	void Update () {
       
	}

    public void OnMouseEnter()
    {
        red.EnableLight();
    }

    public void OnMouseExit()
    {
        red.DisenableLight();
    }

    public void UpdateLight()
    {
        GlowScript bluePlay = this.transform.Find("Options").transform.Find("Play").transform.Find("Glow Play").GetComponent<GlowScript>();
        GlowScript blueDelete = this.transform.Find("Options").transform.Find("Delete").transform.Find("Glow Delete").GetComponent<GlowScript>();
        GlowScript blueEdit = this.transform.Find("Options").transform.Find("Edit").transform.Find("Glow Edit").GetComponent<GlowScript>();

        bluePlay.changeRange();
        blueDelete.changeRange();
        blueEdit.changeRange();

        red.changeRange();
        yellow.changeRange();
    }
}
