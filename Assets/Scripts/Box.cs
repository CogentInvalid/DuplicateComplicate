using UnityEngine;
using System.Collections;

public class Box : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody>().sleepThreshold = 0;
	}

}
