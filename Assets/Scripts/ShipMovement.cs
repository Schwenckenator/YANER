using UnityEngine;
using System.Collections;

public class ShipMovement : MonoBehaviour {
	public float horSpeed;
	public float slowCoEfficient;
	float distCovered;
	float journeyLength;

	// Update is called once per frame
	void FixedUpdate () {
		GetComponent<Rigidbody>().AddForce (new Vector3(Input.GetAxis("Horizontal") * horSpeed, 0, 0),ForceMode.Acceleration);
		if(Input.GetAxisRaw("Horizontal") == 0 && GetComponent<Rigidbody>().velocity != Vector3.zero) {
			GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity*slowCoEfficient;
		}
	}
}
