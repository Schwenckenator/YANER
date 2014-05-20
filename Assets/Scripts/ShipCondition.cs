using UnityEngine;
using System.Collections;

public class ShipCondition : MonoBehaviour {
	public int maxShipShield;
	public int maxShipHull;

	private int curShipShield;
	private int curShipHull;
	
	public int hits;

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
	public int GetCurrentShield(){
		return curShipShield;
	}
	public void SetCurrentShield(int newShield){
		curShipShield = newShield;
	}

	public void RestoreShield(){ // Sets value to maximum
		curShipShield = maxShipShield;
	}
	
	public void RestoreHull(){ // Sets value to maximum
		curShipHull = maxShipHull;
	}

	public void TakeDamage(int dam){ //Takes damage to shield/hull
		if(manager.gameFinished == false){//only works while game active
			if(curShipShield > 0){
				curShipShield -= dam;
			}else{
				curShipHull -= dam;
			}
			hits += 1;
			CheckShipValues();
		}
	}
	
	public int GetShipHits(){
		return hits;
	}
	public void ResetShipHits(){
		hits = 0;
	}

	private void CheckShipValues(){
		if(curShipShield < 0){
			curShipHull += curShipShield;
		}
		if(curShipHull <= 0){
			//You lose
			manager.gameFailed = true;
			manager.gameFinished = true;
		}
	}

}
