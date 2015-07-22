using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

	public bool isMoving;
	private Rigidbody rigidBody;
	private AudioSource audioSource;

	void Start () {
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
}
