using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeScript : MonoBehaviour {

	public float speed;
	public GameObject raven;
	public Sprite[] sprites;
	public SpriteRenderer spriteR;

	WeatherManager weather;
	GameObject gameManager;


	// Use this for initialization
	void Start () {
		gameManager = GameObject.Find ("GameManager");
		speed = 0.01f;
		transform.position = new Vector3 (Random.Range(-4.5f,4.5f), 0, transform.position.z);
		weather = GameObject.Find ("WeatherEmitter").GetComponent<WeatherManager> ();
		SetSprite ();
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3 (transform.position.x, 0, transform.position.z - speed);

		if (transform.position.z < -10) {
			transform.position = new Vector3 (Random.Range(-4.5f,4.5f), 0, 10);
			SetSprite ();
		}
	}

	void SpawnRaven(){
		GameObject newRaven = Instantiate (raven, transform.position, Quaternion.identity);
	}

	void SetSprite(){
		if (weather.currentSeason == WeatherManager.Seasons.Spring) {
			spriteR.sprite = sprites [0];
		}
		if (weather.currentSeason == WeatherManager.Seasons.Summer) {
			spriteR.sprite = sprites [1];
		}
		if (weather.currentSeason == WeatherManager.Seasons.Autumn) {
			spriteR.sprite = sprites [2];
		}
		if (weather.currentSeason == WeatherManager.Seasons.Winter) {
			spriteR.sprite = sprites [3];
		}
	}

	void Collision(GameObject collider){
		if(collider.CompareTag("Player")){
			if (gameManager.GetComponent<SwipeController>().dashing) {
				SpawnRaven ();
			}
		}
	}
}
