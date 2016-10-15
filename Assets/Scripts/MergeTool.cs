using UnityEngine;
using System.Collections;

public class MergeTool : MonoBehaviour {

	GameObject heldObject;

	public float grabRange = 2;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.E)) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit, grabRange)) {
				GameObject obj = hit.collider.gameObject;
				Carryable carryable = obj.GetComponent<Carryable>();
				if (carryable != null) {
					carryable.SetTransparent();
				}
			}

		}
	}
}
