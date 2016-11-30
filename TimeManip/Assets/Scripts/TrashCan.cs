﻿using UnityEngine;
using System.Collections;

public class TrashCan : MonoBehaviour {


	// Use this for initialization
	void OnTriggerExit (Collider other) {

		if (other.GetComponent<CubeManager> ().startCube) {

			GameObject _spawned = Instantiate (other.gameObject);
			_spawned.transform.position = new Vector3 (103, 243, 13);
			if (GameObject.Find (other.GetComponent<CubeManager> ().colorGroup) == null) {
				GameObject _go = new GameObject();
				_go.name = other.GetComponent<CubeManager> ().colorGroup;
			}
			_spawned.transform.parent = GameObject.Find (other.GetComponent<CubeManager> ().colorGroup).transform;
			Destroy (other.gameObject);

		} else {
			Destroy (other.gameObject);
		}
	
	}

}