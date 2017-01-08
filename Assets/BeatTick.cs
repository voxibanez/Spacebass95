using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatTick : MonoBehaviour {
	GameObject[] enemies;
	GameObject[] players;
	GameObject[] backgrounds;
	private int frameCount;
    public int BPM;
    private float time;
    private float targetTime;
    public float offset;
	// Use this for initialization
	void Start () {
		frameCount = 0;
        time = offset;
	}
	
	// Update is called once per frame
	void Update () {
        targetTime = 1f/(BPM / 60f);
        time += Time.deltaTime;

        if (time >= targetTime)
        {
            time = 0f;
            tick();
        }
            
        
		if (frameCount % 57 == 0) {
			backgrounds =  GameObject.FindGameObjectsWithTag("Background");
		}
		if (frameCount % 58 == 0) {
			players =  GameObject.FindGameObjectsWithTag("Player");
		}
		if (frameCount % 59 == 0) {
			enemies =  GameObject.FindGameObjectsWithTag("Enemy");
		}
		if (frameCount == 60) {
			frameCount = 0;
		}
		frameCount++;
	}

	void tick(){
		foreach (GameObject obj in backgrounds) {
			obj.GetComponent<tickScriptBg>().tick();
		}
        GameObject.Find("BattleHandler").GetComponent<Battle>().tick();
	}
}
