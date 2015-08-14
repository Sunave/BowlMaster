using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using System.Linq;

[TestFixture]

public class ActionMasterTest {
	private List<int> pinFalls;
	private ActionMaster.ActionState endTurn = ActionMaster.ActionState.EndTurn;
	private ActionMaster.ActionState tidy = ActionMaster.ActionState.Tidy;
	private ActionMaster.ActionState reset = ActionMaster.ActionState.Reset;
	private ActionMaster.ActionState endGame = ActionMaster.ActionState.EndGame;

	[SetUp]
	// Here anything that should be executed before every test
	public void Setup () {
		pinFalls = new List<int> ();
	}

	[Test]
	public void T00PassingTest () {
		Assert.AreEqual (1, 1);
	}


	[Test]
	public void T01StrikeOnFirstReturnsEndOfTurn () {
		pinFalls.Add (10);
		Assert.AreEqual (endTurn, ActionMaster.NextAction (pinFalls));
	}

	[Test]
	public void T02Bowl8ReturnsTidy () {
		pinFalls.Add (8);
		Assert.AreEqual (tidy, ActionMaster.NextAction (pinFalls));
	}

	[Test]
	public void T03TwoBowlSpareReturnsEndTurn () {
		int[] rolls = {8, 2};
		Assert.AreEqual (endTurn, ActionMaster.NextAction (rolls.ToList()));
	}

	[Test]
	public void T05After21BowlsReturnsEndGame () {
		Assert.AreEqual (endGame, ActionMaster.NextAction(RollMany(2, 21)));
	}

	[Test]
	public void T06ResetsAtStrikeInLastFrame () {
		pinFalls = RollMany(1, 18);
		pinFalls.Add (10);
		Assert.AreEqual (reset, ActionMaster.NextAction(pinFalls));
	}

	[Test]
	public void T07ResetsAtSpareInLastFrame () {
		pinFalls = RollMany(1, 19);
		pinFalls.Add (9);
		Assert.AreEqual (reset, ActionMaster.NextAction(pinFalls));
	}

	[Test]
	public void T08YoutubeReferenceRollsEndGame () {
		// Tutorial rolls from https://www.youtube.com/watch?v=aBe71sD8o8c
		int[] rolls = {8,2, 7,3, 3,4, 10, 2,8, 10, 10, 8,0, 10, 8,2,9};
		Assert.AreEqual (endGame, ActionMaster.NextAction(rolls.ToList()));
	}

	[Test]
	public void T09GameEndsAtBowl20 () {
		pinFalls = RollMany(1, 20);
		Assert.AreEqual (endGame, ActionMaster.NextAction(pinFalls));
	}

	[Test]
	public void T10Bowl20IsTidyAfterStrike () {
		pinFalls = RollMany(1, 18);
		pinFalls.Add (10);
		pinFalls.Add (5);
		Assert.AreEqual (tidy, ActionMaster.NextAction(pinFalls));
	}

	[Test]
	public void T11Bowl20IsTidyEvenIfZeroAfterStrike () {
		pinFalls = RollMany(1, 18);
		pinFalls.Add (10);
		pinFalls.Add (0);
		Assert.AreEqual (tidy, ActionMaster.NextAction(pinFalls));
	}

	[Test]
	public void T12BowlZeroTenFiveReturnsTidy () {
		int[] rolls = {0, 10, 5};
		Assert.AreEqual(tidy, ActionMaster.NextAction(rolls.ToList()));
	}
	
	[Test]
	public void T13BowlZeroTenFiveOneReturnsEndTurn () {
		int[] rolls = {0, 10, 5, 1};
		Assert.AreEqual(endTurn, ActionMaster.NextAction(rolls.ToList()));
	}

	[Test]
	public void T14ThreeStrikesAtLastFramesEndsGame () {
		pinFalls = RollMany(1, 18);
		pinFalls.Add (10);
		pinFalls.Add (10);
		pinFalls.Add (10);
		Assert.AreEqual (endGame, ActionMaster.NextAction(pinFalls));
	}



	private List<int> RollMany(int pinAmount, int amountOfRolls) {
		List<int> rolls = new List<int>();
		for (int i = 0; i < amountOfRolls; i++) {
			rolls.Add(pinAmount);
		}
		return rolls;
	}
}