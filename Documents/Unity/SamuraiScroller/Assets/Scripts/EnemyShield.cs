using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShield : MonoBehaviour {

	public Sprite shieldedSpr;
	public Sprite shieldOffSpr;
	public float timeShielded;
	public float timeOff;
	public Enemy owner;
	public bool shielded;
	public float dashSpeed;
	public int level;
	Enemy thisEnemy;

	float timer;

	// Use this for initialization
	void Start () {
		timer = timeShielded;
		shielded = true;
		thisEnemy = GetComponent<Enemy> ();
	}
	
	void Update () {
		if (owner.hasWeapon) {
			if (timer > 0) {
				timer -= Time.deltaTime;
			} else {
				ToggleShield ();
			}
		}
	}

	void ToggleShield(){
		if (shielded) {
			ShieldOff ();
		} else {
			ShieldOn ();
		}
		shielded = !shielded;
	}

	void ShieldOff(){
		timer = timeOff;
		GetComponentInChildren<SpriteRenderer> ().sprite = shieldOffSpr;
	}

	void ShieldOn(){
			timer = timeShielded;
			GetComponentInChildren<SpriteRenderer> ().sprite = shieldedSpr;
		if (level == 3) {
			StartCoroutine(Dash ());
		}
	}



	IEnumerator Dash(){
		float oldSpeed;
		oldSpeed = thisEnemy.speed;
		thisEnemy.speed = dashSpeed;
		yield return new WaitForSeconds (timeShielded);
		thisEnemy.speed = oldSpeed;
	}
}
