using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tickScriptBg : MonoBehaviour {
	public int offset;
	private int count;
	public int speed;
	private Vector3 targetScale;
	public bool isTick = false;
	// Use this for initialization
	void Start () {
		count = 0;
		targetScale = new Vector3 (15, 15, 1);
	}
	
	// Update is called once per frame
	void Update () {
		if (isTick && count > 10)
			isTick = false;
			
		if (count > offset && !isTick) {
			targetScale = new Vector3 (25, 25, 1);
		}
			transform.localScale = Vector3.Lerp (transform.localScale, targetScale, speed * Time.deltaTime);
		count++;
	}
	public void tick(){
		targetScale = new Vector3 (15, 15, 1);
		isTick = true;
		count = 0;
	}
}
