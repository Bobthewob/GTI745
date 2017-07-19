using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnMouseUp()
    {
        Manager.Instance.selectedCube = this.transform.parent.transform.parent.gameObject.GetComponent<cubeScript>().cube;
        Manager.Instance.transitionIn = true;
    }
}
