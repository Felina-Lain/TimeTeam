using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChangeTime : MonoBehaviour{
	
	public float transitionIn = 1.0f;	//la vitesse de transition de la vitesse du temps à l'ACTIVATION
	public float timerSpeed;

	public Slider slide;
	//public float reverseByTime;


	void Update () {
		//if (Time.timeScale != 1f) {
		//	reverseByTime = (1 / Time.timeScale) * 10;
		//}

		Time.timeScale = Mathf.Lerp(Time.timeScale, slide.value, transitionIn);
		if (!Input.GetMouseButton(0))
			StartCoroutine(OnSliderRelease());		
	}

	IEnumerator OnSliderRelease () {

		while (slide.value > 1) {
			slide.value -= timerSpeed/100000;
			yield return null;
		}
		while(slide.value < 1){
			slide.value += timerSpeed/100000;
			yield return null;
		}
	
	}
		
}