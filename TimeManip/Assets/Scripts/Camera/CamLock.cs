using UnityEngine;
using System.Collections;

public class CamLock : MonoBehaviour {

	public Transform _pivot;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		transform.LookAt (_pivot);
	
	}
}
