using UnityEngine;
using System.Collections;

public class ObjectMovement : MonoBehaviour {
	public string type;
	GameManager manager;
	ShipCondition playerShip;
	// Use this for initialization
	void Start () {
		manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
		playerShip = GameObject.FindGameObjectWithTag("PlayerShip").GetComponent<ShipCondition>();

		Vector3 newForce = new Vector3(0, 0, -1 * manager.GetCurrentAstSpeed());
		GetComponent<Rigidbody>().AddForce(newForce);
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
		if(type == "Asteroid"){
			if(col.CompareTag("PlayerShip")){
				manager.ReduceCurrentSpeed(25); // Percentage reduction here
				playerShip.TakeDamage(1); // For now, each asteroid deals 1 damage
			}
		}else if(type == "Shield"){
			if(col.CompareTag("PlayerShip")){
				playerShip.RestoreShield();
			}
		}

		if(!col.CompareTag("Box")){
			Debug.Log (col.tag);
			Destroy (gameObject);
		}
	}
}
