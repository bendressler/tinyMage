using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveGenerator : MonoBehaviour {

	[System.Serializable] 
	public class EnemySpawn{
		public GameObject enemyType;
		public float xPos;
		public float zPos;
	}
		
	public float difficulty;
	public float minDiff;
	public EnemySpawn[] toSpawn;
	public float waveHeight;
	public float waveWidth;
	public float speedOverr;

	public void Spawn(float xOffset, float zOffset){
		foreach (EnemySpawn e in toSpawn) {
			GameObject newSpawn = Instantiate (e.enemyType, new Vector3 (e.xPos + xOffset, 0, e.zPos + 10 + zOffset), Quaternion.identity);
			newSpawn.transform.parent = GameObject.Find("EnemyParent").transform;
			if (speedOverr > 0) {
				newSpawn.GetComponent<Enemy> ().speed = speedOverr;
			}
		}
	}

	void Start(){
		Spawn (0,0);
	}

		
}
