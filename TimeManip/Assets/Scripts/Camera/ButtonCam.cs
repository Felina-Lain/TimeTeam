using UnityEngine;
using System.Collections;

public class ButtonCam : MonoBehaviour {

	public float speed;
	public bool _right;
	public bool _left;
	public bool _down;
	public bool _up;

	
	// Update is called once per frame
	void Update ()
	{
		if (_left) {
			//transform.Rotate (-Vector3.right * Time.deltaTime * speed);

			float rotationY = transform.localEulerAngles.x - speed * Time.deltaTime;
			if(transform.localEulerAngles.x < 180f)
			{
				rotationY = Mathf.Clamp(rotationY, -75f, 75.0f);
			}
			if(transform.localEulerAngles.x > 180f)
			{
				rotationY = Mathf.Clamp(rotationY , 295f, 360f);
			}

			transform.localEulerAngles = new Vector3(rotationY,transform.localEulerAngles.y , transform.localEulerAngles.z);

		} else if (_right) {
			//transform.Rotate (Vector3.right * Time.deltaTime * speed);

			float rotationY = transform.localEulerAngles.x + speed * Time.deltaTime;
			if(transform.localEulerAngles.x < 180f)
			{
				rotationY = Mathf.Clamp(rotationY, -75f, 75.0f);
			}
			if(transform.localEulerAngles.x > 180f)
			{
				rotationY = Mathf.Clamp(rotationY , 295f, 360f);
			}

			transform.localEulerAngles = new Vector3(rotationY,transform.localEulerAngles.y , transform.localEulerAngles.z);

		} else if (_up) {
			//transform.Rotate (Vector3.up * Time.deltaTime * speed);
			float rotationY = transform.localEulerAngles.y + speed * Time.deltaTime;
			transform.localEulerAngles = new Vector3(transform.localEulerAngles.x ,rotationY, 0);
		} else if (_down) {
			float rotationY = transform.localEulerAngles.y - speed * Time.deltaTime;
			transform.localEulerAngles = new Vector3(transform.localEulerAngles.x ,rotationY, 0);
		}
	}

	public void Right(){

		_right = !_right;
	}
	public void Left(){

		_left = !_left;
	}
	public void Up(){

		_up = !_up;
	}
	public void Down(){

		_down = !_down;
	}
}
