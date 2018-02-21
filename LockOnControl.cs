using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOnControl : MonoBehaviour {

	public GameObject player;
	public Camera cam;
	public Transform currentTarget;
	public Vector3 currentTargetScreenPos;

	public bool lockedOn;

	public List<GameObject> availableTargets;
	public List<GameObject> targetsInView;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if(EnemySpawner.control.enemies.Count > 0){
			List<GameObject> targets = searchForTargetsInRange(EnemySpawner.control.enemies);

			if(Input.GetButtonDown("LockOn")){
				if(!lockedOn){
					if(targets.Count > 0){
						currentTarget = targets[0].transform;
						Behaviour halo = (Behaviour)targets[0].GetComponent("Halo");
						halo.enabled = true; // false
						currentTargetScreenPos = cam.WorldToScreenPoint(targets[0].transform.position);
						lockedOn = true;
					}
				}
				else {
					lockedOn = false;
				}
			}

			if(lockedOn){
				// player.transform.LookAt(targets[0].transform.position);
				player.transform.rotation = Quaternion.Lerp(player.transform.rotation, Quaternion.LookRotation(targets[0].transform.position), Time.deltaTime * 10f);
			}
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
