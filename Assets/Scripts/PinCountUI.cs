using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PinCountUI : MonoBehaviour {

	private Text text;
	private PinSetter setter;
	
	void Start () {
		text = GetComponent<Text>();
		setter = GameObject.FindObjectOfType<PinSetter>();
	}

	void Update () {
		if (setter.ballEnteredBox) {
			text.color = Color.red;
			text.text = setter.CountStanding().ToString();
		}
	}
}
