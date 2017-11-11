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
	float currentXRotation;
	public float currentYRotation;
	float xRotationV = 0.0f;
	float yRotationV = 0.0f;

	// Use this for initialization
	void Start () {
		instance = this;
	}

	// Update is called once per frame
	void Update () {
		xRotation -= Input.GetAxis("Mouse Y") * lookSensitivity;
		yRotation += Input.GetAxis("Mouse X") * lookSensitivity;

		xRotation = Mathf.Clamp(xRotation, -minVertical, maxVertical);

		currentXRotation = Mathf.SmoothDamp(currentXRotation, xRotation, ref xRotationV, 0);
		currentYRotation = Mathf.SmoothDamp(currentYRotation, yRotation, ref yRotationV, 0);

		transform.rotation = Quaternion.Euler(currentXRotation, currentYRotation, 0);
	}
}﻿