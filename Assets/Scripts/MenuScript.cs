using UnityEngine;
using System.Collections;

public class MenuScript : MonoBehaviour {

	Rect menuRect = new Rect(Screen.width/4, Screen.height/4, Screen.width/2,Screen.height/2);

	void OnGUI(){
		menuRect = GUI.Window(0, menuRect, BasicMenu, "MENU");

	}
	void BasicMenu(int windowID){
		if(GUI.Button(new Rect(20, 20, 100, 20), "Play Game")){
			Application.LoadLevel("Test Scene");
		}
	}
}
