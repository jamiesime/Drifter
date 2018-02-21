using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidMovement : MonoBehaviour {

	public GameObject target;

	public float moveSpeed;

	// Use this for initialization
	void Start () {
		moveSpeed = Random.Range(0.5f, 3f);
		if(target != null){
			this.gameObject.transform.LookAt(target.transform);
		}
		else{
			transform.rotation = Random.rotation;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(target != null){
			this.gameObject.transform.LookAt(target.transform);
		}
		transform.Translate(Vector3.forward * (Time.deltaTime * moveSpeed));
	}
}
