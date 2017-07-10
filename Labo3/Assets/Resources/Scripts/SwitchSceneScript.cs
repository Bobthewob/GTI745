using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchSceneScript : MonoBehaviour {

	public void switchSceneTo2DPartition(){
		SceneManager.LoadSceneAsync("2DPartition", LoadSceneMode.Additive);
	}
}
