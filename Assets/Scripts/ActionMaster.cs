﻿using UnityEngine;
using System.Collections;

public class ActionMaster {

	public enum NextAction {Tidy, Reset, EndTurn, EndGame};

	private int[] bowls = new int[21];
	private int bowl = 1;

	public NextAction Bowl (int pins) {
		if (pins < 0 || pins > 10) throw new UnityException ("Invalid pin count.");

		bowls [bowl - 1] = pins;

		if (bowl == 21) return NextAction.EndGame;

		// Last frame special cases
		if (bowl >= 19 && Bowl21Awarded()) {
			bowl++;
			if (bowls[19-1] == 10 && pins < 10) return NextAction.Tidy;
			else return NextAction.Reset;
		} else if (bowl == 20 && !Bowl21Awarded()) {
			return NextAction.EndGame;
		}


		// Regular frames
		if (bowl % 2 != 0) { // First bowl of frames
			if (pins == 10) {
				bowl += 2;
				return NextAction.EndTurn;
			} else {
				bowl++;
				return NextAction.Tidy;
			}
		} else if (bowl % 2 == 0) { // Second bowl of frames
			bowl++;
			return NextAction.EndTurn;
		}

		throw new UnityException ("Not sure which action to return");
	}

	private bool Bowl21Awarded () {
		return (bowls[19 - 1] + bowls[20 - 1]) >= 10;
	}
}