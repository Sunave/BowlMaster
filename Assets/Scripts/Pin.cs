using UnityEngine;
using System.Collections;

public class Pin : MonoBehaviour {

	public float standingThreshold = 3;

	public bool IsStanding () {
		return Vector3.Angle(Vector3.up, transform.up) < standingThreshold;
	}

	public void RaiseIfStanding (float distance) {
		if (IsStanding()) {
			GetComponent<Rigidbody>().useGravity = false;
			transform.Translate (Vector3.up * distance, Space.World);
		}
	}

	public void LowerIfStanding (float distance) {
		if (IsStanding()) {
			GetComponent<Rigidbody>().useGravity = true;
			transform.Translate (Vector3.down * distance, Space.World);
		}
	}
}
