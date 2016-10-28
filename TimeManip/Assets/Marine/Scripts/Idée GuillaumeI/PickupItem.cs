using UnityEngine;
using System.Collections;

public class PickupItem : MonoBehaviour {

	public bool _is_carry;
	
	// Update is called once per frame
	void Update(){
		
		if (Input.GetKeyDown (KeyCode.H)) {
			_is_carry = !_is_carry;
		}

	}


	void OnTriggerStay (Collider _col)
	{
		if(_is_carry){
			if (_col == null)
				return;
			_col.GetComponent<CubeReturnToPoint> ().enabled = false;
			_col.transform.parent = this.transform;
			}
		if(!_is_carry){
			if (this.transform.GetChild (1) != null) {
				this.transform.GetChild (1).GetComponent<CubeReturnToPoint> ().enabled = true;
				this.transform.GetChild (1).transform.parent = null;
				}
			}

		}

}
