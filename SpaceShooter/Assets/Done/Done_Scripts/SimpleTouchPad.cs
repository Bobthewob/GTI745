using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class SimpleTouchPad : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler {

	public float smoothing;

	private Vector2 origin;
	private Vector2 direction;
	private Vector2 smoothDirection;
	private bool touched; //mutex sur le doigt
	private int touchID; //mutex sur le doigt

	void awake() {
		direction = Vector2.zero;
		touched = false;
	}

	public void OnPointerDown(PointerEventData eventData){
		//initialize la position de depart
		if (!touched) {
			touched = true;
			touchID = eventData.pointerId;
			origin = eventData.position;
		}
	}

	public void OnPointerUp(PointerEventData eventData){
		if (touchID == eventData.pointerId) {
			direction = Vector2.zero;
			touched = false;
		}
	}

	public void OnDrag(PointerEventData eventData){
		//compare la position du debut et la position courante
		if (touchID == eventData.pointerId) {
			Vector2 directionRaw = eventData.position - origin;
			direction = directionRaw.normalized; 
		}
	}

	public Vector2 getDirection(){
		if (!Toolbox.Instance.inGame)
			smoothDirection = Vector3.zero;

		smoothDirection = Vector2.MoveTowards(smoothDirection, direction, smoothing);
		return smoothDirection;
	}
}
