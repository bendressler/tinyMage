using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour {

	public Vector3 direction;
	public float speed;
	public float timer;
	float time;
	
	// Update is called once per frame
	void Start(){
		time = timer;
	}

	void Update () {
		transform.position += direction * Time.deltaTime * speed;
		if (time > 0) {
			time -= Time.deltaTime;
		} else {
			Destroy (gameObject);
		}
		transform.position = new Vector3 (transform.position.x, 0.1f, transform.position.z);

	}
}
