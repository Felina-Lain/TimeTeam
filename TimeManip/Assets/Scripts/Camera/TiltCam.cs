using UnityEngine;
using System.Collections;

public class TiltCam : MonoBehaviour {

	public float speed;
	private Quaternion rot_ini;

	// Use this for initialization
	void Start () {
		Input.gyro.enabled = true;
		rot_ini = transform.rotation;
	}
	
	//void FixedUpdate(){
	//	//Vector3 movement = new Vector3 (Input.acceleration.x, 0.0f, 0.0f);
	//	//GetComponent<Rigidbody>().velocity = movement * speed;
	//
	//	Vector3 dir = Vector3.zero;
	//	dir.y = Input.acceleration.x;
	//	dir.x = Input.acceleration.y;
	//	//if (dir.sqrMagnitude > 1)
	//	//	dir.Normalize();
	//
	//	dir *= Time.deltaTime * speed;
	//
	//	transform.Rotate (dir);
	//}

	void FixedUpdate(){
	
	
		//transform.rotation = Quaternion.Euler(new Vector3(Input.acceleration.x, Input.acceleration.z,Input.acceleration.y) * speed);
		transform.Rotate (new Vector3(Input.acceleration.x, Input.acceleration.z,Input.acceleration.y) * speed);
	
	}

}
