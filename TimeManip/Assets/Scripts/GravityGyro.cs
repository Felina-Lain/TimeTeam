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
	public int UpDown = 0;

	void Awake () {
		Input.gyro.enabled = true;
		grav_ini = Physics.gravity;
		tempy = 0 - (float)System.Math.Round(Input.acceleration.y,1);
		tempx = 0 - (float)System.Math.Round(Input.acceleration.x,1);
		gyrouptmp = (float)System.Math.Round (Input.gyro.rotationRate.z);
		gravgyroup = 0;
		UpDown = 0;



	}

	public void ResetGravity(){
		
		tempy = 0 - (float)System.Math.Round(Input.acceleration.y,1);
		tempx = 0 - (float)System.Math.Round(Input.acceleration.x,1);
		gyrouptmp = (float)System.Math.Round (Input.gyro.rotationRate.z);
		gravgyroup = 0;
		UpDown = 0;
		Input.gyro.enabled = true;

	}

	// Update is called once per frame
	void Update () {

		print ("gyro " + gravgyroup);

		print ("direction " + gyrouptmp);

		print ("gravity " + Physics.gravity.y);

		//Physics.gravity = grav_ini + (new Vector3((float)System.Math.Round(Input.gyro.rotationRate.y,2),(float)System.Math.Round(Input.gyro.rotationRate.z,2),(float)System.Math.Round(-Input.gyro.rotationRate.x,2))*_speed);


		//gravity tilt
		Physics.gravity = grav_ini + (new Vector3((float)System.Math.Round(tempx + Input.acceleration.x,1),(gravgyroup),(float)System.Math.Round(tempy + Input.acceleration.y,1))*_speed); 

	

		//check if we moved the gyro
		if (gyrouptmp != (float)System.Math.Round (Input.gyro.rotationRate.z)) {			
			//check if we're up or down already
			if (0f > Physics.gravity.y && Physics.gravity.y >= -150f) {
				//check what direction we're moving the gyro in

				if ((float)System.Math.Round (Input.gyro.rotationRate.z) > 0 && gyrouptmp > 0) {//we're moving always to the left
					
					UpDown = 1;

				} else if ((float)System.Math.Round (Input.gyro.rotationRate.z) < 0 && gyrouptmp < 0) { //moving always to the right
					
					UpDown = 1;

				} else if ((float)System.Math.Round (Input.gyro.rotationRate.z) < 0 && gyrouptmp > 0 && (float)System.Math.Round (Input.gyro.rotationRate.z) != 0 ) { //moving to the right then changing to the left
					while((float)System.Math.Round (Input.gyro.rotationRate.z) != 0){
						UpDown = 2;}

				} else if ((float)System.Math.Round (Input.gyro.rotationRate.z) > 0 && gyrouptmp < 0 && (float)System.Math.Round (Input.gyro.rotationRate.z) != 0) { //moving always to the left then changing to the right
					while((float)System.Math.Round (Input.gyro.rotationRate.z) != 0){
						UpDown = 2;}
				}

			} else if (0f < Physics.gravity.y && Physics.gravity.y <= 150f) {
				//check what direction we're moving the gyro in

				if ((float)System.Math.Round (Input.gyro.rotationRate.z) > 0 && gyrouptmp > 0) {//we're moving always to the left

					UpDown = 2;

				} else if ((float)System.Math.Round (Input.gyro.rotationRate.z) < 0 && gyrouptmp < 0) { //moving always to the right

					UpDown = 2;

				} else if ((float)System.Math.Round (Input.gyro.rotationRate.z) < 0 && gyrouptmp > 0 && (float)System.Math.Round (Input.gyro.rotationRate.z) != 0) { //moving to the right then changing to the left
					while((float)System.Math.Round (Input.gyro.rotationRate.z) != 0){
						UpDown = 1;}

				} else if ((float)System.Math.Round (Input.gyro.rotationRate.z) > 0 && gyrouptmp < 0 && (float)System.Math.Round (Input.gyro.rotationRate.z) != 0) { //moving always to the left then changing to the right
					while((float)System.Math.Round (Input.gyro.rotationRate.z) != 0){
						UpDown = 1;}
				}
			}

			gyrouptmp = (float)System.Math.Round (Input.gyro.rotationRate.z);

		} else if (gyrouptmp == (float)System.Math.Round (Input.gyro.rotationRate.z)) {
			UpDown = 0;
		}


		//switch with states for gravity changes
		switch(UpDown)
		{

		case 2:
			gravgyroup -= _toPlafond * Time.deltaTime;
			break;

		case 1:
			gravgyroup += _toPlafond * Time.deltaTime;

			break;

		case 0:
			gravgyroup += 0;
			break;

		default:
			Debug.Log("NOTHING");
			break;
		}

	}

}
