using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.SceneManagement;

public class MergeTool : MonoBehaviour {

	public GameObject box;
	public GameObject balloon;
	public GameObject fire;
	public GameObject balloonBox;
	public GameObject fireBox;

	public int clone_max = 999;
	private int clone_total = 0;

	GameObject heldObject;

	public float grabRange = 2;

	public AudioClip sfxGrab;
	public AudioClip sfxDrop;
	public AudioClip sfxClone;
	public AudioClip sfxMerge;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		//quit game
		if (Input.GetKeyDown(KeyCode.Escape))
			Application.Quit();

		if (Input.GetKeyDown(KeyCode.R))
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

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
			rigid.velocity = -(heldObject.transform.position-targetPos)*20;
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

		GetComponent<AudioSource>().clip = sfxGrab;
		GetComponent<AudioSource>().Play();
	}

	void ReleaseObject(GameObject obj) {
		Carryable carryable = obj.GetComponent<Carryable>();
		carryable.PutDown();
		heldObject = null;
		GetComponent<FirstPersonController>().canJump = true;

		GetComponent<AudioSource>().clip = sfxDrop;
		GetComponent<AudioSource>().Play();
	}

	void CloneObject(GameObject obj) {
		if (obj.GetComponent<Box>() != null && clone_total != clone_max) {
			GameObject clone = Instantiate(box);
			GrabObject(clone);
            clone_total++;

			GetComponent<AudioSource>().clip = sfxClone;
			GetComponent<AudioSource>().Play();
		}
	}

	void MergeObject(GameObject obj) {
		Carryable obj1 = heldObject.GetComponent<Carryable>();
		Carryable obj2 = obj.GetComponent<Carryable>();
		
		//box + balloon = balloonbox
		if (obj1.identity == Carryable.Identity.Box && obj2.identity == Carryable.Identity.Balloon ||
			obj1.identity == Carryable.Identity.Balloon && obj2.identity == Carryable.Identity.Box) {
			SpawnMerged(obj1.gameObject, obj2.gameObject, balloonBox);
		}

		//box + fire = firebox
		if (obj1.identity == Carryable.Identity.Box && obj2.identity == Carryable.Identity.Fire ||
			obj1.identity == Carryable.Identity.Fire && obj2.identity == Carryable.Identity.Box) {
			SpawnMerged(obj1.gameObject, obj2.gameObject, fireBox);
		}

		//TODO:
		//fire + balloon = balloonfire
	}

	void SpawnMerged(GameObject obj1, GameObject obj2, GameObject merge) {
		GameObject merged = Instantiate(merge) as GameObject;
		obj1.GetComponent<Carryable>().OnMerged(merged);
		obj2.GetComponent<Carryable>().OnMerged(merged);
		Destroy(obj1);
		Destroy(obj2);
		GrabObject(merged);
		GetComponent<AudioSource>().clip = sfxMerge;
		GetComponent<AudioSource>().Play();
	}


}
