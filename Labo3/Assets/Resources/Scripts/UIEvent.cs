using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEvent : MonoBehaviour {

    public Button button;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void onClickCreateNewCube() {
        if (Manager.Instance.cursorType == cursorType.CreateCube)
        {
            Manager.Instance.cursorType = cursorType.FreeView;
            button.image.color = new Color(1f, 1f, 1f);
        }
        else {
            Manager.Instance.cursorType = cursorType.CreateCube;
            button.image.color = new Color(0.65f, 0.65f, 0.65f);
        }
    }

	public void onClickPlayMode() {
		if (Manager.Instance.cursorType == cursorType.Play)
		{
			Manager.Instance.cursorType = cursorType.FreeView;
			button.image.color = new Color(1f, 1f, 1f);
		}
		else {
            Debug.Log("PlayMode");
			Manager.Instance.cursorType = cursorType.Play;
			button.image.color = new Color(0.65f, 0.65f, 0.65f);
		}
	}

    public void onClickEditCube()
    {
        if (Manager.Instance.cursorType == cursorType.MergeCube)
        {
            Manager.Instance.cursorType = cursorType.FreeView;
            button.image.color = new Color(1f, 1f, 1f);
        }
        else
        {
            Manager.Instance.cursorType = cursorType.MergeCube;
            button.image.color = new Color(0.65f, 0.65f, 0.65f);
        }
    }
}
