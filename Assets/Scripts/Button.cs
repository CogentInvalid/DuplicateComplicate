using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {

	public GameObject buttonModel;
	private float targetY = 0.014f;

	public Material offMaterial;
	public Material onMaterial;

	public GameObject[] targetObject;
	public string onFunction;
	public string offFunction;
	public int buttonIndex;

	private bool heldDown = false;
	private float timer = 0.1f;

	void Update() {

		if (heldDown) {
			timer = 0.1f;
		} else {
			timer -= Time.deltaTime;
			if (timer <= 0) {
				SwitchOff();
			}
		}

		heldDown = false;

		Vector3 pos = buttonModel.transform.localPosition;
		pos.y -= (pos.y-targetY)*8*Time.deltaTime;
		buttonModel.transform.localPosition = pos;
	}

	void OnTriggerStay(Collider other) {
		if (other.GetComponent<Box>() != null) {
			if (!heldDown) {
				SwitchOn();
				if (!other.GetComponent<Carryable>().held) heldDown = true;
			}
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.GetComponent<Box>() != null) {
			if (!other.GetComponent<Carryable>().held) heldDown = false;
		}
	}

	void SwitchOn() {
		gameObject.GetComponent<MeshRenderer>().material = onMaterial;
        foreach(GameObject obj in targetObject)
            obj.SendMessage(onFunction, this);

		targetY = -0.01f;
		
	}

	void SwitchOff() {
		gameObject.GetComponent<MeshRenderer>().material = offMaterial;
        foreach (GameObject obj in targetObject)
            obj.SendMessage(offFunction, this);

		targetY = 0.072f;

	}

}
