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
	private bool off = true;
	private float timer = 0.1f;

	public AudioClip sfxDown;
	public AudioClip sfxUp;

	void Update() {

		if (heldDown) {
			timer = 0.1f;
			off = false;
		} else {
			timer -= Time.deltaTime;
			if (timer <= 0) {
				if (!off) SwitchOff();
				off = true;
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

		AudioSource audio = GetComponent<AudioSource>();
		audio.clip = sfxDown;
		//audio.Play();
		
	}

	void SwitchOff() {
		gameObject.GetComponent<MeshRenderer>().material = offMaterial;
        foreach (GameObject obj in targetObject)
            obj.SendMessage(offFunction, this);

		targetY = 0.072f;

		AudioSource audio = GetComponent<AudioSource>();
		audio.clip = sfxUp;
		audio.Play();

	}

}
