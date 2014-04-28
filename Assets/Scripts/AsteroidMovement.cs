using UnityEngine;
using System.Collections;

public class AsteroidMovement : MonoBehaviour {
	GameManager manager;
	ShipCondition playerShip;
	// Use this for initialization
	void Start () {
		manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
		playerShip = GameObject.FindGameObjectWithTag("PlayerShip").GetComponent<ShipCondition>();

		Vector3 newForce = new Vector3(0, 0, -1 * manager.GetCurrentAstSpeed());
		rigidbody.AddForce(newForce);
		StartCoroutine("CheckForDeath");
	}

	IEnumerator CheckForDeath(){
		while(true){
			if(transform.position.z < -5.0f){
				Destroy (gameObject);
			}
			yield return new WaitForSeconds(1.0f);
		}
	}
	void OnTriggerEnter(Collider col){
		if(col.CompareTag("PlayerShip")){
			manager.ReduceCurrentSpeed(25); // Percentage reduction here
			playerShip.TakeDamage(1); // For now, each asteroid deals 1 damage
		}
		if(!col.CompareTag("Box")){
			Debug.Log (col.tag);
			Destroy (gameObject);
		}
	}
}
