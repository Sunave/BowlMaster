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
		text.text = setter.CountStanding().ToString();
		if (setter.ballEnteredBox) text.color = Color.red;
		if (setter.pinsHaveSettled) text.color = Color.green;
	}
}
