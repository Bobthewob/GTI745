using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleControlButton : MonoBehaviour {

	public GameObject button;

	public void setControllerMod()
	{
		Debug.Log ("click");
		if (!Toolbox.Instance.controllerMod)
			button.GetComponentInChildren<Text>().text = "Touch Mode";
		else
			button.GetComponentInChildren<Text>().text = "Accelerometer mode";
		
		Toolbox.Instance.controllerMod = !Toolbox.Instance.controllerMod;
	}
}

