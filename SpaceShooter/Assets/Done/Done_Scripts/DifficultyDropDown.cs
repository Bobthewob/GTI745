using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyDropDown : MonoBehaviour {

	public Dropdown difficultyDropDown;

	public void setDifficulty(){
		switch (difficultyDropDown.value) {
		case 0:
			Toolbox.Instance.currentDifficulty = Difficulty.Easy;
			break;
		case 1:
			Toolbox.Instance.currentDifficulty = Difficulty.Medium;
			break;
		case 2:
			Toolbox.Instance.currentDifficulty = Difficulty.Hard;
			break;
		}
	}
}
