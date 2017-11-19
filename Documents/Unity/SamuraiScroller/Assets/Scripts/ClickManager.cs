using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour {

	public GameObject player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


		/*if (Input.GetMouseButtonUp(0)) {
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			if(Physics.Raycast(ray, out hit, 100)){

				if (player.gameObject.name == "Player") {
					player.GetComponent<PlayerCont>().StartMoving(new Vector3 (hit.point.x, 0, hit.point.z));
				}
			}
		}
		*/
	}
}
