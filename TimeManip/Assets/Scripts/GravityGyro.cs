using UnityEngine;
using System.Collections;

public class GravityGyro : MonoBehaviour {

	Vector3 grav_ini;

	float tempy;
	float tempx;
	float tempz;

	float gravgyroup;
	public float _toPlafond;

	public float _speed;

	void Awake () {
		Input.gyro.enabled = true;
		Physics.gravity = new Vector3 (0,-150,0);
		grav_ini = Physics.gravity;

		tempy = 0 - (float)System.Math.Round(Input.acceleration.y,1);
		tempx = 0 - (float)System.Math.Round(Input.acceleration.x,1);
		gravgyroup = 0;



	}

	public void ResetGravity(){
		Input.gyro.enabled = true;
		Physics.gravity = new Vector3 (0,-150,0);
		grav_ini = Physics.gravity;
		tempy = 0 - (float)System.Math.Round(Input.acceleration.y,1);
		tempx = 0 - (float)System.Math.Round(Input.acceleration.x,1);
		gravgyroup = 0;


	}

	// Update is called once per frame
	void Update () {

		Debug.Log (Physics.gravity.y);


		//gravity tilt
		Physics.gravity = grav_ini + (new Vector3((float)System.Math.Round(tempx + Input.acceleration.x,1),(gravgyroup - grav_ini.y)*_toPlafond/_speed,(float)System.Math.Round(tempy + Input.acceleration.y,1))*_speed); 

		gravgyroup = ((600/360) * (float)System.Math.Round(Input.gyro.attitude.eulerAngles.z)) -300;

	}

}
