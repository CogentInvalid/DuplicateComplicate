using UnityEngine;
using System.Collections;

public class ItemSpawner : MonoBehaviour {

	public GameObject spawnItem;

	private GameObject spawnedItem;

	public void SpawnItem() {
		GameObject b = Instantiate(spawnItem) as GameObject;
		b.transform.position = transform.position + new Vector3(0, 1, 0);

		if (spawnedItem != null) {
			Destroy(spawnedItem);
		}

		spawnedItem = b;
	}
}
