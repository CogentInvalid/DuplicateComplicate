using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CanvasScript : MonoBehaviour {

	public GameObject white;
	public bool fadeIn = false;

	// Use this for initialization
	void Start () {
		white.active = true;
	}
	
	// Update is called once per frame
	void Update () {
		Color color = white.GetComponent<Image>().color;
		if (fadeIn) {
			color.a += Time.deltaTime*0.2f;
		} else {
			color.a -= Time.deltaTime;
			if (color.a < 0) color.a = 0;
		}
		white.GetComponent<Image>().color = color;
	}
}
