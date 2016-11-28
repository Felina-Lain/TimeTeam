using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {


	public void Reset () {
		
		Application.LoadLevel(Application.loadedLevel);

	}
}
