using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	public GameObject Asteroid;

	public GameObject ShieldRestore;

	public int StartingAsteroidSpeed;
	public float AsteroidSpawnPosition; // How far back asteroids spawn
	public float AsteroidHorizontalSpawnPosition; // How far either side the asteroids can spawn

	public float minTime;
	public float maxTime;

	public float minScale;
	public float maxScale;

	private int currentAstSpeed;
	private int highestAstSpeed;
	private int currentDist = 0;

	public bool gameFinished = false;

	public float ShieldRestoreCooldown;
	public int ShieldRestoreChance;

	// Use this for initialization
	void Start () {
		currentAstSpeed = StartingAsteroidSpeed;
		highestAstSpeed = currentAstSpeed;
		StartCoroutine("SpawnAsteroid");
		StartCoroutine("CalculateDistance");
		StartCoroutine("SpawnBonus");

	}

	void FixedUpdate(){
		if(!gameFinished){
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
		while(!gameFinished){
			currentDist += currentAstSpeed/10;
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