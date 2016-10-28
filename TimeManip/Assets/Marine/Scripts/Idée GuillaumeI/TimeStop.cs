using UnityEngine;
using System.Collections;

public class TimeStop : MonoBehaviour {

	public bool _isStopped;

	public float bulletTimeValue = 0.5f;	//la vitesse de l'écoulement du temps (1 = normalement)
	public float transitionIn = 1.0f;	//la vitesse de transition de la vitesse du temps à l'ACTIVATION
	public float transitionOut = 1.0f;	//la vitesse de transition de la vitesse du temps à DESACTIVATION

	private bool timeSlowed = false;


	void Update () {
		print(Time.timeScale);

		if(!timeSlowed){

			_isStopped = false;

			//			Time.timeScale = Mathf.Lerp(bulletTimeValue, 1.0f, transitionOut * Time.deltaTime);
			Time.timeScale = Mathf.Lerp(Time.timeScale, 1.0f, transitionOut);

		}else{

			_isStopped = true;

			//			Time.timeScale = Mathf.Lerp(0.0f, bulletTimeValue, transitionIn * Time.deltaTime);
			Time.timeScale = Mathf.Lerp(Time.timeScale, bulletTimeValue, transitionIn);

		}

		if(Input.GetKeyDown(KeyCode.G)){
			timeSlowed = !timeSlowed;
		}

	}

}