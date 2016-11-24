using UnityEngine;
using System.Collections;

public class TimeZone : MonoBehaviour {

	public GameObject _prefabZone;
	public float _groundZero;

	
	// Update is called once per frame
	void Update () {

		// get mouse pos on this X-Y plane
		Vector3 mPos = Input.mousePosition;
		mPos.z = Camera.main.transform.InverseTransformPoint(transform.position).z;
		Vector3 mPosWorld = Camera.main.ScreenToWorldPoint(mPos);

		if (Input.GetMouseButtonDown (0)) {

			GameObject _temp = (GameObject)Instantiate (_prefabZone, new Vector3(mPosWorld.x,  _groundZero , mPosWorld.z), Quaternion.identity);
		}
	}
}
