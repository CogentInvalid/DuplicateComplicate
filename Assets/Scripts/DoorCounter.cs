using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DoorCounter : MonoBehaviour {

	public GameObject door;

	private Text text;
	private Door doorScript;

	// Use this for initialization
	void Start () {
		text = GetComponent<Text>();
		doorScript = door.GetComponent<Door>();
	}
	
	// Update is called once per frame
	void Update () {
		text.text = doorScript.currentInputs + "/" + doorScript.requiredInputs;
		if (doorScript.currentInputs >= doorScript.requiredInputs) text.text = "";
	}
}
