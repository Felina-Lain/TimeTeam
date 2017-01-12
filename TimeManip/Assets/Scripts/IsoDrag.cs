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

	public List<Vector3> touchPos = new List<Vector3>();

	public void Start()
	{
		dragState = 0;

	}
		


	void Update()
	{
		// check if we have one touch
		//if(Input.touchCount == 1)
		//{
			// get touch pos on this X-Y plane
			//mPos1 = Input.GetTouch(0).position;

		if(Input.GetMouseButton(0))
     	{			
     	// get touch pos on this X-Y plane
			mPos1 = Input.mousePosition;
			//Camera.main.ScreenToWorldPoint (mPos1.x, mPos1.y, 15);

			mPos1.z = Camera.main.transform.InverseTransformPoint(transform.position).z;
			mPosWorld1 = Camera.main.ScreenToWorldPoint(mPos1);
		
			// check if mouse is in bounds
			if (GetComponent<Collider> ().bounds.Contains (mPosWorld1) && this.GetComponent<CubeManager> ().myGroup != CubeGroups.Boing) {
				dragState = 1;
				startPos = transform.position;
				delta = mPosWorld1 - transform.position;
		
			}
			else if(GetComponent<Collider> ().bounds.Contains (mPosWorld1) && this.GetComponent<CubeManager> ().myGroup == CubeGroups.Boing){
				
				Vector3 direction = transform.position - Camera.main.transform.position;
				direction.Normalize ();
				GetComponent<Rigidbody> ().AddForce (direction * 200f, ForceMode.Impulse);
			
				}
		}

		// drag the object if enabled
		if(dragState == 1)
		{
			this.GetComponent<Rigidbody> ().isKinematic = true;
			this.GetComponent<Rigidbody> ().useGravity = false;
			//turn off some scripts
			if (this.GetComponent<CubeManager>().myGroup == CubeGroups.Red ||this.GetComponent<CubeManager>().myGroup == CubeGroups.Green) {
				this.GetComponent<MoveToWaypoints> ().enabled = false;
			}
			//move the object and pattern if needed
			if(this.GetComponent<CubeManager>().myGroup == CubeGroups.Green){

				this.transform.parent.transform.position = new Vector3 (mPosWorld1.x - delta.x, transform.parent.transform.position.y , mPosWorld1.z - delta.z);

			}

			// move the object with mouse if normal
			this.transform.position = new Vector3 (mPosWorld1.x - delta.x , mPosWorld1.y - delta.y, mPosWorld1.z - delta.z);

			//store touch position for throw
			touchPos.Add(mPosWorld1);

		}
			
			


		// end drag
		if(dragState == 1 && Input.GetMouseButtonUp(0))
		{	
			//throw the item 
			if (this.GetComponent<CubeManager> ().myGroup != CubeGroups.Black) {
				this.GetComponent<Rigidbody> ().isKinematic = false;
				this.GetComponent<Rigidbody> ().useGravity = true;
			}

			if (touchPos.Count > 11) {
				Vector3 direction = (touchPos [touchPos.Count - 1] - touchPos [touchPos.Count - 10]);
				direction.Normalize ();
				this.GetComponent<Rigidbody> ().AddForce (direction * 700f, ForceMode.Impulse);
			}

			//turn off drag
			dragState = 0;

			//turn the offed scripts
			if (this.GetComponent<CubeManager>().myGroup == CubeGroups.Red ||this.GetComponent<CubeManager>().myGroup == CubeGroups.Green){
				this.GetComponent<MoveToWaypoints> ().enabled = true;
				}
			touchPos = new List<Vector3>();
		}
	}

}
