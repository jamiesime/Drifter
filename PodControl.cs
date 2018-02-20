﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PodControl : MonoBehaviour {


	public GameObject player;

	// assigned in inspector
	public float maxMoveSpeed;
	public float moveSpeed;

	// public for testing
	public float horRotSpeed;
	public float vertRotSpeed;

	// assigned in inspector
	public float rotSmooth;
	public float moveSmooth;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		// gets vert and hor input axes and applies rotation to player transform based on input
		handleRotationInput();

		// gets space key input and applies forward movement
		handleMovementInput();
		
	}

	public void handleRotationInput(){
		float horInput = Input.GetAxis("Horizontal");
		float vertInput = Input.GetAxis("Vertical");

		float noRot = 0f;
		float maxRot = 50f;

		if(horInput > 0.5f){
			horRotSpeed = Mathf.Lerp(horRotSpeed, maxRot, Time.deltaTime * rotSmooth);
		}
		if(horInput < -0.5f){
			horRotSpeed = Mathf.Lerp(horRotSpeed, -maxRot, Time.deltaTime * rotSmooth);
		}
		if(horInput == 0f){
			horRotSpeed = Mathf.Lerp(horRotSpeed, noRot, Time.deltaTime * (rotSmooth / 2));
		}

		if(vertInput > 0.5f){
			vertRotSpeed = Mathf.Lerp(vertRotSpeed, -maxRot, Time.deltaTime * rotSmooth);
		}
		if(vertInput < -0.5f){
			vertRotSpeed = Mathf.Lerp(vertRotSpeed, maxRot, Time.deltaTime * rotSmooth);
		}
		if(vertInput == 0f){
			vertRotSpeed = Mathf.Lerp(vertRotSpeed, noRot, Time.deltaTime * (rotSmooth / 2));
		}

		player.transform.Rotate(Time.deltaTime * vertRotSpeed, Time.deltaTime * horRotSpeed, 0, Space.Self);	
	}

	public void handleMovementInput(){

		bool moveInput = Input.GetButton("Move");

		if(moveInput){
			moveSpeed = Mathf.Lerp(moveSpeed, maxMoveSpeed, Time.deltaTime * moveSmooth);
		}
		else{
			moveSpeed = Mathf.Lerp(moveSpeed, 0f, Time.deltaTime * moveSmooth);
		}

		player.transform.Translate(Vector3.forward * moveSpeed, Space.Self);
	}
}