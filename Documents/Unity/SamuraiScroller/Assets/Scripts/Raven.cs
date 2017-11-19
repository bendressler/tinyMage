using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raven : MonoBehaviour {

	Vector3 target;
	float speed;

	// Use this for initialization
	void Start () {
		speed = 0.05f;
		float direction = Random.Range(0f,1f);
		if (direction <= 0.5f) {
			target = new Vector3 (-10, 0, transform.position.z + Random.Range (-10, 10));
			transform.localScale = new Vector3 (-1, 1, 1);
		}
		else{
			target = new Vector3 (10, 0, transform.position.z + Random.Range (-10, 10));
		}
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Vector3.MoveTowards (transform.position, target, speed);
		if (transform.position.x < -7 || transform.position.x > 7) {
			Destroy (gameObject);
		}
	}
}
