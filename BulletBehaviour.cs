using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour {


	public float destroyTime;

	// Use this for initialization
	void Start () {
		this.transform.parent = null;
	}
	
	// Update is called once per frame
	void Update () {
		destroyTime += Time.deltaTime;
		transform.Translate(Vector3.forward, Space.Self);
		if(destroyTime > 1.5f){
			Destroy(this.gameObject);
		}
	}
}
