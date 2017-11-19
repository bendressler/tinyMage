using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour {

	public GameObject[] waves;

	public float difficulty;
	public float frequency;
	int newWaveCount;

	public float timer;
	public float currentTime;

	void Start(){
		IncrementTimer();
	}

	// Use this for initialization
	void Update () {
		currentTime += Time.deltaTime;
		CheckTimer ();
	}

	//Handle timing & difficulty

	void CheckTimer(){
		if (currentTime > timer) {
			SpawnWaves (SelectSpawn (difficulty));
			IncrementTimer ();
		} 
	}

	void IncrementTimer(){
		float delay = frequency;
		timer += delay;
		difficulty += timer / 50;
	}

	//Handle spawning waves

	void SpawnWaves(List<GameObject> toSpawn){
		float nextX;
		float nextZ;

		for (int i = 0; i < newWaveCount; i++) {
			if (i == 0) {
				nextX = Random.Range (-5 + toSpawn [0].GetComponent<WaveGenerator> ().waveWidth, 5 - toSpawn [0].GetComponent<WaveGenerator> ().waveWidth);
				nextZ = 0;
			} else {
				nextX = Random.Range (-5 + toSpawn [i].GetComponent<WaveGenerator> ().waveWidth, 5 - toSpawn [0].GetComponent<WaveGenerator> ().waveWidth);
				nextZ = toSpawn [i].GetComponent<WaveGenerator> ().waveHeight + Random.Range(0,3);
			}

			toSpawn [i].GetComponent<WaveGenerator> ().Spawn (nextX, nextZ);
		}

		newWaveCount = 0;
	}

	List<GameObject> SelectSpawn(float total){
		List<GameObject> result = new List<GameObject> ();
		newWaveCount = 0;
		while (total > 0) {
			int wave = Random.Range (0, waves.Length);
			float diff = waves [wave].GetComponent<WaveGenerator> ().difficulty;
			float minDiff = waves [wave].GetComponent<WaveGenerator> ().minDiff;
			if (diff <= total) {
				if (minDiff < difficulty) {
					result.Add (waves [wave]);
					newWaveCount += 1;
					total -= diff;
				}
			}
			if (total < 0.5) {
				total = 0;
			}
		}
		return result;
	}

}
