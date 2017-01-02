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
			
			float _a = GetComponent<MeshRenderer> ().material.color.a;
			_a  -= (lifeTime * Time.deltaTime);
			GetComponent<MeshRenderer> ().material.color = new Color (GetComponent<MeshRenderer> ().material.color.r, GetComponent<MeshRenderer> ().material.color.g, GetComponent<MeshRenderer> ().material.color.b, _a);
			if (_a <= 0) {
				Destroy (this.gameObject);
			}
		}
			
	}


}
