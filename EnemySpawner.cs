using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {


	public static EnemySpawner control;

	public GameObject[] initialEnemyList;
	public List<GameObject> enemies;

	void Awake(){
		if (control == null){
			control = this;
		}
	}

	// Use this for initialization
	void Start () {
		initialEnemyList = GameObject.FindGameObjectsWithTag("Enemy");
		foreach (GameObject enemy in initialEnemyList){
			enemies.Add(enemy);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
