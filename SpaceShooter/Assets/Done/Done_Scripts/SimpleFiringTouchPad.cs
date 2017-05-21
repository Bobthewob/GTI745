using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class SimpleFiringTouchPad : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {
		
	private bool touched; //mutex sur le doigt
	private int touchID; //mutex sur le doigt

	void awake() {
		touched = false;
	}

	public void OnPointerDown(PointerEventData eventData){
		//initialize la position de depart
		if (!touched) {
			touched = true;
			touchID = eventData.pointerId;
		}
	}

	public void OnPointerUp(PointerEventData eventData){
		if (touchID == eventData.pointerId) {
			touched = false;
		}
	}

	public bool canFire(){
		return touched;
	}
}
