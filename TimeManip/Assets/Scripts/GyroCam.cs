﻿using UnityEngine;
using System.Collections;


public class GyroCam : MonoBehaviour {

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

	public float damp_speed;

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
	
	}

	public void ResetGyro () {

		transform.rotation = initialRotation;
	}
		
}