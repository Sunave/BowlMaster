using UnityEngine;
using System.Collections;

public class PinSetter : MonoBehaviour {

	public float distanceToRaise = 40f;
	public GameObject pinSet;

	public bool ballOutOfPlay {get; set;}
	public bool pinsHaveSettled {get; set;}

	private int lastStandingCount = -1;
	private float lastChangeTime;
	private int pinsLeft = 10;

	private Ball ball;
	private Animator animator;
	private ActionMaster actionMaster;

	void Start () {
		ball = GameObject.FindObjectOfType<Ball>();
		animator = GetComponent<Animator>();
		actionMaster = new ActionMaster();

		ballOutOfPlay = false;
		pinsHaveSettled = false;
	}

	void Update () {
		if (ballOutOfPlay) UpdateStandingPinCountAndSettle();
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

	public int CountScore() {
		int standing = CountStanding();
		int score = pinsLeft - standing;
		pinsLeft = standing;
		return score;
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
			HandleBowl();
		}
	}

	void SettlePins () {
		ball.Reset();
		pinsHaveSettled = true;
		ballOutOfPlay = false;
		lastStandingCount = -1;
	}

	void HandleBowl () {
		ActionMaster.NextAction action = actionMaster.Bowl(CountScore());
		if (action == ActionMaster.NextAction.Tidy) {
			animator.SetTrigger ("tidyTrigger");
		} else if (action == ActionMaster.NextAction.EndTurn || action == ActionMaster.NextAction.Reset) {
			animator.SetTrigger ("resetTrigger");
		} else if (action == ActionMaster.NextAction.EndGame) { 
			throw new UnityException ("Don't know how to handle end game, please implement this feature.");
		}
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
		pinsLeft = 10;
		GameObject newPins = Instantiate (pinSet);
		newPins.transform.Translate (Vector3.up * 20);
	}
	
}
