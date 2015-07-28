using UnityEngine;
using System.Collections;

public class MenuScript : MonoBehaviour {
	
	Rect menuRect = new Rect(Screen.width/4, Screen.height/4, Screen.width/2,Screen.height/2);

	void OnGUI(){
		menuRect = GUI.Window(0, menuRect, BasicMenu, "MENU");

	}
	void BasicMenu(int windowID){
		//buttons for multiple levels, should be unlocked by playing, each level can use same scene
		//GameManager can use arrays for things like level complete distances and level achievements
		if(GUI.Button(new Rect(20, 20, 100, 20), "Continue Game")){
			Application.LoadLevel("Test Scene");
		}
		if(GUI.Button(new Rect(20, 50, 95, 20), "Play Level 1")){
			PlayerPrefs.SetInt("Level",1);
			Application.LoadLevel("Test Scene");
		}
		if(GUI.Button(new Rect(130, 50, 95, 20), "Play Level 2")){
			PlayerPrefs.SetInt("Level",2);
			Application.LoadLevel("Test Scene");
		}
		if(GUI.Button(new Rect(240, 50, 95, 20), "Play Level 3")){
			PlayerPrefs.SetInt("Level",3);
			Application.LoadLevel("Test Scene");
		}
		if(GUI.Button(new Rect(350, 50, 95, 20), "Play Level 4")){
			PlayerPrefs.SetInt("Level",4);
			Application.LoadLevel("Test Scene");
		}
		if(GUI.Button(new Rect(460, 50, 95, 20), "Play Level 5")){
			PlayerPrefs.SetInt("Level",5);
			Application.LoadLevel("Test Scene");
		}
	}
}
