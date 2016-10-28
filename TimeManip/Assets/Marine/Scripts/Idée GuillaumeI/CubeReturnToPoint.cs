using UnityEngine;
using System.Collections;

public class CubeReturnToPoint : MonoBehaviour {
	
	public Transform target;
	public float speed;
	public float currentspeed;

	void Start (){

		currentspeed = speed;
	}


	void Update() {
		
		float step = currentspeed * Time.deltaTime;
		transform.position = Vector3.MoveTowards (transform.position, target.position, step);

	}

	void OnTriggerEnter (Collider _col){


		if (_col.gameObject.name == "Target"){
			GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
			currentspeed = 0;
		}
	}

	void OnTriggerExit (Collider _col){

	
		if(_col.gameObject.name == "Target")
			currentspeed = speed;
	}
}