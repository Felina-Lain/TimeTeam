using UnityEngine;
using System.Collections;

public enum CubeGroups {Red, Boing, Black, Green, Roll, Spawner}

public class CubeManager : MonoBehaviour {

	public bool startCube;
	public CubeGroups myGroup;


	static public float _chrono;

	void Start(){
	
		_chrono = 25;

	}


	void Update(){

		_chrono -= Time.deltaTime;
		if (_chrono < 0) {
			transform.tag = "Cube";
		}

	}


}
