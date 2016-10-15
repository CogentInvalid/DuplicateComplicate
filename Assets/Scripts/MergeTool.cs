using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class MergeTool : MonoBehaviour {

	public GameObject box;

	GameObject heldObject;

	public float grabRange = 2;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		//picking up/dropping object
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

		//moving held object
		if (heldObject != null) {
			Rigidbody rigid = heldObject.GetComponent<Rigidbody>();
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			Vector3 targetPos = ray.GetPoint(2);
			rigid.velocity = -(heldObject.transform.position-targetPos)*10;
		}

		//duplicating object
		if (Input.GetMouseButtonDown(0) && heldObject == null) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit, grabRange)) {
				GameObject obj = hit.collider.gameObject;
				Carryable carryable = obj.GetComponent<Carryable>();
				if (carryable != null) {
					CloneObject(obj);
				}
			}
		}

	}

	void GrabObject(GameObject obj) {
		Carryable carryable = obj.GetComponent<Carryable>();
		carryable.PickUp();
		heldObject = obj;
		GetComponent<FirstPersonController>().canJump = false;

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		obj.transform.position = ray.GetPoint(grabRange);
	}

	void ReleaseObject(GameObject obj) {
		Carryable carryable = obj.GetComponent<Carryable>();
		carryable.PutDown();
		heldObject = null;
		GetComponent<FirstPersonController>().canJump = true;
	}

	void CloneObject(GameObject obj) {
		if (obj.GetComponent<Box>() != null) {
			GameObject clone = Instantiate(box);
			GrabObject(clone);
		}
	}
}
