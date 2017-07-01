using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditCubeNameScript : MonoBehaviour {

    public Text text;

    public void onClickEditCubeName() {
        Manager.Instance.selectedCube.name = text.text;
    }
}
