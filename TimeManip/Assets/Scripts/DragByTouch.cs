using UnityEngine;
using System.Collections;

public class DragByTouch : MonoBehaviour {

	public Camera _MainCam;
	private float dist;
	private bool dragging = false;
	private Vector3 offset;
	private Transform toDrag;
	public LayerMask _IgnoreUI;

	void Update() {
		Vector3 v3;

		if (Input.touchCount != 1) {
			dragging = false; 
			return;
		}

		if(Input.GetTouch (0).phase == TouchPhase.Began) {
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch (0).position); 
			if(Physics.Raycast(ray, out hit, Mathf.Infinity, _IgnoreUI) && (hit.collider.tag == "Draggable"))
			{	
				_MainCam.GetComponent<RotateCam> ().enabled = false;
				Debug.Log ("Here");
				toDrag = hit.transform;
				dist = hit.transform.position.z - Camera.main.transform.position.z;
				v3 = new Vector3(Input.GetTouch (0).position.x, Input.GetTouch (0).position.y, dist);
				v3 = Camera.main.ScreenToWorldPoint(v3);
				offset = toDrag.position - v3;
				dragging = true;
			}
		}
		if (dragging && Input.GetTouch (0).phase == TouchPhase.Moved) {
			v3 = new Vector3(Input.mousePosition.x, Input.mousePosition.y, dist);
			v3 = Camera.main.ScreenToWorldPoint(v3);
			toDrag.position = v3 + offset;
		}
		if (dragging && (Input.GetTouch (0).phase == TouchPhase.Ended || Input.GetTouch (0).phase == TouchPhase.Canceled)) {
			dragging = false;
			_MainCam.GetComponent<RotateCam> ().enabled = true;
		}
	}
}