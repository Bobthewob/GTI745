using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;
using System.Threading;

public class Event2DPartitionScript : MonoBehaviour {

	public Dropdown dropDown;
	public InputField melodieNameInput;
	public Text melodyNameLabel;

    public void onClickSaveButton(){		
		Manager.Instance.selectedCube.children[dropDown.value].name = melodieNameInput.text;
		dropDown.options [dropDown.value].text = melodieNameInput.text;
		melodyNameLabel.text = melodieNameInput.text;
		dropDown.RefreshShownValue ();
	}

	public void onClickReturnButton(){		
		Manager.Instance.cursorType = cursorType.FreeView;
		SceneManager.UnloadSceneAsync ("2dPartition");
	}

	public void onChangedEventDropDown(){
		melodieNameInput.text = dropDown.options [dropDown.value].text;
		melodyNameLabel.text = dropDown.options [dropDown.value].text;

		Manager.Instance.clearUINotes ();
		Manager.Instance.loadUINotes (dropDown.value);
	}	

	public void onClickPlaySelectedButton(){
		Debug.Log ("Press play selected");
        GameObject.Find("MusicSource").GetComponent<MusicTest>().PlaySong = 0;
    }

    public void onClickPlayAllButton()
    {
        Debug.Log("Press play selected");
        //Manager.Instance.playSong = true;
        GameObject.Find("MusicSource").GetComponent<MusicTest>().PlaySong = 1;
    }

    public void onClickClearSheet(){
	    var partition = Manager.Instance.selectedCube.children [dropDown.value].partition;

	    for (int i = 0; i < partition.Count() - 1; i++) {
		    partition [i] = 255;
	    }

	    Manager.Instance.clearUINotes ();
	}
}
