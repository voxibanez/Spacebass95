using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatTick : MonoBehaviour {
	GameObject[] enemies;
	GameObject[] players;
	GameObject[] backgrounds;
	private int frameCount;
	// Use this for initialization
	void Start () {
		frameCount = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (frameCount == 57) {
			backgrounds =  GameObject.FindGameObjectsWithTag("Background");
		}
		if (frameCount == 58) {
			players =  GameObject.FindGameObjectsWithTag("Player");
		}
		if (frameCount == 59) {
			enemies =  GameObject.FindGameObjectsWithTag("Enemy");
		}
		if (frameCount == 60) {
			tick ();
			frameCount = 0;
		}
		frameCount++;
	}

	void tick(){
		foreach (GameObject obj in backgrounds) {
			obj.GetComponent<tickScriptBg>().tick();
		}	
	}
}
