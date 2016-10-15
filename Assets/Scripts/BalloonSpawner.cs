using UnityEngine;
using System.Collections;

public class BalloonSpawner : MonoBehaviour {

	public GameObject balloon;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Q)) {
			SpawnBalloon();
		}
	}

	public void SpawnBalloon() {
		GameObject b = Instantiate(balloon) as GameObject;
		b.transform.position = transform.position + new Vector3(0, 1, 0);
	}
}
