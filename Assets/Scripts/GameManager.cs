using UnityEngine;
using System.Collections;
using System.Diagnostics;
using System.Threading;

public class GameManager : MonoBehaviour {
	public GameObject Asteroid;

	public GameObject ShieldRestore;

	public int StartingAsteroidSpeed;
	public float AsteroidSpawnPosition; // How far back asteroids spawn
	public float AsteroidHorizontalSpawnPosition; // How far either side the asteroids can spawn
	//soawn timer
	public float minTime;
	public float maxTime;

	public float minScale;
	public float maxScale;

	private int currentAstSpeed;
	private int highestAstSpeed;
	private int currentDist = 0;

	public bool gameFinished = false;
	//bool for level complete/failed
	public bool gameFailed = false;
	
	ShipCondition playerShip;

	public float ShieldRestoreCooldown;
	public int ShieldRestoreChance;
	
	public float LevelTime; //time elapsed in a level
	
	public static int NumberOfLevels = 5;
	private int CurrentLevel = -99;
	//distance for each level
	public int[] EndLevelAtDistance;
	//time goal of each level
	public int[] TargetTime;
	
	Stopwatch stopWatch = new Stopwatch();

	// Use this for initialization
	void Start() {
		stopWatch.Start();
		EndLevelAtDistance = new int[NumberOfLevels + 1];
		for (int i = 1; i <= NumberOfLevels; i++){
			EndLevelAtDistance[i] = i*50000;
		}
		TargetTime = new int[NumberOfLevels + 1];
		for (int i = 1; i <= NumberOfLevels; i++){
			TargetTime[i] = i*20;
		}
		playerShip = GameObject.FindGameObjectWithTag("PlayerShip").GetComponent<ShipCondition>();
		currentAstSpeed = StartingAsteroidSpeed;
		highestAstSpeed = currentAstSpeed;
		//First level only accessible from menu, levels play in order after that
		//levels can be skipped in the menu
		CurrentLevel = PlayerPrefs.GetInt("Level");
		if (CurrentLevel < 0) {
			CurrentLevel = 0;
		}
		StartCoroutine("SpawnAsteroid");
		StartCoroutine("CalculateDistance");
		StartCoroutine("SpawnBonus");

	}
	
	public void Restart(){//restart level
		stopWatch.Reset();
		stopWatch.Start();
		gameFinished = false;
		gameFailed = false;
		currentAstSpeed = StartingAsteroidSpeed;
		highestAstSpeed = currentAstSpeed;
		currentDist = 0;
		StartCoroutine("SpawnAsteroid");
		StartCoroutine("CalculateDistance");
		StartCoroutine("SpawnBonus");
		playerShip.RestoreShield();
		playerShip.RestoreHull();
	}
	
	//let other scripts get level number
	public int GetCurrentLevel(){
		return CurrentLevel;
	}
	public int GetMaxSpeed(){
		return highestAstSpeed;
	}
	public int GetLevelTargetTime(){
		return TargetTime[CurrentLevel];
	}
	public int GetMaxLevel(){
		return NumberOfLevels;
	}
	
	public float GetCurrentTime(){
		return LevelTime;
	}
	
	//update level number and restart scene
	public void NextLevel(){
		CurrentLevel += 1;
		PlayerPrefs.SetInt("Level",CurrentLevel);
		Restart();
	}
	
	
	void FixedUpdate(){
		if(!gameFinished){
			System.TimeSpan ts = stopWatch.Elapsed;
			float minutes = ts.Minutes;
			float milliSec = ts.Milliseconds;
			float seconds = ts.Seconds;
			LevelTime = minutes*60 + seconds + milliSec/1000;//updates time count
			currentAstSpeed++;
			if(currentAstSpeed > highestAstSpeed){
				highestAstSpeed = currentAstSpeed;
			}
		}
	}
	private bool DiceRoll(int SuccessPercent){
		int roll = Random.Range(0,99);
		return (roll < SuccessPercent);
	}
	IEnumerator CalculateDistance(){
		//stops cycling on level finish instead of game over
		while(currentDist < EndLevelAtDistance[CurrentLevel] && !gameFinished){
			currentDist += currentAstSpeed/10;
			//checks if player has travelled required distance
			if(currentDist >= EndLevelAtDistance[CurrentLevel]){
				stopWatch.Stop();
				gameFinished = true;
			}
			yield return new WaitForSeconds(0.1f);
		}
	}
	IEnumerator SpawnAsteroid(){
		while(!gameFinished){
			GameObject newAsteroid = Instantiate(Asteroid, 
			                                     new Vector3(Random.Range (-AsteroidHorizontalSpawnPosition,AsteroidHorizontalSpawnPosition), 0, AsteroidSpawnPosition), 
			                                     new Quaternion()) as GameObject;
			float newScaleF = Random.Range(minScale, maxScale);
			Vector3 newScaleV3 = new Vector3(newScaleF, newScaleF, newScaleF);
			newAsteroid.transform.localScale = newScaleV3;

			yield return new WaitForSeconds(Random.Range(minTime/(float)currentAstSpeed, maxTime/(float)currentAstSpeed));
		}
	}
	IEnumerator SpawnBonus(){
		while(!gameFinished){
			if(DiceRoll(ShieldRestoreChance)){
				Instantiate(ShieldRestore, new Vector3(Random.Range (-AsteroidHorizontalSpawnPosition,AsteroidHorizontalSpawnPosition),
				                                       0, AsteroidSpawnPosition), new Quaternion());
			}
			yield return new WaitForSeconds(5);
		}
	}
	public int GetCurrentAstSpeed(){
		return currentAstSpeed;
	}
	public int GetHighestAstSpeed(){
		return highestAstSpeed;
	}
	public int GetCurrentDist(){
		return currentDist;
	}
	public void ReduceCurrentSpeed(int amount){
		// Reduces as a percentage, minus 1000 base speed
		int extraSpeed = currentAstSpeed - StartingAsteroidSpeed;
		int speedReduction = Mathf.RoundToInt(extraSpeed * (amount)*0.01f);
		currentAstSpeed -= speedReduction;
	}
}
