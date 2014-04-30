using UnityEngine;
using System.Collections;

public class GUIScript : MonoBehaviour {
	
	GameManager manager;
	int HighestSpeed;


	void Start(){
		manager = GetComponent<GameManager>();
	}
	void OnGUI(){
		GUI.Box(new Rect(10,10,200, 200), "");
		GUI.Label (new Rect(20, 20, 180, 20), "Current Speed:");
		GUI.Label (new Rect(20, 50, 180, 20), manager.GetCurrentAstSpeed().ToString());
		GUI.Label (new Rect(20, 80, 180, 20), "Highest Speed:");
		GUI.Label (new Rect(20, 110, 180, 20), manager.GetHighestAstSpeed().ToString());
		GUI.Label (new Rect(20, 140, 180, 20), "Current Distance:");
		GUI.Label (new Rect(20, 170, 180, 20), manager.GetCurrentDist().ToString());

	}
}
