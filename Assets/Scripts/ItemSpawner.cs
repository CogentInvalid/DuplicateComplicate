using UnityEngine;
using System.Collections;

public class ItemSpawner : MonoBehaviour {

	public GameObject spawnItem;

	private GameObject spawnedItem;
	private GameObject trackedItem;

	public void SpawnItem() {
		GameObject b = Instantiate(spawnItem) as GameObject;
		b.transform.position = transform.position + new Vector3(0, 1, 0);

		if (spawnedItem != null) {
			Destroy(spawnedItem);
		} else {
			if (trackedItem != null) {
				GameObject thing = trackedItem.GetComponent<Carryable>().Split();
				Destroy(thing);
			}
		}

		spawnedItem = b;
		spawnedItem.GetComponent<Carryable>().SetItemSpawner(this);
		trackedItem = null;
	}

	public void SetTrackedItem(GameObject item) {
		trackedItem = item;
	}
}
