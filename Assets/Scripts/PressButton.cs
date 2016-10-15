using UnityEngine;
using System.Collections;

public class PressButton : MonoBehaviour {

	public GameObject targetObject;
	public string pressFunction;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Press() {
		targetObject.SendMessage(pressFunction, this);
	}
}
