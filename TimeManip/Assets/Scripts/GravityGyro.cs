using UnityEngine;
using System.Collections;

public class GravityGyro : MonoBehaviour {

	Vector3 grav_ini;
	public float _speed;

	void Start () {
		grav_ini = Physics.gravity;
		SensorFusion.Recenter();

	}

	// Update is called once per frame
	void Update () {
		Physics.gravity = grav_ini + (new Vector3(Input.gyro.rotationRate.y/5,Input.gyro.rotationRate.z/5,-Input.gyro.rotationRate.x/5)*_speed);
		//Physics.gravity = grav_ini + (new Vector3(SensorFusion.GetOrientation().eulerAngles.x, 0, 0)) *_speed;
	}
}
