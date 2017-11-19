﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corpse : MonoBehaviour {

	public float speed;


	// Use this for initialization
	void Start () {
		speed = 0.01f;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3 (transform.position.x, 0, transform.position.z - speed);

		if (transform.position.z < -10) {
			Destroy(gameObject);
		}
	}
}
