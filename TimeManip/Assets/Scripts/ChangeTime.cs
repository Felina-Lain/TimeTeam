using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChangeTime : MonoBehaviour {
	

	public float bulletTimeValue = 0.5f;	//la vitesse de l'écoulement du temps (1 = normalement)
	public float transitionIn = 1.0f;	//la vitesse de transition de la vitesse du temps à l'ACTIVATION
	public float transitionOut = 1.0f;	//la vitesse de transition de la vitesse du temps à DESACTIVATION

	public Slider slide;


	void Update () {

		bulletTimeValue = slide.value;

		ChangingTime();
	}

	public void ChangingTime(){


		Time.timeScale = Mathf.Lerp(Time.timeScale, bulletTimeValue, transitionIn);

	}

}