using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteCubeScript : MonoBehaviour {

	public GameObject cameraZoom;

	public void onClickDeleteCube(){
		cameraZoom.GetComponent<CameraZoom>().deleteSelectedCube();
	}
}
