using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

	public int health;
	public bool hitDetected;


	public float flashPeriod;
	public Color normalColor;
	public bool flashed;

	// Use this for initialization
	void Start () {
		normalColor = GetComponent<Renderer>().material.GetColor("_EmissionColor");
	}
	
	// Update is called once per frame
	void Update () {
		if(health <= 0){
			killSequence();
		}

		if(hitDetected){
			damageFlash();
		}
		else{
			if(flashed){
				resetEmission();
			}
		}

	}


	public void killSequence(){
		Destroy(this.gameObject);
		EnemySpawner.control.enemies.Remove(this.gameObject);
		GameObject.Find("Player").SendMessage("removeKilledEnemy", this.gameObject);
	}

	public void applyDamage(int amount){
		health -= amount;
		hitDetected = true;
	}

	public void damageFlash(){
		float duration = 0.1f;
		flashPeriod += Time.deltaTime/duration;
		GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.Lerp(normalColor, Color.red, flashPeriod));
		if(flashPeriod >= 1){
			hitDetected = false;
			flashed = true;
			flashPeriod = 0f;
		}
	}

	public void resetEmission(){
		float duration = 0.2f;
		flashPeriod += Time.deltaTime/duration;
		GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.Lerp(Color.red, normalColor, flashPeriod));
		if(flashPeriod >= 1){
			flashed = false;
			flashPeriod = 0f;
		}
	}

}