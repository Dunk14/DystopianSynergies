using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumper : MonoBehaviour {

	public float jumpForce = 500f;

	void Start () {
		
	}

	void FixedUpdate () {
		
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "GameController") {
			Jump (other.gameObject);
		}
	}

	void OnTriggerStay(Collider other) {
		if (other.gameObject.tag == "GameController") {
			Jump (other.gameObject);
		}
	}

	void Jump(GameObject player) {
		Rigidbody rb = player.GetComponent<Rigidbody> ();
		rb.AddForce (0, jumpForce, 0);
	}
}
