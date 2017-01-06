using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatGenerator : MonoBehaviour {
   public int i;

	// Use this for initialization
	void Start () {
        i = 0;
	}
	
	// Update is called once per frame
	void Update () {
        i++;
        if (i > 255)
            i = 0;
	}

    int getBeat()
    {
        return i;
    }
}
