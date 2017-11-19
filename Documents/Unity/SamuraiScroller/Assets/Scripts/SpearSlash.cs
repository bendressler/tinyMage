using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearSlash : MonoBehaviour {

	public Animator spearAnim;
	public GameObject collider;
	public float timer;
	public float maxTime;
	public int level;
	GameObject player;
	Enemy thisEnemy;
	public float dashSpeed;
	public float dashTime;
	public float dashCooldown;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
		thisEnemy = GetComponentInParent<Enemy> ();

		timer = maxTime;
	}

	// Update is called once per frame
	void Update () {
		if (transform.position.z < 10) {
				if (timer > 0) {
					timer -= Time.deltaTime;
				} else {
				Attack ();
				if (level > 0) {
					StartCoroutine (SlashAgain (0.6f));
				}
				if (level > 1) {
					StartCoroutine (SlashAgain (1.2f));
				}
				if (level > 2) {
					StartCoroutine (Dash());
				}

					timer = maxTime;
			}
		}
	}

	void Attack(){
		spearAnim.SetTrigger ("attack");
		collider.SetActive (true);
		StartCoroutine (Slash ());

	}

	IEnumerator Slash(){
		yield return new WaitForSeconds (0.3f);
		collider.SetActive (false);
	}

	IEnumerator SlashAgain(float wait){
		yield return new WaitForSeconds (wait);
		Attack ();

	}

	IEnumerator Dash(){
		Debug.Log ("Dash");
		float oldSpeed;
		oldSpeed = thisEnemy.speed;
		thisEnemy.speed = dashSpeed;
		yield return new WaitForSeconds (dashTime);
		thisEnemy.speed = oldSpeed;
	}
}
