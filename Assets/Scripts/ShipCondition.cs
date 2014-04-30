using UnityEngine;
using System.Collections;

public class ShipCondition : MonoBehaviour {
	public int maxShipShield;
	public int maxShipHull;

	private int curShipShield;
	private int curShipHull;

	private GameManager manager;
	// Use this for initialization
	void Start () {
		manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
		curShipShield = maxShipShield;
		curShipHull = maxShipHull;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void TakeDamage(int dam){
		if(curShipShield > 0){
			curShipShield -= dam;
		}else{
			curShipHull -= dam;
		}
		CheckShipValues();
	}
	public void RemoveShield(int lostShield){
		curShipShield -= lostShield;
	}
	private void CheckShipValues(){
		if(curShipShield < 0){
			curShipHull += curShipShield;
		}
		if(curShipHull <= 0){
			//You lose
			manager.gameFinished = true;
		}
	}
}
