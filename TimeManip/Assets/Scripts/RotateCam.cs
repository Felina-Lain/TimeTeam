using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class RotateCam : MonoBehaviour {
	
	private Vector3 firstpoint; //change type on Vector3
	private Vector3 secondpoint;
	private float xAngle = 0.0f; //angle for axes x for rotation
	private float yAngle = 0.0f;
	private float xAngTemp = 0.0f; //temp variable for angle
	private float yAngTemp = 0.0f;
	public bool _Reverse = false;
	public LayerMask _IgnoreUI;

	void Start() {
		//Initialization our angles of camera
		xAngle = 0.0f;
		yAngle = 0.0f;
		this.transform.rotation = Quaternion.Euler(yAngle, xAngle, 0.0f);
	}

	private bool IsPointerOverUIObject() {
		PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
		eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
		List<RaycastResult> results = new List<RaycastResult>();
		EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
		return results.Count > 0;
	}

	void Update() {
		//Check count touches
		if (Input.touchCount > 0) {
			//check if touch the slider (not working for now)

				//Touch began, save position
				if (Input.GetTouch (0).phase == TouchPhase.Began) {
					firstpoint = Input.GetTouch (0).position;
					xAngTemp = xAngle;
					yAngTemp = yAngle;
				}
				//Move finger by screen
				if (Input.GetTouch (0).phase == TouchPhase.Moved) {
					secondpoint = Input.GetTouch (0).position;
					//Mainly, about rotate camera. For example, for Screen.width rotate on 180 degree
					xAngle = xAngTemp + (secondpoint.x - firstpoint.x) * 180.0f / Screen.width;
					yAngle = yAngTemp - (secondpoint.y - firstpoint.y) * 90.0f / Screen.height;
					//Rotate camera
					if (!_Reverse) {
						this.transform.rotation = Quaternion.Euler (yAngle, xAngle, 0.0f);
					} else {
						this.transform.rotation = Quaternion.Euler (-yAngle, xAngle, 0.0f);
					}
				}
			}
	}

	public void ChangeReverse(bool reverse){

		_Reverse = reverse;

	}
}