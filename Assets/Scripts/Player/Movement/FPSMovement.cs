using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSMovement : MonoBehaviour {

	public static FPSMovement instance;
	public float walkAcceleration = 15f;
	public float maxWalkSpeed = 15f;
	public float jumpVelocity = 2500f;
	public float dashVelocity = 15000f;
	public float maxSlope = 45f;

	private Rigidbody rb;
	private Vector2 horizontalMovement;
	public bool isGrounded = false;
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
		if (Input.GetKey (KeyCode.Z) && transform.InverseTransformDirection(rb.velocity).z < maxWalkSpeed) {
			x += 3f;
			if (transform.InverseTransformDirection (rb.velocity).z > -maxWalkSpeed)
				x *= 3f;
		}
		if (Input.GetKey (KeyCode.S) && transform.InverseTransformDirection(rb.velocity).z > -maxWalkSpeed) {
			x -= 3f;
			if (transform.InverseTransformDirection(rb.velocity).z < maxWalkSpeed)
				x *= 3f;
		}
		if (Input.GetKey (KeyCode.Q) && transform.InverseTransformDirection(rb.velocity).x > -maxWalkSpeed) {
			z -= 3f;
			if (transform.InverseTransformDirection(rb.velocity).x < maxWalkSpeed)
				z *= 3f;
		}
		if (Input.GetKey (KeyCode.D) && transform.InverseTransformDirection(rb.velocity).x < maxWalkSpeed) {
			z += 3f;
			if (transform.InverseTransformDirection(rb.velocity).x > -maxWalkSpeed)
				z *= 3f;
		}

		// Arrêt prompt du glissement à terre
		if ((!Input.GetKey (KeyCode.Z) && !Input.GetKey (KeyCode.S) && !Input.GetKey (KeyCode.Q) && !Input.GetKey (KeyCode.D)) && isGrounded) {
			rb.velocity *= 0.8f;
		}

		// Arrêt latent du glissement en l'air
		if ((!Input.GetKey (KeyCode.Z) && !Input.GetKey (KeyCode.S) && !Input.GetKey (KeyCode.Q) && !Input.GetKey (KeyCode.D)) && !isGrounded) {
			rb.velocity *= 0.9999f;
			Vector3 newVelocity = rb.velocity * 0.99f;
			// Keep the original vertical velocity (jump speed)
			newVelocity.y = rb.velocity.y;
			rb.velocity = newVelocity;
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
		if (Input.GetKeyDown(KeyCode.E) && dashGauge > 0) {
			float newDashVelocity = dashVelocity;
			if (dashGauge < 20)
				newDashVelocity -= dashGauge;
			rb.AddRelativeForce (0, 0, newDashVelocity);
			dashGauge -= 20;
		}

		// Dash recharge
		if (!Input.GetKey (KeyCode.E) && dashGauge < 100 && Time.time > dashReloadTime) {
			dashGauge += 2;
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
		GUI.Label(new Rect(20, 40, 200, 200), "local velocity: " + transform.InverseTransformDirection(rb.velocity));;
		GUI.Label(new Rect(Display.main.systemWidth-100, Display.main.systemHeight-20, 200, 200), "Dash: " + dashGauge);
	}
}
