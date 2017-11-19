using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboScript : MonoBehaviour {


	public float decaySpeed;
	public float minValue;




	float Decay(float currentValue){
		float result = currentValue -= decaySpeed;
		if (result < minValue) {
			result = minValue;
		}
		return result;
	}



}
