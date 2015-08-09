using UnityEngine;
using System.Collections;

public class PinSetter : MonoBehaviour {

	public bool ballEnteredBox = false;

	void Start () {

	}

	void Update () {

	}

	public int CountStanding() {
		int count = 0;
		foreach (Pin pin in GameObject.FindObjectsOfType<Pin>()) {
			if (pin.IsStanding()) count++;
		}
		return count;
	}

	void OnTriggerEnter (Collider collider) {
		if (collider.gameObject.GetComponent<Ball>()) ballEnteredBox = true;
	}

	void OnTriggerExit (Collider collider) {
		print (collider);
		if (collider.gameObject.GetComponentInParent<Pin>()) {
			Destroy (collider.transform.parent.gameObject);
		}
	}
}
