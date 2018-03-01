using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatelliteMovement : MonoBehaviour {


	public float vertRotSpeed;
	public float horRotSpeed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		handleRotation();

	}


	public void handleRotation(){

		transform.Rotate(Time.deltaTime * vertRotSpeed, Time.deltaTime * horRotSpeed, 0, Space.Self);

	}

}
