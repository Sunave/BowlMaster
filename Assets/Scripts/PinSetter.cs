using UnityEngine;
using System.Collections;

public class PinSetter : MonoBehaviour {

	public int lastStandingCount = -1;
	public bool ballEnteredBox = false;
	public bool pinsHaveSettled = false;

	private float lastChangeTime;
	private Ball ball;

	void Start () {
		ball = GameObject.FindObjectOfType<Ball>();
	}

	void Update () {
		if (ballEnteredBox) CheckStandingCount();
	}

	public int CountStanding() {
		int count = 0;
		foreach (Pin pin in GameObject.FindObjectsOfType<Pin>()) {
			if (pin.IsStanding()) count++;
		}
		return count;
	}

	void CheckStandingCount () {
		int currentStanding = CountStanding();
		if (currentStanding != lastStandingCount) {
			lastChangeTime = Time.time;
			lastStandingCount = currentStanding;
			return;
		}

		float settleTime = 3f; // How long to wait to consider pins settled

		if ((Time.time - lastChangeTime) > settleTime) {
			SettlePins();
		}
	}

	void SettlePins () {
		ball.Reset();
		pinsHaveSettled = true;
		ballEnteredBox = false;
		lastStandingCount = -1;
	}

	void OnTriggerEnter (Collider collider) {
		if (collider.gameObject.GetComponent<Ball>()) ballEnteredBox = true;
	}

	void OnTriggerExit (Collider collider) {
		if (collider.gameObject.GetComponentInParent<Pin>()) {
			Destroy (collider.transform.parent.gameObject);
		}
	}
}
