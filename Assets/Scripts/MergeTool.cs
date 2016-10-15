using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class MergeTool : MonoBehaviour {

	public GameObject box;
	public GameObject balloon;
	public GameObject balloonBox;

    public int clone_max = 999;
	private int clone_total = 0;

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
				} else {
					PressButton button = obj.GetComponent<PressButton>();
					if (button != null) {
						button.Press();
					}
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
		//merging object
		else if (Input.GetMouseButtonDown(0) && heldObject != null) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit, grabRange, ~(1 << 9))) {
				GameObject obj = hit.collider.gameObject;
				Carryable carryable = obj.GetComponent<Carryable>();
				if (carryable != null) {
					MergeObject(obj);
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
		if (obj.GetComponent<Box>() != null && clone_total != clone_max) {
			GameObject clone = Instantiate(box);
			GrabObject(clone);
            clone_total++;
		}
	}

	void MergeObject(GameObject obj) {
		Carryable obj1 = heldObject.GetComponent<Carryable>();
		Carryable obj2 = obj.GetComponent<Carryable>();

		Debug.Log(obj1.identity + ", " + obj2.identity);
		if (obj1.identity == Carryable.Identity.Box && obj2.identity == Carryable.Identity.Balloon ||
			obj1.identity == Carryable.Identity.Balloon && obj2.identity == Carryable.Identity.Box) {
			SpawnMerged(obj1.gameObject, obj2.gameObject, balloonBox);
		}
	}

	void SpawnMerged(GameObject obj1, GameObject obj2, GameObject merge) {
		Destroy(obj1);
		Destroy(obj2);
		GameObject merged = Instantiate(merge) as GameObject;
		GrabObject(merged);
	}


}
