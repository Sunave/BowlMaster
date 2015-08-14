using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActionMaster {

	public enum ActionState {Tidy, Reset, EndTurn, EndGame};

	private int[] bowls = new int[21];
	private int bowl = 1;

	public static ActionState NextAction (List<int> pinFalls) {
		ActionMaster actionMaster = new ActionMaster();
		ActionState currentAction = new ActionState();

		foreach (int pinFall in pinFalls) {
			currentAction = actionMaster.Bowl (pinFall);
		}

		return currentAction;
	}

	public ActionState Bowl (int pins) { // TODO make private
		if (pins < 0 || pins > 10) throw new UnityException ("Invalid pin count.");

		bowls [bowl - 1] = pins;

		if (bowl == 21) return ActionState.EndGame;

		// Last frame special cases
		if (bowl >= 19 && Bowl21Awarded()) {
			bowl++;
			if (bowls[19-1] == 10 && pins < 10) return ActionState.Tidy;
			else return ActionState.Reset;
		} else if (bowl == 20 && !Bowl21Awarded()) {
			return ActionState.EndGame;
		}


		// Regular frames
		if (bowl % 2 != 0) { // First bowl of frames
			if (pins == 10) {
				bowl += 2;
				return ActionState.EndTurn;
			} else {
				bowl++;
				return ActionState.Tidy;
			}
		} else if (bowl % 2 == 0) { // Second bowl of frames
			bowl++;
			return ActionState.EndTurn;
		}

		throw new UnityException ("Not sure which action to return");
	}

	private bool Bowl21Awarded () {
		return (bowls[19 - 1] + bowls[20 - 1]) >= 10;
	}
}
