using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEditorInternal;

public class CubeWindow : EditorWindow {

	CubeManager[] cubemag = (CubeManager[])FindObjectsOfType (typeof(CubeManager));

	[MenuItem ("Window/Cube Window")]

	public static void  ShowWindow () {
		EditorWindow.GetWindow(typeof(CubeWindow));
	}
		

	// Use this for initialization
	void OnGUI() {

		GUILayout.Label ("List of Cubes", EditorStyles.boldLabel);

		cubemag = (CubeManager[])FindObjectsOfType (typeof(CubeManager));
		for(int i = 0; i < cubemag.Length; i++)
		{
			cubemag[i] = (CubeManager)EditorGUILayout.ObjectField(cubemag[i], typeof(CubeManager));
		}

	
	}

}
