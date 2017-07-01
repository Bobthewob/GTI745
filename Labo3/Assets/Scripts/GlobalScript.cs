using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalScript : MonoBehaviour {

    public InputField CubeNameInput;
    public Button EditCubeName;
    public Button EditMelodies;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Manager.Instance.selectedCube == null)
        {
            CubeNameInput.gameObject.SetActive(false);
            EditMelodies.gameObject.SetActive(false);
            EditCubeName.gameObject.SetActive(false);
        }
        else
        {
            CubeNameInput.gameObject.SetActive(true);
            EditMelodies.gameObject.SetActive(true);
            EditCubeName.gameObject.SetActive(true);
        }
	}
}
