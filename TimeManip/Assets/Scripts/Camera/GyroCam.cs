using UnityEngine;
using System.Collections;


public class GyroCam : MonoBehaviour {

	// Use this for initialization
	void Start () {
		SensorFusion.Recenter();
	}

	// Update is called once per frame
	void Update () {
		transform.rotation = SensorFusion.GetOrientation();
	}

	/*
	//Time to double tap
	[SerializeField] private float _cooldown = 0.5f;
	//Speed of advance
	[SerializeField] private float _speed;
	//Distance to advance
	[SerializeField] private float _distance;

	private Quaternion initialRotation;
	private Quaternion gyroInitialRotation;

	//Count taps
	private int _count;
	private float _velocity;

	public float damp_speed = 1;

	void Start ()
	{
		Input.gyro.enabled = true;
		initialRotation = transform.rotation; 
		//gyroInitialRotation = Input.gyro.attitude;

	}


	void Update () 
	{


		float newPositionx = Mathf.SmoothDamp(transform.position.x, Input.gyro.rotationRateUnbiased.x, ref _velocity, damp_speed);
		float newPositiony = Mathf.SmoothDamp(transform.position.y, Input.gyro.rotationRateUnbiased.y, ref _velocity, damp_speed);
		transform.Rotate (newPositionx, 0, newPositiony);
		//float newPositionx = Input.gyro.rotationRateUnbiased.x;
		//float newPositiony = Input.gyro.rotationRateUnbiased.y;
		//transform.localRotation = Quaternion.Euler(newPositionx,0,newPositiony);
	
	}

	public void ResetGyro () {

		transform.rotation = initialRotation;
	}*/
		
}