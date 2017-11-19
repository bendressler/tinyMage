using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManeuvering : MonoBehaviour {

	Enemy thisEnemy;
	public int level;
	public float dashSpeed;
	public float dashTime;
	public float dashCooldown;
	public float evadeCooldown;




	// Use this for initialization
	void Start () {
		thisEnemy = GetComponent<Enemy> ();

		if (level == 1) {
			StartCoroutine (Dash ());
		}
		if (level == 2) {
			StartCoroutine (Evade ());
		}
		if (level == 3) {
			StartCoroutine (Dash ());
			StartCoroutine (Evade ());
		}
	}


	IEnumerator Evade(){
		yield return new WaitForSeconds (1f);
		Vector3 oldDir = thisEnemy.targetDir;
		float coin = Random.Range (0f, 1f);
		if (coin > 0.5f) {
			thisEnemy.targetDir = new Vector3 (1, 0, 0);
		} else {
			thisEnemy.targetDir = new Vector3 (-1, 0, 0);
		}
		yield return new WaitForSeconds (3f);
		thisEnemy.targetDir = oldDir;
		yield return new WaitForSeconds (evadeCooldown);
		StartCoroutine (Evade ());
	}

	IEnumerator Dash(){
		yield return new WaitForSeconds (1f);
		Debug.Log ("Dash");
		float oldSpeed;
		oldSpeed = thisEnemy.speed;
		thisEnemy.speed = dashSpeed;
		yield return new WaitForSeconds (dashTime);
		thisEnemy.speed = oldSpeed;
		yield return new WaitForSeconds (dashCooldown);
		StartCoroutine (Dash ());
	}

}
