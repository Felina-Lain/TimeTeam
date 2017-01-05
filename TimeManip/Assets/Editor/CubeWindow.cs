using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

public class CubeWindow : EditorWindow //Deriving from editor window to get window and GUI elements.
{
	List<bool> showGeneralSettings = new List<bool>();//create a list of foldouts, one for each objects
	private CubeManager[] Cubemag; //array of target objects
	[MenuItem("Window/Cube Window")] //Declaring to Unity engine that this is meant as a menu item.
	public static void ShowWindow() //Telling unity what MenuItem - "WIndowCube" does.
	{
		EditorWindow cubeWindow = EditorWindow.GetWindow(typeof(CubeWindow)); //Getting our window for unity to use.
		GUIContent titlecontent = new GUIContent("Cube Window"); //Creating some title content.
		cubeWindow.titleContent = titlecontent; //Setting the title content to the window.
		cubeWindow.maxSize = new Vector2(300, 500); //Declaring the max size of the window.
		cubeWindow.minSize = new Vector2(300, 500); //Declaring the min size of the window.
		cubeWindow.ShowPopup(); //Showing our window as a Popup, I.E., a default editor style.
	}


	void OnGUI() //OnGUI gets called even more than Update avoid making large data collection calls here.
	{
		EditorGUILayout.BeginVertical(EditorStyles.helpBox); //Declaring our first part of our layout, and adding a bit of flare with EditorStyles.

		GUILayout.Label("List of Cubes", EditorStyles.boldLabel); //Making a label in our vertical view, declaring its contents, and adding editor flare.

		Cubemag = FindObjectsOfType<CubeManager>(); //Collecting our cube info, if too many cube, do this outside of GUI

		for (int i = 0; i < Cubemag.Length; i++) //Your loop to display the objects, THIS must exist in OnGUI.
		{
			showGeneralSettings.Add (true); 
			if (Cubemag[i] != null) //Preventing null errors if the objects are removed.
			{
				Cubemag[i] = (CubeManager)EditorGUILayout.ObjectField(Cubemag[i], typeof(CubeManager), true); //Declaring our object as an object field, with the type of object we want, and allowing scene object.

				showGeneralSettings[i] = EditorGUILayout.Foldout(showGeneralSettings[i], "Properties");//creating a foldout for clarity
				
				if (showGeneralSettings[i]){//if the foldout is open
					
				EditorGUILayout.BeginHorizontal(); //Adding a horizontal view to indent our next line so that the properties look like children, LOTS of things you can add like toggles and stuff to reduce this clutter.
				GUILayout.Space(15); //The indent for our next line, in pixels.

				EditorGUIUtility.labelWidth = 60;//changing the length of the label space
				EditorGUIUtility.fieldWidth = 10;//changing the lenght of the field space

				Cubemag[i].lifeTime = EditorGUILayout.FloatField("Life Time", Cubemag[i].lifeTime); //the lifeTime property of cubemap can be assigned to a GUI element here.
				EditorGUIUtility.fieldWidth = 0.01f;

				Cubemag[i].myGroup = (CubeGroups)EditorGUILayout.EnumPopup(Cubemag[i].myGroup);
				EditorGUIUtility.labelWidth = 80;

				Cubemag[i].startCube = EditorGUILayout.Toggle("Start Cube", Cubemag[i].startCube);

				EditorGUILayout.EndHorizontal(); //All layout areas must be closed like this.
				}
			}
		}
		EditorGUILayout.EndVertical(); // And closing our last area.

	}
}
