using UnityEngine;
using System.Collections;

public class Carryable : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Q)) {
			SetTransparent();
		}
	}

	public void SetTransparent() {
		Color color = GetComponent<MeshRenderer>().material.color;
		color.a = 0.2f;
		GetComponent<MeshRenderer>().material.color = color;
	}

	public void SetOpaque() {
		Color color = GetComponent<MeshRenderer>().material.color;
		color.a = 1f;
		GetComponent<MeshRenderer>().material.color = color;
	}
}
