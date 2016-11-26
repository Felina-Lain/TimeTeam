using UnityEngine;
using System.Collections;

public class DragOnMouse : MonoBehaviour { 


	Vector3 delta;
	Vector3 startPos;
	int dragState = 0;
	public GameObject maincam;


	public void Start()
	{
		dragState = 0;
		maincam = GameObject.FindWithTag ("CameraPivot");

	}

	void Update()
	{
		// get mouse pos on this X-Y plane
		Vector3 mPos = Input.mousePosition;
		mPos.z = Camera.main.transform.InverseTransformPoint(transform.position).z;
		Vector3 mPosWorld = Camera.main.ScreenToWorldPoint(mPos);

		// check if we have a mouse down
		if(Input.GetMouseButtonDown(0))
		{
			// check if mouse is in bounds
			if(GetComponent<Collider>().bounds.Contains(mPosWorld))
			{
				dragState = 1;
				startPos = transform.position;
				delta = mPosWorld - transform.position;

			}
		}

		// drag the object if enabled
		if(dragState == 1)
		{
			//turn off some scripts
			this.GetComponent<MoveToWaypoints>().enabled = false;
			maincam.GetComponent<RotateCam>().enabled = false;
			maincam.GetComponent<TimeZone>().enabled = false;
			// move the object with mouse
			transform.position = mPosWorld - delta;
		}

		// end drag
		if(dragState == 1 && Input.GetMouseButtonUp(0))
		{	
			//turn the offed scripts
			this.GetComponent<MoveToWaypoints>().enabled = true;
			maincam.GetComponent<RotateCam>().enabled = true;
			maincam.GetComponent<TimeZone>().enabled = true;
			//turn off drag
			dragState = 0;
		}
	}
}