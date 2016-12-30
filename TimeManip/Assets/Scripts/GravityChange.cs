using UnityEngine;
using System.Collections;

public class GravityChange : MonoBehaviour {

	private Vector3 gravity_ini;
	public float speed;

	// gravity constant
	public float g = 9.8f;

	// Use this for initialization
	void Start () {
		Input.gyro.enabled = true;
		gravity_ini = Physics.gravity;
	}

	void Update() {
		
		// normalize axis
		Physics.gravity = new Vector3( Input.acceleration.x, Input.acceleration.z,	Input.acceleration.y)*g * speed;

	}
		
}
