using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonSatellite : MonoBehaviour {

	public GameObject target;
	public GameObject smallShell;
	public GameObject barrel;

	public float firePeriod;

	// Use this for initialization
	void Start () {
		target = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {

		lookAtPlayer();

		fireCannon();
		
	}


	public void lookAtPlayer(){
		Quaternion targetRotation = Quaternion.LookRotation(target.transform.position - transform.position);
		transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10f * Time.deltaTime);
	}

	public void fireCannon(){

		Vector3 rightAmmoSpawnPos = new Vector3(barrel.transform.position.x, barrel.transform.position.y, barrel.transform.position.z);	
		
		if(firePeriod == 0f){
			GameObject newBullet = Instantiate(smallShell, rightAmmoSpawnPos, transform.rotation);
			newBullet.transform.parent = transform;
		}
		firePeriod += Time.deltaTime;

		if(firePeriod > 1f){
			firePeriod = 0f;
		}			
	}

}
