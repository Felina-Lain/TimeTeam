using UnityEngine;
using System.Collections;

public class PinchZoom : MonoBehaviour {
	public float perspectiveZoom = 0.8f;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if (Input.touchCount == 2)
		{
			// Store les touches
			Touch touchZero = Input.GetTouch (0);
			Touch touchOne = Input.GetTouch (1);		

			Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
			Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition; 

			float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude; 
			float touchDeltaMag = (touchZero.position - touchOne.position).magnitude; 

			float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag; 

			GetComponent<Camera>().fieldOfView += deltaMagnitudeDiff * perspectiveZoom;
			GetComponent<Camera>().fieldOfView = Mathf.Clamp (GetComponent<Camera>().fieldOfView, 10f, 60f); 
		}


	}
}
