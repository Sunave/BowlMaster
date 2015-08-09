using UnityEngine;
using System.Collections;

public class PinSetter : MonoBehaviour {

	private Pin[] pins;
	
	void Start () {
		pins = GameObject.FindObjectsOfType<Pin>();
	}

	void Update () {
		print (CountStanding());
	}

	public int CountStanding() {
		int count = 0;
		foreach (Pin pin in pins) {
			if (pin.IsStanding()) count++;
		}
		return count;
	}
}
