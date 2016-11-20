using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChangeTime : MonoBehaviour{
	

	//public float bulletTimeValue = 0.5f;	//la vitesse de l'écoulement du temps (1 = normalement)
	public float transitionIn = 1.0f;	//la vitesse de transition de la vitesse du temps à l'ACTIVATION
	public float transitionOut = 1.0f;	//la vitesse de transition de la vitesse du temps à DESACTIVATION
	public float timerSpeed;

	public Slider slide;
	public float reverseByTime;


	void Update () {
		if (Time.timeScale != 1f) {
			reverseByTime = (1 / Time.timeScale) * 10;
		}

		Time.timeScale = Mathf.Lerp(Time.timeScale, slide.value, transitionIn);
		if (!Input.GetMouseButton(0))
			StartCoroutine(OnSliderRelease());
	}

	IEnumerator OnSliderRelease () {

		while (slide.value > 1) {
			slide.value -= timerSpeed * (Time.deltaTime* reverseByTime);
			yield return null;
		}
		while(slide.value < 1){
			slide.value += timerSpeed * (Time.deltaTime* reverseByTime);
			yield return null;
		}
	
	}
		
}