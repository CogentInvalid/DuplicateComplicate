using UnityEngine;
using System.Collections;

public class ScaleDoor : MonoBehaviour {

	public int requiredInputs = 1;
	public float moveTime = 1;

	public int currentInputs;

	private bool[] isEnabled;

	private float openTimer;
	private float closedScale = 8;
	private float openScale = 0;

	void Start() {
		isEnabled = new bool[999];
	}

	void Update() {
		int count = 0;
		foreach (bool b in isEnabled) {
			if (b) count++;
		}
		currentInputs = count;

		if (count >= requiredInputs) {
			Vector3 scale = transform.localScale;
			scale.z -= (scale.z-openScale)*8*Time.deltaTime;
			transform.localScale = scale;
		}
		else {
			Vector3 scale = transform.localScale;
			scale.z -= (scale.z-closedScale)*8*Time.deltaTime;
			transform.localScale = scale;
		}


	}

	public void SetInput(int index, bool value) {
		isEnabled[index] = value;
	}

	public void BoxOn(Button button) {
		SetInput(button.buttonIndex, true);
	}

	public void BoxOff(Button button) {
		SetInput(button.buttonIndex, false);
	}
}
