using UnityEngine;
using System.Collections;

public class Shredder : MonoBehaviour {

	void OnTriggerExit (Collider collider) {
		if (collider.gameObject.GetComponentInParent<Pin>()) {
			Destroy (collider.transform.parent.gameObject);
		}
	}
}
