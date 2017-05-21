using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AccelerometerInputField : MonoBehaviour {

	public InputField accelFactorInputField;

	public void setAccelFactor()
	{
		Toolbox.Instance.AccelFactor = float.Parse((accelFactorInputField as InputField).text);
		(accelFactorInputField as InputField).text = Toolbox.Instance.AccelFactor.ToString();
	}
}
