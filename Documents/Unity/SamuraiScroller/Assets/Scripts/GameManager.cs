using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public List<GameObject> enemies;
	public Text scoreTxt;
	public int score;

	void Start(){
		enemies = new List<GameObject> ();
	}

	void Update(){
		scoreTxt.text = score.ToString ();
	}
}
