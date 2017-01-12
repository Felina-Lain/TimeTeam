using UnityEngine;
using System.Collections;

public class GravityGyro : MonoBehaviour {

	Vector3 grav_ini;

	float tempy;
	float tempx;

	float gyrouptmp;

	float gravgyroup;

	public float _speed;
	public float _toPlafond = 20;

	void Start () {
		grav_ini = Physics.gravity;
		tempy = 0 - (float)System.Math.Round(Input.acceleration.y,1);
		tempx = 0 - (float)System.Math.Round(Input.acceleration.x,1);



	}

	// Update is called once per frame
	void Update () {



		//Physics.gravity = grav_ini + (new Vector3((float)System.Math.Round(Input.gyro.rotationRate.y,2),(float)System.Math.Round(Input.gyro.rotationRate.z,2),(float)System.Math.Round(-Input.gyro.rotationRate.x,2))*_speed);


		//gravity tilt
		Physics.gravity = grav_ini + (new Vector3((float)System.Math.Round(tempx + Input.acceleration.x,1),gravgyroup/_toPlafond,(float)System.Math.Round(tempy + Input.acceleration.y,1))*_speed); 

		//turn off gravity to stick to ceiling if the gyro says so
		if(gyrouptmp != (float)System.Math.Round(Input.gyro.rotationRate.z,2)){

			gravgyroup += (float)System.Math.Round (Input.gyro.rotationRate.z, 2);
			gyrouptmp = (float)System.Math.Round (Input.gyro.rotationRate.z, 2);


		}

		//print ("gyro y " + (float)System.Math.Round(tempgyroy + Input.gyro.rotationRate.z,2));
		//print ("tilt y " + System.Math.Round(tempy + Input.acceleration.y,1));
		//print ("tilt  x " + System.Math.Round(tempx + Input.acceleration.x,1));
	}
}
