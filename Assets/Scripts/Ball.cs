using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

	public bool isMoving;
	private Rigidbody rigidBody;
	private AudioSource audioSource;
	private Vector3 startPosition;

	void Start () {
		startPosition = transform.position;
		isMoving = false;
		rigidBody = GetComponent<Rigidbody>();
		rigidBody.useGravity = false;
		audioSource = GetComponent<AudioSource>();
	}

	public void Launch (Vector3 velocity) {
		if (!isMoving) {
			isMoving = true;
			rigidBody.useGravity = true;
			rigidBody.velocity = velocity;

			audioSource.Play();
		}
	}

	public void Reset () {
		isMoving = false;
		rigidBody.useGravity = false;
		rigidBody.velocity = Vector3.zero;
		rigidBody.angularVelocity = Vector3.zero;
		transform.position = startPosition;
	}
}
