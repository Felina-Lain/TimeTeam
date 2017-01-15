using UnityEngine;
using System.Collections;

public class GravityGyro : MonoBehaviour {

	Vector3 grav_ini;

	float tempy;
	float tempx;

	float gyrouptmp;

	float gravgyroup;

	public float _speed;
	public float _toPlafond = 100;
	public int UpDown = 1;

	void Awake () {
		Input.gyro.enabled = true;
		grav_ini = Physics.gravity;
		tempy = 0 - (float)System.Math.Round(Input.acceleration.y,1);
		tempx = 0 - (float)System.Math.Round(Input.acceleration.x,1);
		gravgyroup = 0;



	}

	public void ResetGravity(){
		
		tempy = 0 - (float)System.Math.Round(Input.acceleration.y,1);
		tempx = 0 - (float)System.Math.Round(Input.acceleration.x,1);
		gravgyroup = 0;
		Input.gyro.enabled = true;

	}

	// Update is called once per frame
	void Update () {

		//Physics.gravity = grav_ini + (new Vector3((float)System.Math.Round(Input.gyro.rotationRate.y,2),(float)System.Math.Round(Input.gyro.rotationRate.z,2),(float)System.Math.Round(-Input.gyro.rotationRate.x,2))*_speed);


		//gravity tilt
		Physics.gravity = grav_ini + (new Vector3((float)System.Math.Round(tempx + Input.acceleration.x,1),(gravgyroup*3/_speed),(float)System.Math.Round(tempy + Input.acceleration.y,1))*_speed); 

		//check gyro state to switch to ceiling if needed
		gyrouptmp += (float)System.Math.Round (Input.gyro.rotationRate.z);
		if (gyrouptmp > 320f || gyrouptmp < -320f) {
			gyrouptmp = 0f;
		}

		if ( 0f < gyrouptmp && gyrouptmp < 150f) {
			UpDown = 1;
		} else if (0f > gyrouptmp && gyrouptmp > -150f) {
			UpDown = 2;
		} else if ((150f < gyrouptmp || gyrouptmp < -150f) && (float)System.Math.Round (Input.gyro.rotationRate.z) > 0f ) {
			UpDown = 3;
		}else if ((150f < gyrouptmp || gyrouptmp < -150f) && (float)System.Math.Round (Input.gyro.rotationRate.z) < 0f) {
			UpDown = 4;
		}

		switch(UpDown)
		{
		case 4:
			gravgyroup += (float)System.Math.Round (Input.gyro.rotationRate.z);
			break;

		case 3:
			gravgyroup -= (float)System.Math.Round (Input.gyro.rotationRate.z);
			break;

		case 2:
			gravgyroup -= (float)System.Math.Round (Input.gyro.rotationRate.z);
			break;

		case 1:
			gravgyroup += (float)System.Math.Round (Input.gyro.rotationRate.z);

			break;
		default:
			Debug.Log("NOTHING");
			break;
		}

	}

	public void Pinrt(){
	
		print ("gyro " + gravgyroup);
		print ("temp " + gyrouptmp);
		print ("stick " + Physics.gravity.y);
	
	}
}
