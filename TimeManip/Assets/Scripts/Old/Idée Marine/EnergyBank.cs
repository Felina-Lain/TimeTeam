using UnityEngine;
using System.Collections;

public class EnergyBank : MonoBehaviour {

	public bool can_be_fiddled = true;
	public bool _negative;

	public float energy_score;

	public float _modificator;

	public float current_energy;


	// Use this for initialization
	void Start () {

		current_energy = energy_score;

	}

	// Update is called once per frame
	void Update () {

		GetComponent<AiRandom> ().velocidadMax = current_energy/100;

		if (can_be_fiddled) {

			if (_negative && current_energy > 0) {

				current_energy -= _modificator;

			} else {

				current_energy += _modificator;

			}
		}

	}
}
