using UnityEngine;
using System.Collections;

public class PlayerEnergyBank : MonoBehaviour {

	public float energy_score;

	public float current_energy;


	// Use this for initialization
	void Start () {

		current_energy = energy_score;

	}

	void Update (){

		this.GetComponent<CharacMove> ().movespeed = current_energy;

	}
}
