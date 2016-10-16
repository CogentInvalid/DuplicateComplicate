using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour {

	GameObject player;

	float timer = 5;

	public AudioClip sfx;

	// Use this for initialization
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Player") {
			if (player == null) {
				//play sound
				GetComponent<AudioSource>().clip = sfx;
				GetComponent<AudioSource>().Play();
			}

			player = other.gameObject;
			player.GetComponent<Rigidbody>().useGravity = false;
			player.GetComponent<CharacterController>().enabled = false;

			GameObject.Find("Canvas").GetComponent<CanvasScript>().fadeIn = true;
		}
	}

	void Update() {
		if (player != null) {
			Vector3 pos = player.transform.position;
			float y = pos.y + 3*Time.deltaTime;
			Vector3 targetPos = new Vector3(transform.position.x, y, transform.position.z);
			pos -= (pos-targetPos)*5*Time.deltaTime;
			pos.y = y;
			player.transform.position = pos;

			timer -= Time.deltaTime;
			if (timer <= 0) {
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
			}
		}
	}
}
