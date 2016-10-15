using UnityEngine;
using System.Collections;

public class BalloonSpawner : MonoBehaviour {

	public GameObject balloon;

	private GameObject spawnedBalloon;

	public void SpawnBalloon() {
		GameObject b = Instantiate(balloon) as GameObject;
		b.transform.position = transform.position + new Vector3(0, 1, 0);

		if (spawnedBalloon != null) {
			Destroy(spawnedBalloon);
		}

		spawnedBalloon = b;
	}
}
