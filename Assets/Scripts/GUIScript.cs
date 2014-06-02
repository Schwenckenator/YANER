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
		GUI.Box(new Rect(10,10,200, 430), "");
		GUI.Label (new Rect(20, 20, 180, 20), "Current Speed:");
		GUI.Label (new Rect(20, 50, 180, 20), manager.GetCurrentAstSpeed().ToString());
		GUI.Label (new Rect(20, 80, 180, 20), "Highest Speed:");
		GUI.Label (new Rect(20, 110, 180, 20), manager.GetHighestAstSpeed().ToString());
		GUI.Label (new Rect(20, 140, 180, 20), "Current Distance:");
		GUI.Label (new Rect(20, 170, 180, 20), manager.GetCurrentDist().ToString());
		GUI.Label (new Rect(20, 200, 180, 20), "Time:");
		GUI.Label (new Rect(20, 230, 180, 20), manager.GetCurrentTime().ToString("0.00"));
		GUI.Label (new Rect(20, 260, 180, 20), "Shields:");
		GUI.Label (new Rect(20, 290, 180, 20), condition.GetCurrentShield().ToString());
		GUI.Label (new Rect(20, 320, 180, 20), "Collisions:");
		GUI.Label (new Rect(20, 350, 180, 20), condition.GetShipHits().ToString());
		GUI.Label (new Rect(20, 380, 180, 20), "Current Level:");
		GUI.Label (new Rect(20, 410, 180, 20), manager.GetCurrentLevel().ToString());

		//changed to gamefailed so there can also be GUI for complete level
		if(manager.gameFailed){
			gameOverRect = GUI.Window(0, gameOverRect, GameOverScreen, "GAME OVER");
		} else if(manager.gameFinished){//level complete
			gameOverRect = GUI.Window(0, gameOverRect, LevelFinishedScreen, "LEVEL COMPLETE!");
		}

	}

	void GameOverScreen(int windowId){
		if(GUI.Button(new Rect(20, 20, 100, 20), "Back to Menu")){
			Application.LoadLevel("Menu");
		}
		// Restart current level
		if(GUI.Button(new Rect(140, 20, 100, 20), "Retry Level")){
			condition.ResetShipHits();
			manager.Restart();
		}
	}
	
	void LevelFinishedScreen(int windowId){
		if(GUI.Button(new Rect(20, 20, 100, 20), "Back to Menu")){
			Application.LoadLevel("Menu");
		}
		//play next level (just restarts scene but increases current level value
		if(manager.GetCurrentLevel() < manager.GetMaxLevel()){
			if(GUI.Button(new Rect(140, 20, 100, 20), "Next Level")){
				condition.ResetShipHits();
				manager.NextLevel();
			}
		}
		if (manager.GetCurrentTime() <= manager.GetLevelTargetTime ()) {
			if (GUI.Button (new Rect (260, 20, 100, 20), "Time Bonus!")) {
			}
		}
		if (condition.GetShipHits() < 1) {
			if (GUI.Button (new Rect (375, 20, 105, 20), "Evasion Bonus!")) {
			}
		}
	}
}
