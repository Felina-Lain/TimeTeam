using UnityEngine;
using System.Collections;

public class GravityGyro : MonoBehaviour {

	Vector3 grav_ini;
	public float _speed;

	void Start () {
		grav_ini = Physics.gravity;
	}

	// Update is called once per frame
	void Update () {
		Physics.gravity = grav_ini + (new Vector3(Input.gyro.rotationRate.y,Input.gyro.rotationRate.z,-Input.gyro.rotationRate.x)*_speed);
	}
}
