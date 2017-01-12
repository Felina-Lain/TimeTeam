using UnityEngine;
using System.Collections;

public class GravityGyro : MonoBehaviour {

	Vector3 grav_ini;
	float tempz;
	public float _speed;

	void Start () {
		grav_ini = Physics.gravity;
		tempz = 0 - (float)System.Math.Round(Input.acceleration.z,1);


	}

	// Update is called once per frame
	void Update () {



		//Physics.gravity = grav_ini + (new Vector3((float)System.Math.Round(Input.gyro.rotationRate.y,2),(float)System.Math.Round(Input.gyro.rotationRate.z,2),(float)System.Math.Round(-Input.gyro.rotationRate.x,2))*_speed);



		Physics.gravity = grav_ini + (new Vector3((float)System.Math.Round(Input.acceleration.x,1),(float)System.Math.Round(Input.acceleration.y,1),(float)System.Math.Round(tempz + Input.acceleration.z,1))*_speed); //(float)System.Math.Round(-Input.acceleration.z,1))*_speed


		print ("gyro y " + System.Math.Round(Input.acceleration.y,1));
		print ("gyro z " + (System.Math.Round(tempz + Input.acceleration.z,1)));
		print ("gyro -x " + System.Math.Round(Input.acceleration.x,1));
	}
}
