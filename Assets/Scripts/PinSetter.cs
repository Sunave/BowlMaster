using UnityEngine;
using System.Collections;

public class PinSetter : MonoBehaviour {

	public int lastStandingCount = -1;
	public bool ballEnteredBox = false;
	public bool pinsHaveSettled = false;
	public float distanceToRaise = 40f;
	public GameObject pinSet;

	private float lastChangeTime;
	private Ball ball;

	void Start () {
		ball = GameObject.FindObjectOfType<Ball>();
	}

	void Update () {
		if (ballEnteredBox) UpdateStandingPinCountAndSettle();
	}



	// PIN HANDLING AND COUNTING RELATED TO BALL MOVEMENT
	// **************************************************

	public int CountStanding() {
		int count = 0;
		foreach (Pin pin in GameObject.FindObjectsOfType<Pin>()) {
			if (pin.IsStanding()) count++;
		}
		return count;
	}

	void UpdateStandingPinCountAndSettle () {
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



	// PIN MOVEMENT & CONTROL
	// **********************

	void RaisePins () {
		foreach (Pin pin in GameObject.FindObjectsOfType<Pin>()) {
			pin.RaiseIfStanding(distanceToRaise);
		}
	}

	void LowerPins () {
		foreach (Pin pin in GameObject.FindObjectsOfType<Pin>()) {
			pin.LowerIfStanding(distanceToRaise);
		}
	}

	void RenewPins () {
		GameObject newPins = Instantiate (pinSet);
		newPins.transform.Translate (Vector3.up * 20);
	}



	// TRIGGER HANDLING
	// ****************
	
	void OnTriggerEnter (Collider collider) {
		if (collider.gameObject.GetComponent<Ball>()) ballEnteredBox = true;
	}
	
}
