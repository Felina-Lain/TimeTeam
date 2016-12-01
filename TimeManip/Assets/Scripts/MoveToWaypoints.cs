using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class MoveToWaypoints : MonoBehaviour {


	public int markcount;
	[HideInInspector]
	public static float speed;
	public float _speedA;
	public List<Transform> targets = new List<Transform>();
	[SerializeField]
	private bool m_bDebug = false; 

	private float nextMove;

	// Use this for initialization
	void Start()
	{
		markcount = -1;
	}

	// Update is called once per frame
	void Update()
	{

		float step = Mathf.Clamp((/*speed + */_speedA),0,Mathf.Infinity) * Time.deltaTime;
		this.transform.position = Vector3.MoveTowards(this.transform.position, targets[markcount + 1].transform.position, step);

	}


	void OnTriggerEnter(Collider other)
	{ 
		for (int i = 0; i < targets.Count; i++) {
			if (other.transform == targets[i].transform) {
				if (m_bDebug)
					Debug.Log ("OnTriggerEnter with " + other.name); 
				Waypoint wp = other.GetComponent<Waypoint> ();
				if (wp == null)
					return;
				if (m_bDebug)
					Debug.Log ("Collider " + other.name + "has a wp");
				if (wp.waitHere)
					StartCoroutine (GoToNextWaypoint (wp.waitingTime));
				else
					CalculateNextWaypoint ();
			}
		}
	}


	IEnumerator GoToNextWaypoint(float _waitingTime)
	{
		yield return new WaitForSeconds(_waitingTime);
		CalculateNextWaypoint();
	}

	private void CalculateNextWaypoint()
	{
		if (m_bDebug) {
			Debug.Log ("Calculate Waypoint entered");
		}
		markcount += 1;
		print ("I reached here");
		if (markcount == targets.Count - 1)
		{
			print ("I died here");
			markcount = -1;
		}

	}
}