using UnityEngine;
using System.Collections;

public class GUIScript : MonoBehaviour {
	
	GameManager manager;
	ShipCondition condition;
	int HighestSpeed;

	Rect gameOverRect = new Rect(Screen.width/4, Screen.height/4, Screen.width/2,Screen.height/2);

	void Start(){
		manager = GetComponent<GameManager>();
		condition = GameObject.FindGameObjectWithTag("PlayerShip").GetComponent<ShipCondition>();
	}
	void OnGUI(){
		GUI.Box(new Rect(10,10,200, 250), "");
		GUI.Label (new Rect(20, 20, 180, 20), "Current Speed:");
		GUI.Label (new Rect(20, 50, 180, 20), manager.GetCurrentAstSpeed().ToString());
		GUI.Label (new Rect(20, 80, 180, 20), "Highest Speed:");
		GUI.Label (new Rect(20, 110, 180, 20), manager.GetHighestAstSpeed().ToString());
		GUI.Label (new Rect(20, 140, 180, 20), "Current Distance:");
		GUI.Label (new Rect(20, 170, 180, 20), manager.GetCurrentDist().ToString());
		GUI.Label (new Rect(20, 200, 180, 20), "Shields:");
		GUI.Label (new Rect(20, 230, 180, 20), condition.GetCurrentShield().ToString());

		if(manager.gameFinished){
			gameOverRect = GUI.Window(0, gameOverRect, GameOverScreen, "GAME OVER");
		}

	}

	void GameOverScreen(int windowId){
		if(GUI.Button(new Rect(20, 20, 100, 20), "Back to Menu")){
			Application.LoadLevel("Menu");
		}
	}

}
