using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour {

	public GameObject background;
	public Material material;
	public Color startColor;
	public Color endColor;

	public Color[] colors;
	public float[] colorDiffSteps;
	public int currentColor;

	public float minDiff;

	public PlayerCont player;

	public float morphDegree;

	void Update(){
		ColorByTime ();
		Color newColor = Color.Lerp (startColor, endColor, morphDegree);
		background.GetComponent<Renderer> ().material.color = newColor;
	}

	/*
	void ColorByEnergy(){
		morphDegree = (player.energyReg - player.minEnergyReg) / player.maxEnergyReg - player.minEnergyReg;
	}
	*/

	void ColorByDifficulty(){
		WaveManager wave = GameObject.Find ("WaveManager").GetComponent<WaveManager> ();


		if (wave.difficulty < colorDiffSteps[0]) {
			currentColor = 0;
		}
		else if(wave.difficulty < colorDiffSteps[1]) {
			currentColor = 1;
		}
		else if(wave.difficulty < colorDiffSteps[2]) {
			currentColor = 2;
		}


		if (currentColor == 0) {
			startColor = Color.white;
			endColor = colors [0];
			morphDegree = (wave.difficulty - minDiff) / colorDiffSteps[0] - minDiff;
		} else {
			startColor = colors [currentColor - 1];
			endColor = colors [currentColor];
			morphDegree = (colorDiffSteps[currentColor-1] - minDiff / colorDiffSteps[currentColor] - minDiff);
		}


	}

	void ColorByTime(){
		WaveManager wave = GameObject.Find ("WaveManager").GetComponent<WaveManager> ();

		for (int i = 0; i < colors.Length; i++) {
			if (wave.currentTime < colorDiffSteps [i]) {
				currentColor = i;
				break;
			}
		}
			
		startColor = colors [currentColor - 1];
		endColor = colors [currentColor];
		morphDegree = ((wave.currentTime - colorDiffSteps[currentColor-1]) / (colorDiffSteps[currentColor] - colorDiffSteps[currentColor-1]));

	}

	void ColorByScore(){

		for (int i = 0; i < colors.Length; i++) {
			if (GetComponent<GameManager>().score < colorDiffSteps [i]) {
				currentColor = i;
				break;
			}
		}

		startColor = colors [currentColor - 1];
		endColor = colors [currentColor];
		morphDegree = ((GetComponent<GameManager>().score - colorDiffSteps[currentColor-1]) / (colorDiffSteps[currentColor] - colorDiffSteps[currentColor-1]));

	}

}
