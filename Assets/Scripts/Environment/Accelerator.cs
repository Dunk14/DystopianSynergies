using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accelerator : MonoBehaviour {

	public float acceleratorForce = 50f;
	public float maxAccelerateSpeed = 20f;
	private bool flag;
	private float previousMaxWalkSpeed;
	private float timeOut;

	void Start () {
	}

	void Update () {
		if (Time.time > timeOut && flag) {
			flag = false;
			FPSMovement.instance.maxWalkSpeed = previousMaxWalkSpeed;
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "GameController") {
			Accelerate(other.gameObject);
		}
	}

	void OnTriggerStay(Collider other) {
		if (other.gameObject.tag == "GameController") {
			Accelerate(other.gameObject);
		}
	}

	void Accelerate(GameObject player) {
		if (!flag) {
			// Set new< max speed
			previousMaxWalkSpeed = FPSMovement.instance.maxWalkSpeed;
			flag = true;
		}
		FPSMovement.instance.maxWalkSpeed = 50f;
		timeOut = Time.time + 3f;
	}
}
