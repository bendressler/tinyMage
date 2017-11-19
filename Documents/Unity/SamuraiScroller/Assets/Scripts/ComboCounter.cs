using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboCounter : MonoBehaviour {

	//public bool boosted;

	PlayerCont player;
	SwipeController swipeCont;

	public float timer;

	public int killStacks;
	float lastKill;
	public float comboThresh;
	public int killsForCombo;

	void Start(){
		player = GetComponent<PlayerCont> ();
		swipeCont = GameObject.Find("GameManager").GetComponent<SwipeController> ();

	}

	void Update(){
		
	}



	public void RegisterKill(float time, GameObject enemy){

		if (swipeCont.dashing) {
			killStacks++;
			enemy.GetComponent<Enemy>().CreateCorpse (true);
		} else {
			killStacks = 0;
			enemy.GetComponent<Enemy>().CreateCorpse (false);
		}

		if (killStacks > 2) {
			player.GetComponent<PlayerCont> ().Boost (killStacks);
		}
	}


		/*
		 * if (time - lastKill <= comboThresh) {
			killStacks++;
		} else {
			killStacks = 0;
		}
		if (killStacks >= killsForCombo) {
			enemy.GetComponent<Enemy>().CreateCorpse (true);
			player.GetComponent<PlayerCont> ().Boost ();

		} else {
			enemy.GetComponent<Enemy>().CreateCorpse (false);
		}
		lastKill = time;

	}
*/
		

}
