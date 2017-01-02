using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GravityChange : MonoBehaviour {

	private Vector3 gravity_ini;
	public float speed;

	public Text _text;

	//calbiration
	Matrix4x4 calibrationMatrix;

	// gravity constant
	public float g = 9.8f;

	// Use this for initialization
	void Start () {
		Input.gyro.enabled = true;
		calibrateAccelerometer ();
		gravity_ini = Physics.gravity;

	}

	void Update() {

		Vector3 dir = getAccelerometer(Input.acceleration);

		if (dir.sqrMagnitude > 1)
			dir.Normalize();
		// normalize axis
		Vector3 _temp = new Vector3(Mathf.Floor(dir.x/5),Mathf.Floor(dir.z/5),Mathf.Floor(dir.y/5));

		Physics.gravity = _temp * Time.deltaTime * speed;


		_text.text = ("x " + Physics.gravity.x.ToString() + "y " + Physics.gravity.y.ToString()  + "z " + Physics.gravity.z.ToString());
	}
		

	void calibrateAccelerometer(){
		Vector3 wantedDeadZone = Input.acceleration;;
		Quaternion rotateQuaternion = Quaternion.FromToRotation(new Vector3(0f, 0f, -1f), wantedDeadZone);
		//create identity matrix ... rotate our matrix to match up with down vec
		Matrix4x4 matrix = Matrix4x4.TRS(Vector3.zero, rotateQuaternion, new Vector3(1f, 1f, 1f));
		//get the inverse of the matrix
		this.calibrationMatrix = matrix.inverse;
	}

	Vector3 getAccelerometer(Vector3 accelerator){
		Vector3 accel = this.calibrationMatrix.MultiplyVector(accelerator);
		return accel;
	}

}
