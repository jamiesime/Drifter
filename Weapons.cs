using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour {

	public GameObject smallCannon;
	public GameObject smallShell;
	public float firePeriod;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		// gets right fire and handles firing and animations
		handleSmallCannon();
	}

	public void handleSmallCannon(){
		bool rightFireKey = Input.GetButton("RightFire");
		float rightFireTrigger = Input.GetAxis("RightFire");
		Vector3 rightAmmoSpawnPos = new Vector3(smallCannon.transform.position.x, smallCannon.transform.position.y, smallCannon.transform.position.z);

		if(rightFireKey || rightFireTrigger > 0.5f){

			if(firePeriod == 0f){
				GameObject newBullet = Instantiate(smallShell, rightAmmoSpawnPos, transform.rotation);
				newBullet.transform.parent = smallCannon.transform;
			}
			
			firePeriod += Time.deltaTime;

			if(firePeriod > 0.1f){
				firePeriod = 0f;
			}
		}
		else{
			firePeriod = 0f;
		}
	}
}
