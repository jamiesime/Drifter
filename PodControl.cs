using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PodControl : MonoBehaviour {


	public GameObject player;

	// just to initialise
	public Vector3 dir;

	// assigned in inspector
	public float maxMoveSpeed;
	public float moveSpeed;

	// public for testing
	public float horRotSpeed;
	public float vertRotSpeed;

	// assigned in inspector
	public float rotSmooth;
	public float moveSmooth;

	// used to only flip once at a time
	public bool flipping;
	// stores target value of lerp when flipping
	public float currentDir;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		// gets vert and hor input axes and applies rotation to player transform based on input
		handleRotationInput();

		// gets space key input and applies forward movement
		handleMovementInput();

		// listens for flip direction input and inverts player rotation on button down
		handleFlipDirection();
		
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
		bool reverseInput = Input.GetButton("Reverse");

		if(moveInput || reverseInput){
			moveSpeed = Mathf.Lerp(moveSpeed, maxMoveSpeed, Time.deltaTime * moveSmooth);
		}
		else{
			moveSpeed = Mathf.Lerp(moveSpeed, 0f, Time.deltaTime * moveSmooth);
		}

		if(moveInput){
			dir = Vector3.forward;
		}

		if(reverseInput){
			dir = Vector3.back;
		}

		player.transform.Translate(dir * moveSpeed, Space.Self);
	}

	public void handleFlipDirection(){
		bool flipInput = Input.GetButtonDown("Flip");
		float duration = (10f * Time.deltaTime);

		if(flipInput && !flipping){
			StartCoroutine(setRotator(new Vector3(0f, 180f, 0f), 0.5f));
			flipping = true;
		}

		if(flipping){
			flipping = false;
		}
	}

	IEnumerator setRotator(Vector3 byAngles, float inTime) {
           Quaternion fromAngle = player.transform.rotation;
           Quaternion toAngle = Quaternion.Euler(transform.eulerAngles + byAngles);
           for(float t = 0f; t < 1; t += Time.deltaTime/inTime) {
                player.transform.rotation = Quaternion.Lerp(fromAngle, toAngle, t);
                yield return null;
           }
      }

	IEnumerator endFlip(){
		yield return new WaitForSeconds(1f);
		// flippedRotation = new Quaternion (0f,0f,0f,0f);
		flipping = false;
	}


	public float getMousePosition(string axis){
		int deadZone = 200;
		Vector3 mousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);

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
