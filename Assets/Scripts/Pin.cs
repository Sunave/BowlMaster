using UnityEngine;
using System.Collections;

public class Pin : MonoBehaviour {

	public float standingThreshold = 3;

	public bool IsStanding () {
		return Vector3.Angle(Vector3.up, transform.up) < standingThreshold;
	}
}
