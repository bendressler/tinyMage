using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerCont : MonoBehaviour {

	//manages movement
	public float currentSpeed;
	public float walkSpeed;
	public float dashSpeed;
	public int dashStacks;
	public float dashDistance;
	public float swipeDelta; 
	float dashRemaining;
	Vector3 target;

	//manages energy
	public float energy;
	public float maxEnergy;
	public float energyReg;
	public float energySpend;

	public float bonusMultiplier;
	public float bonusEnergy;

	//manages life
	int hp;

	//references
	GameManager gameManager;
	SwipeController swipeCont;
	ComboCounter combo;

	//art & effects
	public GameObject blood;
	public GameObject bodyObject;
	public GameObject shadowObject;
	public GameObject hatDrop;
	public GameObject coatDrop;
	public Sprite[] bodySprites;
	public Sprite[] shadowSprites;
	public GameObject splatter;
	public Animator swordAnim;

	//UI
	public GameObject energyBar;



	// Use this for initialization
	void Start () {

		//set references
		gameManager = GameObject.Find ("GameManager").GetComponent<GameManager> ();
		combo = GetComponent<ComboCounter> ();
		swipeCont = gameManager.GetComponent<SwipeController> ();

		hp = 2; //set amount of lives

		currentSpeed = walkSpeed;	//set speed to walk
		dashRemaining = dashDistance;	//set dash counter to maximum distance
	}


	// Update is called once per frame
	void Update () {

		dashStacks = (int)energy;	//creates a round number of stacks based on the energy

		//set speed and goal for dashing & subtract from dash counter
		if (swipeCont.dashing) {
			currentSpeed = dashSpeed;
			target = swipeCont.dashGoal;
			dashRemaining -= Time.deltaTime;
		}

		//set speed and goal for walking and reset combo counter
		if (swipeCont.walking) {
			currentSpeed = walkSpeed;
			target = swipeCont.walkGoal;
			combo.killStacks = 0;
		}

		Move ();

		//energy regeneration and ceiling
		if (energy < maxEnergy) {
			energy += energyReg;
		} else {
			energy = maxEnergy;
		}

		//stop dash if counter expires or goal is reached
		if ((dashRemaining <= 0) || (transform.position == swipeCont.dashGoal)) {
			swipeCont.StopDashing();
			dashRemaining = dashDistance;
		}

		//update energy UI
		energyBar.GetComponent<RectTransform> ().localScale = new Vector3 (1, energy / maxEnergy, 1);

	}


	// Handling collisions

	void OnTriggerEnter(Collider col){

		col.gameObject.SendMessage ("Collision", gameObject, SendMessageOptions.DontRequireReceiver);

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

	void OnTriggerStay(Collider col){
		if (col.gameObject.CompareTag ("Obstacle")) {
			CollideObstacle (col);
		}
	}

	void CollideObstacle(Collider col){

		Debug.Log ("Colliding");
		Vector3 dir;
		dir = col.transform.position - transform.position;
		transform.position = transform.position - ((dir.normalized)*0.3f);

	}



	//take damage from colliding with weapons
	void CollideEnemyWeapon(Collider col){
		CreateSplatter (gameObject, col.gameObject);
		TakeDamage ();

	}

	void CollideEnemy(Collider col){
		swordAnim.SetTrigger ("attack");	//trigger slash animation
		//check if enemy is shielded
		if (col.GetComponent<EnemyShield> () != null && col.GetComponent<EnemyShield> ().shielded) {
			transform.position = new Vector3 (col.transform.position.x, 0, col.transform.position.z - 1f); //let player bounce off the shield
		} else {
			//deal damage and, if damage is lethal, kill the enemy
			if (col.GetComponent<Enemy> ().TakeDamage ()) {
				KillEnemy (col.gameObject);
				CreateSplatter (col.gameObject, gameObject);
			}
		}

	}

	//take damage and restart game if lethal
	public void TakeDamage(){
		bool dead = false;
		hp -= 1;
		if (hp > -1) {
			bodyObject.GetComponent<SpriteRenderer>().sprite = bodySprites [hp];
			shadowObject.GetComponent<SpriteRenderer>().sprite = shadowSprites [hp];
		}
		if (hp == 1) {
			GameObject newCoatDrop;
			newCoatDrop = Instantiate (coatDrop, transform.position, Quaternion.identity);
		}
		if (hp == 0) {
			GameObject newHatDrop;
			newHatDrop = Instantiate (hatDrop, transform.position, Quaternion.identity);
		}

		if (hp == -1) {
			SceneManager.LoadScene (0);
		}
	}
		

	// Handling Killing

	void KillEnemy(GameObject enemy){
		
		LogKill (enemy);		//send kill to combo counter script
		energy += enemy.GetComponent<Enemy> ().energyYield * bonusMultiplier;	//add enemies energy yield
		gameManager.score += 1;		//add score
	}

	void LogKill(GameObject enemy){
		combo.RegisterKill (Time.time, enemy);	// send a timestamp and the killed enemy to the combo counter script
	}

	public void Boost(int kills){
		energy += bonusEnergy * kills;
	}

	void CreateSplatter(GameObject bleeder, GameObject cause){
		GameObject newSplatter;
		newSplatter = Instantiate (splatter, bleeder.transform.position, Quaternion.identity);
		newSplatter.transform.LookAt (cause.transform);
	}

	void Move(){

		transform.position = Vector3.MoveTowards (transform.position, target, currentSpeed * 0.1f);

	}



}
