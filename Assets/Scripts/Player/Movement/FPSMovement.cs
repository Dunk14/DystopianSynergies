using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSMovement : MonoBehaviour {

	public static FPSMovement instance;
	public float walkAcceleration = 15f;
	public float maxWalkSpeed = 15f;
	public float jumpVelocity = 1500f;
	public float dashVelocity = 100f;
	public float maxSlope = 45f;

	private Rigidbody rb;
	private Vector2 horizontalMovement;
	private bool isGrounded = false;
	private bool doubleJumped = false;
	private int dashGauge = 100;
	private float dashReloadTime;

	// Use this for initialization
	void Start () {
		instance = this;
		dashReloadTime = Time.time;
		rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 horizontalVelocity = rb.velocity;
		horizontalVelocity.y = 0;

		if(rb.velocity.magnitude > maxWalkSpeed && isGrounded){
			// Clamp the horizontal component
			Vector3 newVelocity = Vector3.ClampMagnitude(horizontalVelocity, maxWalkSpeed);
			// Keep the original vertical velocity (jump speed)
			newVelocity.y = rb.velocity.y;
			rb.velocity = newVelocity;
		}

		transform.rotation = Quaternion.Euler (0, GetComponentInChildren<FPSCamera>().currentYRotation, 0);

		// Entrées clavier ZQSD déplaçant le joueur
		float x = 0, z = 0;
		if (Input.GetKey (KeyCode.Z) && Vector3.Project(rb.velocity, transform.forward).magnitude < maxWalkSpeed) {
			x += 5f;
		}
		if (Input.GetKey (KeyCode.S) && Vector3.Project(rb.velocity, transform.forward).magnitude < maxWalkSpeed) {
			x -= 5f;
		}
		if (Input.GetKey (KeyCode.Q) && Vector3.Project(rb.velocity, transform.right).magnitude < maxWalkSpeed) {
			z -= 5f;
		}
		if (Input.GetKey (KeyCode.D) && Vector3.Project(rb.velocity, transform.right).magnitude < maxWalkSpeed) {
			z += 5f;
		}

		// Arrêt prompt du glissement
		if ((!Input.GetKey (KeyCode.Z) && !Input.GetKey (KeyCode.S) && !Input.GetKey (KeyCode.Q) && !Input.GetKey (KeyCode.D))
		    && isGrounded) {
			rb.velocity /= 1.15f;
		}

		// Saut du joueur
		if (Input.GetKeyDown (KeyCode.Space) && isGrounded) {
			rb.AddForce (0, jumpVelocity, 0);
		}

		// Deuxième saut
		if (Input.GetKeyDown (KeyCode.Space) && !doubleJumped && !isGrounded) {
			rb.AddForce (0, jumpVelocity, 0);
			doubleJumped = true;
		}

		// Dash button
		if (Input.GetKey (KeyCode.E) && dashGauge > 0) {
			rb.AddRelativeForce (0, 0, dashVelocity);
			dashGauge -= 2;
		}

		if (!Input.GetKey (KeyCode.E) && dashGauge < 100 && Time.time > dashReloadTime) {
			dashGauge++;
			dashReloadTime = Time.time + 0.1f;
		}

		rb.AddRelativeForce (z * walkAcceleration, 0, x * walkAcceleration);
	}

	void OnCollisionStay(Collision other) {
		foreach (ContactPoint contact in other.contacts) {
			if (Vector3.Angle (contact.normal, Vector3.up) < maxSlope) {
				isGrounded = true;
				doubleJumped = false;
			}
		}
	}

	void OnCollisionExit() {
		isGrounded = false;
	}

	void OnGUI()
	{
		GUI.Label(new Rect(20, 20, 200, 200), "rigidbody velocity: " + rb.velocity);
		GUI.Label(new Rect(20, 40, 200, 200), "forward velocity: " + Vector3.Project(rb.velocity, transform.forward).magnitude);
		GUI.Label(new Rect(Display.main.systemWidth-100, Display.main.systemHeight-20, 200, 200), "Dash: " + dashGauge);
	}
}
