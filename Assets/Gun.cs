using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

	public Camera mainCamera;
	public float rotateSpeed = .05f;
	public float holdHeight = -.5f;
	public float holdSlide = .5f;

	private float targetXRotation;
	private float targetYRotation;
	private float targetXRotationV;
	private float targetYRotationV;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = mainCamera.transform.position + (Quaternion.Euler(0, targetYRotation, 0) * new Vector3(holdSlide, holdHeight, 0));
	
		targetXRotation = Mathf.SmoothDamp (targetXRotation, mainCamera.GetComponent<FPSCamera> ().xRotation, ref targetXRotationV, rotateSpeed); 
		targetYRotation = Mathf.SmoothDamp (targetYRotation, mainCamera.GetComponent<FPSCamera> ().yRotation, ref targetYRotationV, rotateSpeed);
	
		transform.rotation = Quaternion.Euler (targetXRotation, targetYRotation, 0);
	}
}
