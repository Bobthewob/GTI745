using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Global2DPartitionScript : MonoBehaviour {

	public Dropdown dropDown;
	public InputField melodieNameInput;
	public Text melodyNameLabel;

	// Use this for initialization
	void Start () {
		Manager.Instance.cursorType = cursorType.EditMelody;
		dropDown.options.Clear();
		var linqName = Manager.Instance.selectedCube.children.Select (x => x.name);

		dropDown.AddOptions(linqName.ToList());
		melodieNameInput.text = linqName.First();
		melodyNameLabel.text = linqName.First ();

		Manager.Instance.loadUINotes (0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
