using UnityEngine;
using System.Collections;

public class PlayerInfluence : MonoBehaviour {

	public float p_influence;


	// Use this for initialization
	void OnTriggerStay (Collider _col) {

		if (Input.GetKey (KeyCode.F)) {
		
			if (_col.GetComponent<EnergyBank> ().can_be_fiddled) {

				if (_col.GetComponent<EnergyBank> ()._negative) {

					_col.GetComponent<EnergyBank> ().current_energy += p_influence;
					this.GetComponentInParent<PlayerEnergyBank> ().current_energy -= p_influence;

				} else if(_col.GetComponent<EnergyBank> ().current_energy > 0) {
					
					_col.GetComponent<EnergyBank> ().current_energy -= p_influence;
					this.GetComponentInParent<PlayerEnergyBank> ().current_energy += p_influence;

				}

			}
		
		}
			
	}

}
