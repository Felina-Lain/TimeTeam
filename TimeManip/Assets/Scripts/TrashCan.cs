using UnityEngine;
using System.Collections;

public class TrashCan : MonoBehaviour {

	public float minX;
	public float maxX;
	public float minZ;
	public float maxZ;


	// Use this for initialization
	void OnTriggerExit (Collider other) {

		if (other.GetComponent<CubeManager> ().startCube) {

			GameObject _spawned = Instantiate (other.gameObject);
			_spawned.transform.position = new Vector3 (Random.Range (minX,maxX), 243, Random.Range (minX,maxX));
		//	if (GameObject.Find (other.GetComponent<CubeManager> ().myGroup.ToString()) == null) {
		//		GameObject _go = new GameObject();
		//		_go.name = other.GetComponent<CubeManager> ().myGroup.ToString();
		//	}
		//	_spawned.transform.parent = GameObject.Find (other.GetComponent<CubeManager> ().myGroup.ToString()).transform;
			Destroy (other.gameObject);

		} else {
			Destroy (other.gameObject);
		}
	
	}

}
