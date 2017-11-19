using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public float speed;
	GameManager gameManager;

	public bool hasWeapon;
	public int hp;
	public GameObject weaponDrop;
	public Sprite[] hpSprites;
	public GameObject weaponCollider;
	public SpriteRenderer spriteRenderer;

	public GameObject corpse;
	public GameObject specialCorpse;

	public float energyYield;

	public Vector3 direction;
	public Vector3 targetDir;

	Transform obstacle;

	//interact with spawner
	private GameObject objSpawn;
	private int SpawnerID;

	// Use this for initialization
	void Start () {

		speed += 0.01f;
		targetDir = new Vector3 (0, 0, -1);

		gameManager = GameObject.Find ("GameManager").GetComponent<GameManager> ();
		gameManager.enemies.Add (this.gameObject);
		objSpawn = GameObject.FindWithTag ("Spawner"); //find Spawner

	}
	
	// Update is called once per frame
	void Update () {

		Move();

		if (transform.position.z < -10) {
			Destroy (gameObject);
		}

	}

	public bool TakeDamage(){
		bool dead = false;
		hp -= 1;
		if (hp > -1) {
			spriteRenderer.sprite = hpSprites [hp];
		}
		if (hp == 0) {
			if (hasWeapon) {
				DropWeapon ();
			}
		}

		if (hp == -1) {
			dead = true;
		}
		return dead;
	}



	void Move(){

		//add "desired direction" which by default is down but can be overridden to enable evasive types
		bool canMove = false;
		if (targetDir == Vector3.back) {
			canMove = !CheckDown ();
		}
		else if (targetDir == Vector3.left) {
			canMove = !CheckLeft ();
		}
		else if (targetDir == Vector3.right) {
			canMove = !CheckRight ();
		}

		if (canMove) {
			direction = targetDir;
		} 
		else if (targetDir == Vector3.left || targetDir == Vector3.right) {
			direction = Vector3.back;
		} else if (targetDir == Vector3.back) {
			if (obstacle.position.x < transform.position.x) {
				direction = new Vector3 (1, 0, 0);
				if (CheckRight ()) {
					direction = new Vector3 (-1, 0, 0);
				}
			} else {
				direction = new Vector3 (-1, 0, 0);
				if (CheckLeft ()) {
					direction = new Vector3 (1, 0, 0);
				}
			}
		}

		transform.position = transform.position + (direction * speed);

	}
		


	bool CheckDown(){
		bool obstacle = false;

		bool left = SendRaycast (new Vector3 (transform.position.x -0.5f, 0.1f, transform.position.z), new Vector3(0,0,-1),  1.5f);
		bool right = SendRaycast (new Vector3 (transform.position.x + 0.5f, 0.1f, transform.position.z),new Vector3(0,0,-1), 1.5f);
		if (left || right) {
			obstacle = true;
		}
		return obstacle;

	}

	bool CheckRight(){
		bool obstacle = false;

		if (transform.position.x > 6) {
			obstacle = true;
		}

		bool up = SendRaycast (new Vector3 (transform.position.x, 0.1f, transform.position.z -0.5f), new Vector3(1,0,0),  1.5f);
		bool down = SendRaycast (new Vector3 (transform.position.x, 0.1f, transform.position.z + 0.5f),new Vector3(1,0,0), 1.5f);
		if (up || down) {
			obstacle = true;
		}
		return obstacle;

	}

	bool CheckLeft(){
		bool obstacle = false;

		if (transform.position.x < -6) {
			obstacle = true;
		}

		bool up = SendRaycast (new Vector3 (transform.position.x, 0.1f, transform.position.z -0.5f), new Vector3(-1,0,0),  1.5f);
		bool down = SendRaycast (new Vector3 (transform.position.x, 0.1f, transform.position.z + 0.5f),new Vector3(-1,0,0), 1.5f);
		if (up || down) {
			obstacle = true;
		}
		return obstacle;

	}
		

	bool SendRaycast(Vector3 position, Vector3 dir,float length){
		RaycastHit hit;
		bool result = false;
		if (Physics.Raycast (position, dir, out hit, length)) {

			if (Vector3.Distance (transform.position, hit.collider.transform.position) < (length + 0.5f)) {

				if (hit.collider.gameObject.CompareTag ("Obstacle")) {

					obstacle = hit.collider.transform;
					result = true;

				}
			}
		}

		return result;
	}



	public void CreateCorpse(bool combo){
		GameObject newCorpse;
		if (!combo) {
			newCorpse = Instantiate (corpse, transform.position, Quaternion.identity);
		} else {
			newCorpse = Instantiate (specialCorpse, transform.position, Quaternion.identity);
		}
		newCorpse.transform.parent = GameObject.Find("CorpseParent").transform;
		Destroy (gameObject);
	}


	void OnDestroy(){
		RemoveMe ();
		gameManager.enemies.Remove (gameObject);
	}

	void DropWeapon(){
		GameObject newWeapon;
		newWeapon = Instantiate (weaponDrop, transform.position, Quaternion.identity);
		hasWeapon = false;
		if (weaponCollider != null) {
			weaponCollider.SetActive(false);
		}

	}


	// Call this when you want to kill the enemy
	void RemoveMe ()
	{
		objSpawn.BroadcastMessage("KillEnemy", SpawnerID);
	}
	// this gets called in the beginning when it is created by the spawner script
	void SetName(int sName)
	{
		SpawnerID = sName;
	}


}
