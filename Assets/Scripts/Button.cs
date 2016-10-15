using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {

	public Material offMaterial;
	public Material onMaterial;

	void Update() {

	}

	void OnTriggerStay(Collider other) {
		if (other.GetComponent<Box>() != null) {
			SwitchOn();
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.GetComponent<Box>() != null) {
			SwitchOff();
		}
	}

	void SwitchOn() {
		gameObject.GetComponent<MeshRenderer>().material = onMaterial;
	}

	void SwitchOff() {
		gameObject.GetComponent<MeshRenderer>().material = offMaterial;
	}

}
