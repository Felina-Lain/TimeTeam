using UnityEngine;
using System.Collections;

public enum CubeGroups {Red, Boing, Black, Green, Roll, Spawner}

public class CubeManager : MonoBehaviour {

	public bool startCube;
	public CubeGroups myGroup;

	[Header("Smaller Number means longer life")]
	public float lifeTime;


	static public float _chrono;

	void Start(){
	
		_chrono = 25;

	}


	void Update(){

		_chrono -= Time.deltaTime;
		if (_chrono < 0) {
			transform.tag = "Cube";
		}
		if (!startCube) {
			
			float _a = GetComponentInChildren<MeshRenderer> ().material.color.a;
			_a  -= (lifeTime * Time.deltaTime);
			GetComponentInChildren<MeshRenderer> ().material.color = new Color (GetComponentInChildren<MeshRenderer> ().material.color.r, GetComponentInChildren<MeshRenderer> ().material.color.g, GetComponentInChildren<MeshRenderer> ().material.color.b, _a);
			if (_a <= 0) {
				Destroy (this.gameObject);
			}
		}
			
	}


}
