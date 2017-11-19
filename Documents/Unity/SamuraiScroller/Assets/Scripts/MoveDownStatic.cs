using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDownStatic : MonoBehaviour {

	public float speed;
	public bool returning;


	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		transform.position = new Vector3 (transform.position.x, 0, transform.position.z - speed);

		if (transform.position.z < -10) {
			if (returning) {
				transform.position = new Vector3 (transform.position.x, 0, 10);
			} else {
				Destroy (gameObject);
			}

		}
	}


}
