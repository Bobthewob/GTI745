using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteScript : MonoBehaviour {

    public Camera camera;

	// Use this for initialization
	void Start () {
		
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
        Component halo = this.gameObject.GetComponent("Halo");
        halo.GetType().GetProperty("enabled").SetValue(halo, true, null);
    }

    public void OnMouseExit()
    {
        Component halo = this.gameObject.GetComponent("Halo");
        halo.GetType().GetProperty("enabled").SetValue(halo, false, null);
    }
}
