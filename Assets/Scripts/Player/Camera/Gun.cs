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

	void Awake () {
		defaultYRotation = transform.rotation.y;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
		currentGunbobX = Mathf.Sin (mainCamera.GetComponent<FPSCamera> ().headbobStepCounter) * gunbobAmountX;
		currentGunbobY = Mathf.Cos (mainCamera.GetComponent<FPSCamera> ().headbobStepCounter * 2) * gunbobAmountY;

		transform.position = mainCamera.transform.position + (Quaternion.Euler(targetXRotation, targetYRotation, 0) * new Vector3(holdSlide + currentGunbobX, holdHeight + currentGunbobY, holdDepth));
	
		targetXRotation = Mathf.SmoothDamp (targetXRotation, mainCamera.GetComponent<FPSCamera> ().xRotation, ref targetXRotationV, rotateSpeed); 
		targetYRotation = Mathf.SmoothDamp (targetYRotation, mainCamera.GetComponent<FPSCamera> ().yRotation, ref targetYRotationV, rotateSpeed);
	
		transform.rotation = Quaternion.Euler (0, targetYRotation-75, -targetXRotation);
	}

	Vector3 getPositionWithoutBob() {
		//Vector3 position = new Vector3(mainCamera.GetComponentInParent<Rigidbody> ().transform.position.x
		return Vector3.zero;
	}
}
