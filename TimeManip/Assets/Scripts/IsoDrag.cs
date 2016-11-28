using UnityEngine;
using System.Collections;

public class IsoDrag : MonoBehaviour {
	
	Vector3 delta;
	Vector3 startPos;
	int dragState = 0;

	private Vector3 targetPosition;
	private Vector3 startPosition;
	private Vector3 mPos;
	private Vector3 mPosWorld;

	public void Start()
	{
		dragState = 0;

	}

	void Update()
	{


		// check if we have a mouse down
		if(Input.touchCount == 1)
		{
			// get mouse pos on this X-Y plane
			mPos = Input.GetTouch(0).position;
			mPos.z = Camera.main.transform.InverseTransformPoint(transform.position).z;
			mPosWorld = Camera.main.ScreenToWorldPoint(mPos);

			// check if mouse is in bounds
			if(GetComponent<Collider>().bounds.Contains(mPosWorld))
			{
				dragState = 1;
				startPos = transform.position;
				delta = mPosWorld - transform.position;

				if (Input.touchCount == 2) {
					dragState = 2;
				}

			}
		}

		// drag the object if enabled
		if(dragState == 1)
		{
			//turn off some scripts
			if (this.name.Contains ("Cubepatternfixe")) {
				this.GetComponent<MoveToWaypoints> ().enabled = false;
			}
			//record position
			startPosition= Input.GetTouch(0).position;
			//move the object and pattern if needed
			if(this.name.Contains("Cubepatternmove")){

				this.transform.parent.transform.position = new Vector3 (mPosWorld.x - delta.x, transform.parent.transform.position.y, mPosWorld.z - delta.z);

			}

			// move the object with mouse if normal
			transform.position = new Vector3 (mPosWorld.x - delta.x, transform.position.y, mPosWorld.z - delta.z);

		}

		// drag the object on z if enabled
		if(dragState == 2)
		{
			//turn off some scripts
			if (this.name.Contains ("Cubepatternfixe")) {
				this.GetComponent<MoveToWaypoints> ().enabled = false;
			}
			//record position
			startPosition= Input.GetTouch(0).position;
			//move the object and pattern if needed
			if(this.name.Contains("Cubepatternmove")){

				this.transform.parent.transform.position = new Vector3 (transform.parent.transform.position.x, transform.parent.transform.position.y, mPosWorld.z - delta.z);

			}

			// move the object with mouse if normal
			transform.position = new Vector3 (transform.position.x, transform.position.y, mPosWorld.z - delta.z);

		}


		// end drag
		if(dragState == 1 | dragState ==2 && Input.GetMouseButtonUp(0))
		{	
			//turn the offed scripts
			if (this.name.Contains ("Cubepatternfixe")) {
				this.GetComponent<MoveToWaypoints> ().enabled = true;
			}

			//throw the item
			//targetPosition = Input.GetTouch(0).position;
			//Vector3 direction = (targetPosition - startPosition);
			//direction.Normalize();
			//GetComponent<Rigidbody>().AddForce(direction * 0.1f, ForceMode.Impulse);

			//turn off drag
			dragState = 0;
		}
	}

}
