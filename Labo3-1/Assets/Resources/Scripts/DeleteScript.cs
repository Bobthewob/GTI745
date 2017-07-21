using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteScript : MonoBehaviour {
    
    private GlowScript glow;

    // Use this for initialization
    void Start () {
        glow = this.transform.Find("Glow Delete").GetComponent<GlowScript>();
    }
	
	// Update is called once per frame
	void Update () {
    }

    public void OnMouseDown()
    {
        if (Manager.Instance.cursorType == cursorType.FreeView)
        {
            Manager.Instance.rootCubes.Remove(this.transform.parent.transform.parent.gameObject.GetComponent<cubeScript>().cube);
            Manager.Instance.selectedCube = null;
            GameObject.Destroy(this.transform.parent.transform.parent.gameObject);
        }
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
