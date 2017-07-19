using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditCubeNameScript : MonoBehaviour {

	public InputField input;

    public void onClickEditCubeName() {
		Manager.Instance.selectedCube.name = input.text;
    }
}
