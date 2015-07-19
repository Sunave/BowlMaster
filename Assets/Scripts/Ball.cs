using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

	private Rigidbody rigidBody;
	private AudioSource audioSource;

	void Start () {
		rigidBody = GetComponent<Rigidbody>();
		rigidBody.useGravity = false;
		audioSource = GetComponent<AudioSource>();
	}

	public void Launch (Vector3 velocity) {
		rigidBody.useGravity = true;
		rigidBody.velocity = velocity;

		audioSource.Play();
	}
}
