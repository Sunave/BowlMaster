using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

[TestFixture]

public class ActionMasterTest {
	private ActionMaster.NextAction endTurn = ActionMaster.NextAction.EndTurn;

	[Test]
	public void T00PassingTest () {
		Assert.AreEqual (1, 1);
	}

	[Test]
	public void T01StrikeOnFirstReturnsEndOfTurn () {
		ActionMaster actionMaster = new ActionMaster();
		Assert.AreEqual (endTurn, actionMaster.Bowl(10));
	}
}