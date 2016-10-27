using UnityEngine;

public class CharacMove : MonoBehaviour
{

	public float turnspeed;
	public float movespeed;

	void Update()
	{
		var x = Input.GetAxis ("Horizontal") * Time.deltaTime * turnspeed;
		var z = Input.GetAxis("Vertical") * Time.deltaTime * movespeed;

		transform.Rotate(0, x, 0);
		transform.Translate(0, 0, z);
	}
}
