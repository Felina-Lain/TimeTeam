using UnityEngine;
using System.Collections;

public class VizuTool : MonoBehaviour {

	public Transform _maincam;

	// Use this for initialization
	void OnDrawGizmos () {

		Gizmos.color = Color.white;
		Gizmos.DrawRay (Vector3.zero,  Physics.gravity);

		//Gizmos.color = Color.blue;
		//Gizmos.DrawRay (new Vector3(0.3f,0.3f,0.3f),  Input.gyro.attitude.eulerAngles*500);
		//Gizmos.color = Color.green;
		//Gizmos.DrawRay (new Vector3(0.3f,0.3f,0.3f),  _maincam.transform.up*500);
		//Gizmos.color = Color.red;
		//Gizmos.DrawRay (new Vector3(0.3f,0.3f,0.3f),  _maincam.transform.right*500);
		//
		//Gizmos.color = Color.magenta;
		//Gizmos.DrawLine(Vector3.right * 2000, Vector3.zero);
		//Gizmos.color = Color.yellow;
		//Gizmos.DrawLine(Vector3.up * 2000, Vector3.zero);
		//Gizmos.color = Color.cyan;
		//Gizmos.DrawLine(Vector3.forward * 2000, Vector3.zero);

	}

}
