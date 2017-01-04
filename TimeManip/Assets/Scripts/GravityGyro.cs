using UnityEngine;
using System.Collections;

public class GravityGyro : MonoBehaviour {

	Vector3 grav_ini;
	Vector3 rot_ini;
	public float _speed;

	void Start () {
		grav_ini = Physics.gravity;
		rot_ini = Input.gyro.rotationRate;
		//SensorFusion.Recenter();

	}

	// Update is called once per frame
	void Update () {

		Vector3 min5 = grav_ini - ((grav_ini * 5) / 100);
		Vector3 plus5 = grav_ini + ((grav_ini * 5) / 100);

		Physics.gravity = grav_ini + (new Vector3(Input.gyro.rotationRate.y,Input.gyro.rotationRate.z,-Input.gyro.rotationRate.x)*_speed);
		if (min5 == Physics.gravity || Physics.gravity == plus5) {
			Physics.gravity = grav_ini;
		}
		//Physics.gravity = grav_ini + (new Vector3(SensorFusion.GetOrientation().eulerAngles.x, 0, 0)) *_speed;
	}
}
