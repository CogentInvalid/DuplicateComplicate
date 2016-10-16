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

	public AudioClip sfx;
	
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
		int count1 = CountInputs();
		SetInput(button.buttonIndex, true);
		int count2 = CountInputs();

		if (count1 < requiredInputs && count2 >= requiredInputs) {
			GetComponent<AudioSource>().clip = sfx;
			GetComponent<AudioSource>().Play(1500);
		}
	}

	public void BoxOff(Button button) {
		int count1 = CountInputs();
		SetInput(button.buttonIndex, false);
		int count2 = CountInputs();

		if (count1 >= requiredInputs && count2 < requiredInputs) {
			GetComponent<AudioSource>().clip = sfx;
			GetComponent<AudioSource>().Play(1500);
		}
	}

	public int CountInputs() {
		int count = 0;
		foreach (bool b in isEnabled) {
			if (b) count++;
		}
		return count;
	}

}
