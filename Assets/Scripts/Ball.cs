using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

	public float velocity;

	private AudioSource audioSource;

	void Start () {
		audioSource = GetComponent<AudioSource>();
		Launch();
	}

	void Update () {
	
	}

	public void Launch () {
		GetComponent<Rigidbody>().velocity = new Vector3 (0f, 0f, velocity);
		audioSource.Play();
	}
}
