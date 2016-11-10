using UnityEngine;

public class CharacMove : MonoBehaviour
{

	public float turnspeed;
	public float movespeed;

	public float reverseByTime;

	private float _x;
	private float _z;

	void Update()
	{

		if (Time.timeScale < 1f) {
			reverseByTime = (1 / Time.timeScale)*10;
			_x = Input.GetAxis ("Horizontal") * (Time.deltaTime * reverseByTime) * turnspeed ;
			_z = Input.GetAxis ("Vertical") * (Time.deltaTime * reverseByTime) * movespeed;
		} else {
		 _x = Input.GetAxis ("Horizontal") * Time.deltaTime * turnspeed;
		 _z = Input.GetAxis ("Vertical") * Time.deltaTime * movespeed;
		}

		transform.Rotate(0, _x, 0);
		transform.Translate(0, 0, _z);
	}
}
