using UnityEngine;
using System.Collections;

public class Carryable : MonoBehaviour {

	public bool held = false;

	new Rigidbody rigidbody;

	public enum Identity {
		Box, Balloon, BalloonBox
	}

	public Identity identity;

	void Awake() {
		rigidbody = GetComponent<Rigidbody>();
	}
	
	void Start () {
		//this is bad code. don't try this at home
		if (GetComponent<Box>() != null) {
			identity = Identity.Box;
		}
		if (GetComponent<Balloon>() != null) {
			identity = Identity.Balloon;
		}
		if (GetComponent<Box>() != null && GetComponent<Balloon>() != null) {
			identity = Identity.BalloonBox;
		}
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void PickUp() {
		held = true;
		SetTransparent();
		gameObject.layer = 9;
		rigidbody.angularDrag = 5;
	}

	public void PutDown() {
		held = false;
		SetOpaque();
		gameObject.layer = 8;
		rigidbody.angularDrag = 0;

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
