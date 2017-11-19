using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherManager : MonoBehaviour {

	GameObject weatherMachine;
	public GameObject[] weatherEmitters;

	public float weatherTime;
	float weatherTimer;
	public float seasonTimer;
	public float seasonDuration;

	enum Weather {AutumnLeaves, Rain, StrongRain, Snow, StrongSnow, SummerLeaves, WinterWeather};
	public enum Seasons {Spring, Summer, Autumn, Winter};

	Weather currentWeather;
	public Seasons currentSeason;

	Weather[] autumnWeather;
	Weather[] summerWeather;
	Weather[] springWeather;
	Weather[] winterWeather;


	void Start(){

		seasonTimer = 0;
		currentSeason = Seasons.Spring;
		currentWeather = Weather.SummerLeaves;
		weatherTimer = weatherTime;
		foreach (GameObject g in weatherEmitters) {
			g.GetComponent<ParticleSystem>().Stop();
		}

		autumnWeather = new Weather[3];
		autumnWeather [0] = Weather.AutumnLeaves;
		autumnWeather [1] = Weather.Rain;
		autumnWeather [2] = Weather.StrongRain;
		winterWeather = new Weather[3];
		winterWeather [0] = Weather.WinterWeather;
		winterWeather [1] = Weather.Snow;
		winterWeather [2] = Weather.StrongSnow;
		summerWeather = new Weather[3];
		summerWeather [0] = Weather.SummerLeaves;
		summerWeather [1] = Weather.Rain;
		summerWeather [2] = Weather.StrongRain;
		springWeather = new Weather[3];
		springWeather [0] = Weather.SummerLeaves;
		springWeather [1] = Weather.Rain;
		springWeather [2] = Weather.StrongRain;

	}

	void Update(){

		if (seasonTimer < seasonDuration) {
			seasonTimer += Time.deltaTime;
		} else {
			seasonTimer = 0;
			if (currentSeason < (Seasons)3) {
				currentSeason += 1;
			}
			else{ 
				currentSeason = (Seasons)0;
			}
			SetWeather (currentSeason);
		}

		if (weatherTimer > 0) {
			weatherTimer -= Time.deltaTime;
		} else {
			SetWeather (currentSeason);
			weatherTimer = Random.Range (weatherTime - (weatherTime / 4), weatherTime + (weatherTime / 4));
		}
	}

	void SetWeather(Seasons newSeason){

		weatherEmitters [(int)currentWeather].GetComponent<ParticleSystem> ().Stop();

		if (newSeason == Seasons.Autumn) {
			currentWeather = RandomWeather (autumnWeather);
		}
		if (newSeason == Seasons.Winter) {
			currentWeather = RandomWeather (winterWeather);
		}
		if (newSeason == Seasons.Spring) {
			currentWeather = RandomWeather (springWeather);
		}
		if (newSeason == Seasons.Summer) {
			currentWeather = RandomWeather (summerWeather);
		}

		weatherEmitters [(int)currentWeather].GetComponent<ParticleSystem> ().Play();


	}

	Weather RandomWeather(Weather[] weatherSelection){
		Weather result = currentWeather;

		float dice = Random.Range (0f, 1f);

		if (dice < 0.5) {
			result = weatherSelection[0];
		} 
		else if (dice < 0.8) {
			result = weatherSelection[1];
		} 
		else {
			result = weatherSelection[2];
		} 
		return result;

	}


}
