using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

[TestFixture]

public class ActionMasterTest {
	private ActionMaster actionMaster;
	private ActionMaster.NextAction endTurn = ActionMaster.NextAction.EndTurn;
	private ActionMaster.NextAction tidy = ActionMaster.NextAction.Tidy;
	private ActionMaster.NextAction reset = ActionMaster.NextAction.Reset;
	private ActionMaster.NextAction endGame = ActionMaster.NextAction.EndGame;

	[SetUp]
	public void SetupActionMaster () {
		actionMaster = new ActionMaster();
	}

	[Test]
	public void T00PassingTest () {
		Assert.AreEqual (1, 1);
	}

	[Test]
	public void T01StrikeOnFirstReturnsEndOfTurn () {
		Assert.AreEqual (endTurn, actionMaster.Bowl(10));
	}

	[Test]
	public void T02Bowl8ReturnsTidy () {
		Assert.AreEqual (tidy, actionMaster.Bowl(8));
	}

	[Test]
	public void T03TwoBowlsReturnEndTurn () {
		actionMaster.Bowl(8);
		Assert.AreEqual (endTurn, actionMaster.Bowl(1));
	}

	[Test]
	public void T04TwoBowlSpareReturnsEndTurn () {
		actionMaster.Bowl (8);
		Assert.AreEqual (endTurn, actionMaster.Bowl(2));
	}

	[Test]
	public void T05After21BowlsReturnsEndGame () {
		RollMany(2, 20);
		Assert.AreEqual (endGame, actionMaster.Bowl(2));
	}

	[Test]
	public void T06ResetsAtStrikeInLastFrame () {
		RollMany(1, 18);
		Assert.AreEqual (reset, actionMaster.Bowl (10));
	}

	[Test]
	public void T07ResetsAtSpareInLastFrame () {
		RollMany(1, 18);
		actionMaster.Bowl (1);
		Assert.AreEqual (reset, actionMaster.Bowl (9));
	}

	[Test]
	public void T08YoutubeReferenceRollsEndGame () {
		// Tutorial rolls from https://www.youtube.com/watch?v=aBe71sD8o8c
		int[] rolls = {8,2, 7,3, 3,4, 10, 2,8, 10, 10, 8,0, 10, 8,2};
		foreach (int roll in rolls) {
			actionMaster.Bowl (roll);
		}
		Assert.AreEqual (endGame, actionMaster.Bowl (9));
	}

	[Test]
	public void T09GameEndsAtBowl20 () {
		RollMany(1, 19);
		Assert.AreEqual (endGame, actionMaster.Bowl (1));
	}

	[Test]
	public void T10Bowl20IsTidyAfterStrike () {
		RollMany(1, 18);
		actionMaster.Bowl(10);
		Assert.AreEqual (tidy, actionMaster.Bowl(5));
	}

	[Test]
	public void T11Bowl20IsTidyEvenIfZeroAfterStrike () {
		RollMany(1, 18);
		actionMaster.Bowl(10);
		Assert.AreEqual (tidy, actionMaster.Bowl(0));
	}



	private void RollMany(int pinAmount, int amountOfRolls) {
		for (int i = 0; i < amountOfRolls; i++) {
			actionMaster.Bowl (pinAmount);
		}
	}
}