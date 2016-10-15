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
		if (Input.GetKeyDown(KeyCode.E) && heldObject == null) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit, grabRange)) {
				GameObject obj = hit.collider.gameObject;
				Carryable carryable = obj.GetComponent<Carryable>();
				if (carryable != null) {
					GrabObject(obj);
				}
			}
		}
		else if (Input.GetKeyDown(KeyCode.E) && heldObject != null) {
			ReleaseObject(heldObject);
		}


		if (heldObject != null) {
			Rigidbody rigid = heldObject.GetComponent<Rigidbody>();
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			Vector3 targetPos = ray.GetPoint(2);
			rigid.velocity = -(heldObject.transform.position-targetPos)*10;
		}
	}

	void GrabObject(GameObject obj) {
		Carryable carryable = obj.GetComponent<Carryable>();
		carryable.SetTransparent();
		carryable.held = true;

		heldObject = obj;
	}

	void ReleaseObject(GameObject obj) {
		Carryable carryable = obj.GetComponent<Carryable>();
		carryable.SetOpaque();
		carryable.held = false;

		heldObject = null;
	}
}
