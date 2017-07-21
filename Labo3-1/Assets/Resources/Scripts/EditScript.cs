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
        if (Manager.Instance.cursorType == cursorType.FreeView && !Manager.Instance.transitionIn) {
            Manager.Instance.selectedCube = this.transform.parent.transform.parent.gameObject.GetComponent<cubeScript>().cube;
            Manager.Instance.transitionIn = true;
            GameObject.Find("MusicSource").GetComponent<MusicTest>().StopMusic();
        }
    }

    public void OnMouseEnter()
    {
        if (Manager.Instance.cursorType == cursorType.FreeView && !Manager.Instance.transitionIn)
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
