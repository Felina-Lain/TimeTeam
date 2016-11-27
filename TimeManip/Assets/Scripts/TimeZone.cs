using UnityEngine;
using System.Collections;

public class TimeZone : MonoBehaviour {

	public GameObject _prefabZone;
	public float _groundZero;

	//Time to double tap
	[SerializeField] private float _cooldown = 0.5f;
	//Speed of advance
	[SerializeField] private float _speed;
	//Distance to advance
	[SerializeField] private float _distance;

	//Count taps
	private int _count;
	private Vector3 _velocity = Vector3.zero;
	




	void LateUpdate()
	{
		//If one tap
		if (Input.GetMouseButtonDown(0))
		{
			//If there's already been a tap within the timer, launch the move
			if (_cooldown > 0 && _count == 1 /*Number of Taps you want Minus One*/)
			{
				// get mouse pos on this X-Y plane
				Vector3 mPos = Input.mousePosition;
				mPos.z = Camera.main.transform.InverseTransformPoint(transform.position).z;
				Vector3 mPosWorld = Camera.main.ScreenToWorldPoint(mPos);
				//spawn the zone
				GameObject _temp = (GameObject)Instantiate (_prefabZone, new Vector3(mPosWorld.x,  _groundZero , mPosWorld.z), Quaternion.identity);

				//Has double tapped
			}
			//Else launch timer
			else
			{
				_cooldown = 0.5f;
				_count += 1;
			}
		}
		//If timer launched, count time
		if (_cooldown > 0)
		{
			_cooldown -= Time.deltaTime;
		}
		else
		{
			_count = 0;
		}
	}
}
