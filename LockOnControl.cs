using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOnControl : MonoBehaviour {

	public GameObject player;
	public Camera cam;
	public Transform currentTarget;
	public Vector3 currentTargetScreenPos;

	public int targetNumber;

	public bool lockedOn;

	public List<GameObject> targets;
	public List<GameObject> availableTargets;
	public List<GameObject> targetsInView;

	// Use this for initialization
	void Start () {
		targetNumber = -1;
	}
	
	// Update is called once per frame
	void Update () {
		
		if(EnemySpawner.control.enemies.Count > 0){
			targets = searchForTargetsInRange(EnemySpawner.control.enemies);

			if(Input.GetButtonDown("LockOn")){
				lockOnToTarget(targets);
			}

			if(Input.GetButtonDown("LockOff")){
				lockedOn = false;
			}

			if(lockedOn){
				if(currentTarget != null){
					Quaternion targetRotation = Quaternion.LookRotation(currentTarget.transform.position - player.transform.position);
					player.transform.rotation = Quaternion.Slerp(player.transform.rotation, targetRotation, 10f * Time.deltaTime);
				}
			}
		}

	}	



	public void lockOnToTarget(List<GameObject> targets){
			if(targets.Count > 0){
				if(targetNumber < targets.Count -1){
				Debug.Log(targets.Count);
				targetNumber += 1;
				}
				else{
					targetNumber = 0;
					lockedOn = false;
				}

				currentTarget = targets[targetNumber].transform;
				Behaviour halo = (Behaviour)targets[targetNumber].GetComponent("Halo");
				halo.enabled = true; // false
				currentTargetScreenPos = cam.WorldToScreenPoint(targets[targetNumber].transform.position);
				lockedOn = true;
			}
		}


	public List<GameObject> populateTargetList(){
		availableTargets = EnemySpawner.control.enemies;
		return availableTargets;
	}

	public List<GameObject> searchForTargetsInRange(List<GameObject> enemies){
		bool remove = false;
		foreach (GameObject enemy in enemies){
			Rect screenRect = new Rect(0, 0, Screen.width, Screen.height);
			Vector3 enemyPos = cam.WorldToScreenPoint(enemy.transform.position);
			Vector2 enemyPosScreen = new Vector2(enemyPos.x, enemyPos.y);
			if(screenRect.Contains(enemyPosScreen) && enemyPos.z > 0f){
				if(!targetsInView.Contains(enemy)){
					targetsInView.Add(enemy);
				}
			}
			else {
				if(targetsInView.Contains(enemy)){
					targetsInView.Remove(enemy);
				}
			}
			}
		return targetsInView;
	}

	public void removeKilledEnemy(GameObject killedEnemy){
		targetsInView.Remove(killedEnemy);
	}
		
}
