using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

	public int requiredInputs = 1;
	public Vector3 openPosition;
	public float moveTime = 1;

	private bool[] isEnabled;

	private float openTimer;
	private Vector3 closedPosition;
	
	void Start () {
		closedPosition = transform.position;
		isEnabled = new bool[requiredInputs];
	}
	
	void Update () {
		bool open = true;
		foreach (bool b in isEnabled){
			if (!b) open = false;
		}

		if (open) {
			//openTimer += Time.deltaTime;
			//if (openTimer > moveTime) openTimer = moveTime;
			transform.position -= (transform.position - openPosition)*8*Time.deltaTime;
		} else {
			//openTimer -= Time.deltaTime;
			//if (openTimer < 0) openTimer = 0;
			transform.position -= (transform.position - closedPosition)*8*Time.deltaTime;
		}
		//transform.position = Vector3.Lerp(closedPosition, openPosition, openTimer/moveTime);

		
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
