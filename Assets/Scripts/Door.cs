using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

	public int requiredInputs = 1;
	public Vector3 openPosition;
	public float moveTime = 1;

	public int currentInputs;

	private bool[] isEnabled;

	private float openTimer;
	private Vector3 closedPosition;
	
	void Start () {
		closedPosition = transform.position;
		isEnabled = new bool[999];
	}
	
	void Update () {
		int count = 0;
		foreach (bool b in isEnabled){
			if (b) count++;
		}
		currentInputs = count;

		if (count >= requiredInputs) {
			transform.position -= (transform.position - openPosition)*8*Time.deltaTime;
		} else {
			transform.position -= (transform.position - closedPosition)*8*Time.deltaTime;
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
