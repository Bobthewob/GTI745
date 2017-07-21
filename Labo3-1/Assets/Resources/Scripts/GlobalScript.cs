using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalScript : MonoBehaviour {

    public InputField CubeNameInput;
    public Button EditMelodies;
	public Button CreateStars;
	public Button MergeStars;
	public Button MoveStars;
	public Button DeleteStars;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Manager.Instance.selectedCube == null)
        {
            CubeNameInput.gameObject.SetActive(false);
        }
        else
        {
            CubeNameInput.gameObject.SetActive(true);
        }

		switch (Manager.Instance.cursorType) {
		case cursorType.CreateCube:
			MergeStars.image.color = new Color(1f, 1f, 1f);
			MoveStars.image.color = new Color(1f, 1f, 1f);
			break;
		case cursorType.MergeCube:
			CreateStars.image.color = new Color(1f, 1f, 1f);
			MoveStars.image.color = new Color(1f, 1f, 1f);
			break;
		case cursorType.MoveCube:
			CreateStars.image.color = new Color(1f, 1f, 1f);
			MergeStars.image.color = new Color(1f, 1f, 1f);
			break;
		}
	}
}
