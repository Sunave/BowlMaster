using UnityEngine;
using System.Collections;

public class ActionMaster {

	public enum NextAction {Tidy, Reset, EndTurn, EndGame};

	public NextAction Bowl (int pins) {
		if (pins < 0 || pins > 10) throw new UnityException ("Invalid pin count.");

		if (pins == 10) return NextAction.EndTurn;

		throw new UnityException ("Not sure which action to return");
	}
}
