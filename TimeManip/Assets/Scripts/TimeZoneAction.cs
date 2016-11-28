using UnityEngine;
using System.Collections;

public class TimeZoneAction : MonoBehaviour {

	private float lifeTime;
	public float _lifeTime;
	public float _influence;

	void Start () {

		lifeTime = _lifeTime;

	}


	void OnTriggerExit (Collider _col) {

		if (_col.name.Contains ("Cube") && !_col.name.Contains("fixe")) {

			_col.GetComponent<MoveToWaypoints>()._speedA = 0f;
		}
	
	}
	
	// Update is called once per frame
	void OnTriggerStay (Collider _col) {


		if (_col.name.Contains ("Cube") && !_col.name.Contains("fixe")) {

			_col.GetComponent<MoveToWaypoints>()._speedA = _influence * 100;
		}

	}

	void Update (){

		lifeTime -= Time.deltaTime;
		float _size = (200/_lifeTime) * lifeTime;

		transform.localScale = new Vector3 (_size, _size, _size);

		if (lifeTime <= 0) {
		
			Destroy (this.gameObject);

		}
	}
}
