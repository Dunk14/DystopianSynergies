using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FPSCamera : MonoBehaviour {

	public static FPSCamera instance;
	public float lookSensitivity = 5.0f;
	public int maxVertical = 90;
	public int minVertical = 90;
	public float xRotation;
	public float yRotation;
	public float currentXRotation;
	public float currentYRotation;
	float xRotationV = 0.0f;
	float yRotationV = 0.0f;

	public float currentAimRatio = 1f;
	public float headbobSpeed;
	public float headbobStepCounter;
	public float headbobAmountX;
	public float headbobAmountY;
	Vector3 parentLastPos;
	public float eyeHeightRatio = 0.9f;

	void Awake () {
		parentLastPos = transform.parent.position;
	}

	// Use this for initialization
	void Start () {
		instance = this;
	}

	// Update is called once per frame
	void Update () {
		if (transform.parent.GetComponent<FPSMovement> ().isGrounded)
			headbobStepCounter += Vector3.Distance(parentLastPos, transform.parent.position) * headbobSpeed;

		transform.localPosition = getHeadbob ();

		parentLastPos = transform.parent.position;
			
		xRotation -= Input.GetAxis("Mouse Y") * lookSensitivity;
		yRotation += Input.GetAxis("Mouse X") * lookSensitivity;

		xRotation = Mathf.Clamp(xRotation, -minVertical, maxVertical);

		currentXRotation = Mathf.SmoothDamp(currentXRotation, xRotation, ref xRotationV, 0);
		currentYRotation = Mathf.SmoothDamp(currentYRotation, yRotation, ref yRotationV, 0);

		transform.rotation = Quaternion.Euler(currentXRotation, currentYRotation, 0);
	}

	Vector3 getHeadbob () {
		return new Vector3(Mathf.Sin(headbobStepCounter) * headbobAmountX * currentAimRatio,
					(Mathf.Cos(headbobStepCounter * 2) * headbobAmountY * -1 * currentAimRatio) + (transform.parent.localScale.y * eyeHeightRatio) - (transform.parent.localScale.y / 2),
					transform.localScale.z);
	}﻿
}