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
		if (setter.ballEnteredBox) StartCounter();
		if (setter.pinsHaveSettled) SettleCounter();
	}

	void StartCounter () {
		text.color = Color.red;
		text.text = setter.CountStanding().ToString();
	}

	void SettleCounter () {
		text.color = Color.green;
	}
}
