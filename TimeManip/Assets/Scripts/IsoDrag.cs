using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class IsoDrag : MonoBehaviour {
	
	Vector3 delta;
	Vector3 startPos;
	int dragState = 0;

	private Vector3 mPos1;
	private Vector3 mPosWorld1;

	public Vector3[] touchPos = new Vector3[3];
	[SerializeField]
	private int touchTip;

	public void Start()
	{
		dragState = 0;
		touchTip = 0;

	}

	void Update()
	{
		// check if we have one touch
		//if(Input.touchCount == 1)
		//{
		//	// get touch pos on this X-Y plane
		//	mPos1 = Input.GetTouch(0).position;

		if(Input.GetMouseButton(0))
     	{
			
     	// get touch pos on this X-Y plane
			mPos1 = Input.mousePosition;
			mPos1.z = Camera.main.transform.InverseTransformPoint(transform.position).z;
			mPosWorld1 = Camera.main.ScreenToWorldPoint(mPos1);

			// check if mouse is in bounds
			if(GetComponent<Collider>().bounds.Contains(mPosWorld1))
			{
				dragState = 1;
				startPos = transform.position;
				delta = mPosWorld1 - transform.position;

			}
		}

		// drag the object if enabled
		if(dragState == 1)
		{
			GetComponent<Rigidbody> ().isKinematic = true;
			//turn off some scripts
			if (this.name.Contains ("Cubepattern")) {
				this.GetComponent<MoveToWaypoints> ().enabled = false;
			}
			//move the object and pattern if needed
			if(this.name.Contains("Cubepatternmove")){

				this.transform.parent.transform.position = new Vector3 (mPosWorld1.x - delta.x, transform.parent.transform.position.y , mPosWorld1.z - delta.z);

			}

			// move the object with mouse if normal
			transform.position = new Vector3 (mPosWorld1.x - delta.x , transform.position.y , mPosWorld1.z - delta.z);


			//store touch position for throw
			touchPos[touchTip] = mPosWorld1;
			touchTip++;
			if (touchTip > 2) {
				touchTip = 0;}

		}
			
			


		// end drag
		if(dragState == 1 && Input.GetMouseButtonUp(0))
		{	
			//throw the item
			if(!this.name.Contains ("fixe")) 
			GetComponent<Rigidbody> ().isKinematic = false;

			Vector3 direction = (touchPos[2] - touchPos[0]);
			direction.Normalize();
			GetComponent<Rigidbody>().AddForce(direction * 500f, ForceMode.Impulse);

			//turn off drag
			dragState = 0;

			//turn the offed scripts
			if (this.name.Contains ("Cubepattern")) {
				this.GetComponent<MoveToWaypoints> ().enabled = true;
				}
			for ( int i = 0; i < touchPos.Length; i++)
			{
				touchPos[i] = new Vector3(0,0,0);
			}
		}
	}

}
