using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour {


	public float destroyTime;

	void OnCollisionEnter(Collision other){
		if(other.gameObject.tag == "Enemy"){
			other.gameObject.SendMessage("applyDamage", 1);
			Destroy(this.gameObject);
		}
		if(other.gameObject.tag == "Player"){
			other.gameObject.SendMessage("applyDamage", Random.Range(0.3f, 1.5f));
			Destroy(this.gameObject);
		}
	}


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
