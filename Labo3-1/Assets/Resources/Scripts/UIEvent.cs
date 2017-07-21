﻿using System.Collections;
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
		MusicTest musicScript = GameObject.Find ("MusicSource").GetComponent<MusicTest>();

        //if ((int)musicScript.PlaySong == -1) {
        //	musicScript.PlaySong = MusicTest.MusicPlayer.All3D;
        //}

        musicScript.startMusicAll3D();

		/*if (Manager.Instance.cursorType == cursorType.Play)
		{
			Manager.Instance.cursorType = cursorType.FreeView;
			button.image.color = new Color(1f, 1f, 1f);
		}
		else {
            Debug.Log("PlayMode");
			Manager.Instance.cursorType = cursorType.Play;
			button.image.color = new Color(0.65f, 0.65f, 0.65f);
		}*/
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

	public void onClickMoveCube()
	{
		if (Manager.Instance.cursorType == cursorType.MoveCube)
		{
			Manager.Instance.cursorType = cursorType.FreeView;
			button.image.color = new Color(1f, 1f, 1f);
		}
		else
		{
			Manager.Instance.cursorType = cursorType.MoveCube;
			button.image.color = new Color(0.65f, 0.65f, 0.65f);
		}
	}

    public void onClickCloseModalHelp()
    {
        GameObject.Find("FirstCanvas").transform.Find("Help").gameObject.SetActive(false);
    }

    public void onClickOpenModalHelp()
    {
        GameObject.Find("FirstCanvas").transform.Find("Help").gameObject.SetActive(true);
    }

    public void onClickQuit()
    {
        Debug.Log("test");
        Application.Quit();
    }
}
