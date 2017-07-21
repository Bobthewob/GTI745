using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowScript : MonoBehaviour {

    public float defaultRange = 1.5f;
    private float range;
    private Light glow;

	// Use this for initialization
	void Start () {
        glow = GetComponent<Light>();
        range = defaultRange;
        this.DisenableLight();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void DisenableLight()
    {
        glow.range = 0;
    }

    public void EnableLight()
    {
        glow.range = range;
    }

    public void changeRange()
    {
        range = range + defaultRange;
    }
}
