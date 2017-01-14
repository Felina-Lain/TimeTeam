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
	private int UpDown = 1;

	void Awake () {
		grav_ini = Physics.gravity;
		tempy = 0 - (float)System.Math.Round(Input.acceleration.y,1);
		tempx = 0 - (float)System.Math.Round(Input.acceleration.x,1);
		gravgyroup = 0;



	}

	public void ResetGravity(){
	
		tempy = 0 - (float)System.Math.Round(Input.acceleration.y,1);
		tempx = 0 - (float)System.Math.Round(Input.acceleration.x,1);
		gravgyroup = 0;

	}

	// Update is called once per frame
	void Update () {

		//Physics.gravity = grav_ini + (new Vector3((float)System.Math.Round(Input.gyro.rotationRate.y,2),(float)System.Math.Round(Input.gyro.rotationRate.z,2),(float)System.Math.Round(-Input.gyro.rotationRate.x,2))*_speed);


		//gravity tilt
		Physics.gravity = grav_ini + (new Vector3((float)System.Math.Round(tempx + Input.acceleration.x,1),gravgyroup/_toPlafond,(float)System.Math.Round(tempy + Input.acceleration.y,1))*_speed); 

		//turn off gravity to stick to ceiling if the gyro says so
		//if(gyrouptmp != (float)System.Math.Round(Input.gyro.rotationRate.z)){
		//
		//	gravgyroup += (float)System.Math.Round (Input.gyro.rotationRate.z, 1);
		//	gyrouptmp = (float)System.Math.Round (Input.gyro.rotationRate.z, 1);
		//
		//
		//}

		switch(UpDown)
		{
		case 2:
			gravgyroup -= Mathf.Abs ((float)System.Math.Round (Input.gyro.rotationRate.z, 1));
			//print ("case2");
			if (gravgyroup <= -50f) {UpDown = 1;} 
			break;
		case 1:
			gravgyroup += Mathf.Abs ((float)System.Math.Round (Input.gyro.rotationRate.z, 1));
			//print ("case1");
			if (Mathf.Abs (gravgyroup) >= 150f) {UpDown = 2;} 
			break;
		default:
			Debug.Log("NOTHING");
			break;
		}

	}

	public void Pinrt(){
	
		print ("gyro " + gravgyroup);
	
	}
}
