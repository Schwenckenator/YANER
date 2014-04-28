using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	public GameObject Asteroid;
	public int StartingAsteroidSpeed;
	private int currentAstSpeed;
	private int highestAstSpeed;
	public float minTime;
	public float maxTime;
	// Use this for initialization
	void Start () {
		currentAstSpeed = StartingAsteroidSpeed;
		highestAstSpeed = currentAstSpeed;
		StartCoroutine("SpawnAsteroid");
	}

	void FixedUpdate(){
		currentAstSpeed++;
		if(currentAstSpeed > highestAstSpeed){
			highestAstSpeed = currentAstSpeed;
		}
	}

	IEnumerator SpawnAsteroid(){
		while(true){
			Instantiate(Asteroid, new Vector3(Random.Range (-5,5), 0, 50), new Quaternion());
			yield return new WaitForSeconds(Random.Range(minTime/(float)currentAstSpeed, maxTime/(float)currentAstSpeed));
		}
	}
	public int GetCurrentAstSpeed(){
		return currentAstSpeed;
	}
	public int GetHighestAstSpeed(){
		return highestAstSpeed;
	}
	public void ReduceCurrentSpeed(int amount){
		// Reduces as a percentage, minus 1000 base speed
		int extraSpeed = currentAstSpeed - 1000;
		int speedReduction = Mathf.RoundToInt(extraSpeed * (amount)*0.01f);
		Debug.Log (speedReduction.ToString());
		currentAstSpeed -= speedReduction;
	}
}