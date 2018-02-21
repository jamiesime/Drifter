using System.Collections;
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
		float horInput = Input.GetAxis("Horizontal") + getMousePosition("x");
		float vertInput = Input.GetAxis("Vertical") + getMousePosition("y");

		float noRot = 0f;
		float maxRot = 70f;

		if(horInput != 0f){
			horRotSpeed = Mathf.Lerp(horRotSpeed, maxRot * horInput, Time.deltaTime * rotSmooth);
		}
		else{
			horRotSpeed = Mathf.Lerp(horRotSpeed, noRot, Time.deltaTime * (rotSmooth / 2));
		}

		if(vertInput != 0f){
			vertRotSpeed = Mathf.Lerp(vertRotSpeed, maxRot * -vertInput, Time.deltaTime * rotSmooth);
		}
		else{
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


	public float getMousePosition(string axis){
		int deadZone = 200;
		Vector3 mousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
		Debug.Log(mousePos);

		if(axis == "x"){
			if(mousePos.x > 0.6f){
				return mousePos.x;
			}
			if(mousePos.x < 0.4f){
				float invertedPos = (mousePos.x - 1);
				return invertedPos;
			}
		}
		if(axis == "y"){
			if(mousePos.y > 0.6f){
				return mousePos.y;
			}
			if(mousePos.y < 0.4f){
				float invertedPos = (mousePos.y - 1);
				return invertedPos;
			}
		}
		return 0f;
	}
}
