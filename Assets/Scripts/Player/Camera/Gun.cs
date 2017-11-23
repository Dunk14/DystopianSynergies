using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

	public Camera mainCamera;
	public float rotateSpeed = 0.05f;
	public float holdHeight = -0.15f;
	public float holdSlide = 0.35f;
	public float holdDepth = 0.4f;

	private float targetXRotation;
	private float targetYRotation;
	private float targetXRotationV;
	private float targetYRotationV;
	private float defaultYRotation;

	public float gunbobAmountX = .5f;
	public float gunbobAmountY = .5f;
	public float currentGunbobX;
	public float currentGunbobY;

	public float ratioHipHold = 1f;
	public float ratioHipHoldV;
	public float hipToAimSpeed = 0.1f;
	public bool isAiming = false;

	void Awake () {
		defaultYRotation = transform.rotation.y;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
		if (Input.GetMouseButtonDown (1))
			isAiming = !isAiming;

		if (isAiming)
			ratioHipHold = Mathf.SmoothDamp (ratioHipHold, 0, ref ratioHipHoldV, hipToAimSpeed);
		else
			ratioHipHold = Mathf.SmoothDamp (ratioHipHold, 1, ref ratioHipHoldV, hipToAimSpeed);

		currentGunbobX = Mathf.Sin (mainCamera.GetComponent<FPSCamera> ().headbobStepCounter) * gunbobAmountX;
		currentGunbobY = Mathf.Cos (mainCamera.GetComponent<FPSCamera> ().headbobStepCounter * 2) * gunbobAmountY;

		transform.position = mainCamera.transform.position + (Quaternion.Euler(targetXRotation, targetYRotation, 0) * new Vector3(holdSlide * ratioHipHold + currentGunbobX, holdHeight * ratioHipHold + currentGunbobY, holdDepth));
	
		targetXRotation = Mathf.SmoothDamp (targetXRotation, mainCamera.GetComponent<FPSCamera> ().xRotation, ref targetXRotationV, rotateSpeed); 
		targetYRotation = Mathf.SmoothDamp (targetYRotation, mainCamera.GetComponent<FPSCamera> ().yRotation, ref targetYRotationV, rotateSpeed);
	
		transform.rotation = Quaternion.Euler (0, targetYRotation-90, -targetXRotation+10);
	}

	Vector3 getPositionWithoutBob() {
		//Vector3 position = new Vector3(mainCamera.GetComponentInParent<Rigidbody> ().transform.position.x
		return Vector3.zero;
	}
}
