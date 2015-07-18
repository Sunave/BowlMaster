using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

	public Vector3 velocity;

	private AudioSource audioSource;

	void Start () {
		audioSource = GetComponent<AudioSource>();
		Launch();
	}

	void Update () {
	
	}

	public void Launch () {
		GetComponent<Rigidbody>().velocity = velocity;
		audioSource.Play();
	}
}
