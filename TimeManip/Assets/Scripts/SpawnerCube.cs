using UnityEngine;
using System.Collections;

public class SpawnerCube : MonoBehaviour {

	public int maxNbSpawn;

	// Use this for initialization
	void OnCollisionEnter (Collision _collide) {


		if (_collide.transform.tag == "Cube") {


			for (int i = 0; i < maxNbSpawn; i++) {
				
				string _pathname = _collide.gameObject.GetComponent<CubeManager> ().myGroup.ToString ();
				//print (_pathname);
				GameObject newcube = Instantiate (Resources.Load (_pathname, typeof(GameObject))) as GameObject;
				newcube.GetComponent<CubeManager> ().startCube = false;
				newcube.transform.tag = "temptag";
				newcube.transform.position = new Vector3 (transform.position.x, 243, transform.position.z);
			}
		}
	
	}

}
