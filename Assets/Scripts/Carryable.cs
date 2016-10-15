using UnityEngine;
using System.Collections;

public class Carryable : MonoBehaviour {

	public bool held = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void PickUp() {
		held = true;
		SetTransparent();
		gameObject.layer = 9;
	}

	public void PutDown() {
		held = false;
		SetOpaque();
		gameObject.layer = 8;

		Rigidbody rigidbody = GetComponent<Rigidbody>();
		float maxReleaseSpeed = 5;
		if (rigidbody.velocity.magnitude > maxReleaseSpeed) {
			rigidbody.velocity = rigidbody.velocity.normalized*maxReleaseSpeed;
		}
	}

	void SetTransparent() {
		Color color = GetComponent<MeshRenderer>().material.color;
		color.a = 0.2f;
		GetComponent<MeshRenderer>().material.color = color;
	}

	void SetOpaque() {
		Color color = GetComponent<MeshRenderer>().material.color;
		color.a = 1f;
		GetComponent<MeshRenderer>().material.color = color;
	}
}
