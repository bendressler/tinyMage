using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeController : MonoBehaviour {

	PlayerCont player;

	//swiping
	public float velocityThresh;
	Vector3 currentMousePos;
	Vector3 lastMousePos;
	float currentVelocity;
	public bool swiping;
	public Vector3 swipeEndPos;

	//moving
	public bool walking;
	public bool dashing;
	public Vector3 walkGoal;
	public Vector3 dashGoal;


	void Start () {
		player = GameObject.Find ("Player").GetComponent<PlayerCont> ();

		//PC controls
		currentMousePos = GrabCursor();
		lastMousePos = currentMousePos;
		currentVelocity = Vector3.Distance(currentMousePos, lastMousePos);

		walking = true; //start off by walking
	}


	// Update is called once per frame
	void Update () {

		IsSwiping (); //check for whether the player is swiping
		//if no swipe detected, set a target to walk to
		if (!swiping) {
			SetWalkGoal ();
		}
	}

	//if the player has dash stacks available, consume and execute
	void StartDashing(){
		if (player.dashStacks > 0) {
			dashing = true;
			walking = false;
			player.GetComponent<TrailRenderer> ().time = 0.1f; 	//activate trail renderer animation
			player.energy -= player.energySpend;
		}
	}

	public void StopDashing(){
		dashing = false;
		walking = true;
		player.GetComponent<TrailRenderer> ().time = 0; 	//disable trail renderer animation

	}

	void SetWalkGoal(){
		walkGoal = GrabCursor (); //set walk goal to cursor/finger position
	}


	Vector3 GrabCursor(){
		Vector3 result = new Vector3();

		//PC Controls: shoot a ray through the mouse cursor and return the position
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		if (Physics.Raycast (ray, out hit, 100)) {
			result = new Vector3 (hit.point.x, 0, hit.point.z);
		}

		//Touch controls

		return result;
	}


	//if mouse moves faster than the threshold, set swiping to true, 
	//else end it and execute the dash

	void IsSwiping(){

		//PC Controls
		lastMousePos = currentMousePos;
		currentMousePos = GrabCursor ();
		currentVelocity = Vector3.Distance(currentMousePos, lastMousePos);

		if (!swiping && (currentVelocity > velocityThresh)) { //if player has not swiped but is now swiping
			swiping = true;
		}
		if (swiping && (currentVelocity < velocityThresh)) { //if player has swiped but is no longer swiping
			swiping = false;
			swipeEndPos = currentMousePos;
			dashGoal = swipeEndPos;
			StartDashing();
		}

		//Touch controls

	}
}
