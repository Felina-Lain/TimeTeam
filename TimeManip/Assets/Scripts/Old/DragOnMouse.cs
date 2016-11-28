using UnityEngine;
using System.Collections;

public class DragOnMouse : MonoBehaviour { 


	Vector3 delta;
	Vector3 startPos;
	int dragState = 0;
	public GameObject maincam;

	private Vector3 targetPosition;
	private Vector3 startPosition;

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

		//check if double mouse down
		if(Input.GetMouseButtonDown(0)&&Input.GetMouseButtonDown(1))
		{
			// check if mouse is in bounds
			if(GetComponent<Collider>().bounds.Contains(mPosWorld))
			{
				dragState = 2;
				startPos = transform.position;
				delta = mPosWorld - transform.position;

			}
		}

		// drag the object if enabled
		if(dragState == 1)
		{
			//turn off some scripts
			if (this.name.Contains ("Cubepatternfixe")) {
				this.GetComponent<MoveToWaypoints> ().enabled = false;
			}
			maincam.GetComponent<RotateCam>().enabled = false;
			//maincam.GetComponent<TimeZone>().enabled = false;
			//record position
			startPosition= Input.mousePosition;
			//move the object and pattern if needed
			if(this.name.Contains("Cubepatternmove")){

				this.transform.parent.transform.position = new Vector3 (mPosWorld.x- delta.x, mPosWorld.y- delta.y, transform.position.z);
				//this.transform.parent.transform.position = mPosWorld- delta;
			}

			// move the object with mouse if normal
			transform.position = new Vector3 (mPosWorld.x- delta.x, mPosWorld.y- delta.y, transform.position.z);
			//transform.position = mPosWorld- delta;
		}

		// drag the object on z if enabled
		if(dragState == 1)
		{
			//turn off some scripts
			if (this.name.Contains ("Cubepatternfixe")) {
				this.GetComponent<MoveToWaypoints> ().enabled = false;
			}
			maincam.GetComponent<RotateCam>().enabled = false;
			//maincam.GetComponent<TimeZone>().enabled = false;
			//record position
			startPosition= Input.mousePosition;
			//move the object and pattern if needed
			if(this.name.Contains("Cubepatternmove")){

				this.transform.parent.transform.position = new Vector3 (transform.parent.transform.position.x, transform.parent.transform.position.y, mPosWorld.z - delta.z);
				//this.transform.parent.transform.position = mPosWorld- delta;
			}

			// move the object with mouse if normal
			transform.position = new Vector3 (transform.position.x, transform.position.y, mPosWorld.z - delta.z);
			//transform.position = mPosWorld- delta;
		}


		// end drag
		if(dragState == 1 | dragState ==2 && Input.GetMouseButtonUp(0))
		{	
			//turn the offed scripts
			if (this.name.Contains ("Cubepatternfixe")) {
				this.GetComponent<MoveToWaypoints> ().enabled = true;
			}
			maincam.GetComponent<RotateCam>().enabled = true;
			//maincam.GetComponent<TimeZone>().enabled = true;

			//throw the item
			targetPosition = Input.mousePosition;
			Vector3 direction = (targetPosition - startPosition);
			//direction.Normalize();
			GetComponent<Rigidbody>().AddForce(direction * 0.1f, ForceMode.Impulse);

			//turn off drag
			dragState = 0;
		}
	}

}