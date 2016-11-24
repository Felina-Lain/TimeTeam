using UnityEngine;
using System.Collections;

public class PickupItem : MonoBehaviour {
	
	public bool _is_carry;


	void OnTriggerStay (Collider _col)
	{

		print (transform.childCount);

		if (Input.GetKeyDown (KeyCode.H) && transform.childCount == 0) {
			_is_carry = true;
		}else if (Input.GetKeyDown (KeyCode.H) && transform.childCount > 0){
			_is_carry = false;
		}


		if(_is_carry){
			if (_col == null)
				return;
			//_col.GetComponent<MoveAtoB> ().enabled = false;
			_col.GetComponent<CubeReturnToPoint> ().enabled = false;
			_col.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
			_col.transform.parent = this.transform;
			}
		if(this.transform.childCount != 0){
		if(!_is_carry){
				this.transform.GetChild (0).GetComponent<CubeReturnToPoint> ().enabled = true;
				//this.transform.GetChild (1).GetComponent<MoveAtoB> ().enabled = true;
				this.transform.GetChild (0).transform.parent = null;
			}

		}
	}

}
