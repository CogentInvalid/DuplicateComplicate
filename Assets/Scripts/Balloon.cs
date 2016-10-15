using UnityEngine;
using System.Collections;

public class Balloon : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Rigidbody rigidbody = GetComponent<Rigidbody>();
		Vector3 vel = rigidbody.velocity;
		vel.y -= (vel.y-1.5f)*Time.deltaTime;
		rigidbody.velocity = vel;
	}
}
