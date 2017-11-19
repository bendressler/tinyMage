using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingArrows : MonoBehaviour {

	public GameObject arrow;
	GameObject player;
	public float timer;
	public float maxTime;
	public int level;

	// Use this for initialization
	void Start () {
		timer = maxTime;
		player = GameObject.Find ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if ((transform.position.z < 8) && (transform.position.z > -10))  {
			if (GetComponent<Enemy> ().hasWeapon) {
				if (timer > 0) {
					timer -= Time.deltaTime;
				} else {
					Shoot ();
					timer = maxTime;
				}
			}
		}
	}

	void Shoot(){
		GameObject newArrow = Instantiate (arrow, transform.position, Quaternion.identity);
		if (level == 0) {
			newArrow.GetComponent<ArrowScript> ().direction = new Vector3 (0, 0, -1);
		}
		if (level == 1) {
			newArrow.GetComponent<ArrowScript> ().direction = (player.transform.position - this.transform.position).normalized;
			newArrow.transform.LookAt (player.transform.position);
		}
		if (level == 3) {
			newArrow.GetComponent<ArrowScript> ().direction = (player.transform.position - this.transform.position).normalized;
			newArrow.transform.LookAt (player.transform.position);

			newArrow = Instantiate (arrow, transform.position, Quaternion.LookRotation (player.transform.position));
			Vector3 dir = Quaternion.Euler(0,15,0) * (player.transform.position - this.transform.position).normalized;
			newArrow.GetComponent<ArrowScript> ().direction = dir;
			newArrow.transform.rotation = Quaternion.LookRotation(dir);

			newArrow = Instantiate (arrow, transform.position, Quaternion.LookRotation (player.transform.position));
			dir = Quaternion.Euler(0,-15,0) * (player.transform.position - this.transform.position).normalized;
			newArrow.GetComponent<ArrowScript> ().direction = dir;
			newArrow.transform.rotation = Quaternion.LookRotation(dir);
		}
	}

}
