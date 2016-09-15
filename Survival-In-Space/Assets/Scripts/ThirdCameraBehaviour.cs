﻿using UnityEngine;
using System.Collections;

public class ThirdCameraBehaviour : MonoBehaviour {
	public float zoomSensitivity= 15.0f;
	public float zoomSpeed= 5.0f;
	public float zoomMin= 5.0f;
	public float zoomMax= 80.0f;
	
	private float zoom;
	
	
	void Start() {
		zoom = GetComponent<Camera>().fieldOfView;
	}
	
	void Update() {
		zoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSensitivity;
		zoom = Mathf.Clamp(zoom, zoomMin, zoomMax);
	}
	
	void LateUpdate() {
		GetComponent<Camera>().fieldOfView = Mathf.Lerp (GetComponent<Camera>().fieldOfView, zoom, Time.deltaTime * zoomSpeed);
	}
}
