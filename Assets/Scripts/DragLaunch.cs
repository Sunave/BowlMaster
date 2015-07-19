using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Ball))]
public class DragLaunch : MonoBehaviour {

	private Ball ball;
	private float dragStartTime;
	private Vector3 dragStartPosition;
	
	void Start () {
		ball = GetComponent<Ball>();
	}

	public void DragStart () {
		// Capture time & position of drag start
		dragStartTime = Time.time;
		dragStartPosition = Input.mousePosition;
	}

	public void DragEnd () {
		// Launch the ball
		Vector3 launchPosition = Input.mousePosition;

		float dragEndTime = Time.time;
		float dragDuration = dragEndTime - dragStartTime;

		float launchSpeedX = (launchPosition.x - dragStartPosition.x) / dragDuration;
		float launchSpeedZ = (launchPosition.y - dragStartPosition.y) / dragDuration;
		Vector3 launchVelocity = new Vector3 (launchSpeedX, 0f, launchSpeedZ);

		ball.Launch(launchVelocity);
	}
	
}
