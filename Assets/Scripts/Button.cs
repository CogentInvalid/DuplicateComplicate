using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {

	public Material offMaterial;
	public Material onMaterial;

	public GameObject targetObject;
	public string onFunction;
	public string offFunction;
	public int buttonIndex;

	void Update() {

	}

	void OnTriggerStay(Collider other) {
		if (other.GetComponent<Box>() != null) {
			if (!other.GetComponent<Carryable>().held) SwitchOn();
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.GetComponent<Box>() != null) {
			if (!other.GetComponent<Carryable>().held) SwitchOff();
		}
	}

	void SwitchOn() {
		gameObject.GetComponent<MeshRenderer>().material = onMaterial;
		targetObject.SendMessage(onFunction, this);
	}

	void SwitchOff() {
		gameObject.GetComponent<MeshRenderer>().material = offMaterial;
		targetObject.SendMessage(offFunction, this);
	}

}
