using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChangeSpeed : MonoBehaviour{

	public float transitionIn = 1.0f;	//la vitesse de transition de la vitesse du temps à l'ACTIVATION
	public float timerSpeed;
	public float timercoef;
	public float speedCoef;
	public Slider slide;


	void Update () {


		MoveToWaypoints.speed = (Mathf.Lerp(MoveToWaypoints.speed, slide.value, transitionIn)) * speedCoef;

		if (!Input.GetMouseButton(0))
			StartCoroutine(OnSliderRelease());		
	}

	IEnumerator OnSliderRelease () {

		while (slide.value > 1) {
			slide.value -= (timerSpeed / timercoef) * Time.deltaTime;
			yield return null;
		}
		while(slide.value < 1){
			slide.value += (timerSpeed / timercoef) * Time.deltaTime;
			yield return null;
		}

	}

}
