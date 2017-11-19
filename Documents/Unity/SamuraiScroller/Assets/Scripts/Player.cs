using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {
	/*
	public int [] chargeCost;
	public float [] chargeSpeed;
	public float [] chargeTimer;
	public Sprite[] chargeSprites;

	public int currentCharge;
	public int currentStep;
	public float currentSpeed;

	bool moving;
	Vector3 target;
	public GameObject corpse;

	public Sprite strikeSpr;



	// Use this for initialization
	void Start () {
		moving = false;
		currentSpeed = 1;
		currentStep = 0;
		NewStep (0);
	}

	// Update is called once per frame
	void Update () {

		if (moving) {
			transform.position = Vector3.MoveTowards (transform.position, target, currentSpeed * 0.1f);
			CheckMovement ();
		}

	}

	// Handling collisions

	void OnTriggerEnter(Collider col){

		if (col.gameObject.CompareTag ("Obstacle")) {
			CollideObstacle (col);
		}

		if (col.gameObject.CompareTag ("EnemyWeapon")) {
			CollideEnemyWeapon (col);
		}

		if (col.gameObject.CompareTag("Enemy")){
			CollideEnemy (col);
		}
	}


	void CollideObstacle(Collider col){
		Cooldown ();
		if (col.GetComponent<TreeScript> () != null) {
			col.GetComponent<TreeScript> ().SpawnRaven ();
		}
	}

	void CollideEnemyWeapon(Collider col){
		SceneManager.LoadScene (0);

	}

	void CollideEnemy(Collider col){
		GameObject newCorpse = Instantiate (corpse, col.transform.position, Quaternion.identity);

		col.transform.position = new Vector3 (Random.Range(-4.5f,4.5f), 0, 10);

		AddCharge (1);

		RefreshTimer ();

	}

	// Handling movement

	void CheckMovement(){
		if (transform.position == target) {
			moving = false;
		}
	}

	public void StartMoving(Vector3 newTarget){
		moving = true;
		target = newTarget;
	}


	// Handling speed

	void AddCharge(int toAdd){
		currentCharge += toAdd;
		if (currentStep < (chargeCost.Length - 1)) {
			if (currentCharge > chargeCost [currentStep + 1]) {
				NewStep (currentStep + 1);
			}
		}
	}

	void NewStep(int newStep){
		currentStep = newStep;
		currentSpeed = chargeSpeed [currentStep];
		gameObject.GetComponentInChildren<SpriteRenderer> ().sprite = chargeSprites [currentStep];

	}

	void RefreshTimer(){
		if (currentStep > 0) {
			StopCoroutine ("Timer");
			StartCoroutine ("Timer");
		}
	}

	IEnumerator Timer(){
		float countdown = chargeTimer[currentStep];
		while (countdown > 0) {
			countdown -= Time.deltaTime;
			yield return null;
		}
		Cooldown ();
	}

	void Cooldown(){
		if (currentStep > 0) {
			currentCharge = chargeCost [currentStep - 1];
			NewStep (currentStep - 1);
		}
	}

*/
}
