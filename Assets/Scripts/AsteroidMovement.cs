using UnityEngine;
using System.Collections;

public class AsteroidMovement : MonoBehaviour {
	GameManager manager;
	// Use this for initialization
	void Start () {
		manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

		Vector3 newForce = new Vector3(0, 0, -1 * manager.GetCurrentAstSpeed());
		rigidbody.AddForce(newForce);
		StartCoroutine("CheckForDeath");
	}

	IEnumerator CheckForDeath(){
		while(true){
			if(transform.position.z < -1.0f){
				Destroy (gameObject);
			}
			yield return new WaitForSeconds(1.0f);
		}
	}
	void OnTriggerEnter(Collider col){
		manager.ReduceCurrentSpeed(25); // Percentage reduction here
		Destroy (gameObject);
	}
}
