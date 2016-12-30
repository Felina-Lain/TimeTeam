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
			transform.Rotate (-Vector3.right * Time.deltaTime * speed);
		} else if (_right) {
			transform.Rotate (Vector3.right * Time.deltaTime * speed);
		} else if (_up) {
			transform.Rotate (Vector3.up * Time.deltaTime * speed);
		} else if (_down) {
			transform.Rotate (-Vector3.up * Time.deltaTime * speed);
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
