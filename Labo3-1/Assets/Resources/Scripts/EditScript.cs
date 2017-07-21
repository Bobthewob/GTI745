using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditScript : MonoBehaviour {

    private GlowScript glow;

    // Use this for initialization
    void Start () {
        glow = this.transform.Find("Glow Edit").GetComponent<GlowScript>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnMouseUp()
    {
        Manager.Instance.selectedCube = this.transform.parent.transform.parent.gameObject.GetComponent<cubeScript>().cube;
        Manager.Instance.transitionIn = true;
    }

    public void OnMouseEnter()
    {
        glow.EnableLight();
    }

    public void OnMouseExit()
    {
        glow.DisenableLight();
    }

    public void UpdateLight()
    {
        glow.changeRange();
    }
}
