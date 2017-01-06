using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour {

    public GameObject followObject;
    public float hardDampTime = 0.5f;
    private float dampTime;
    private Vector3 velocity = Vector3.zero;
    private Transform target;
    // Use this for initialization
    void Start () {
        target = followObject.transform;
        dampTime = hardDampTime;
    }
	
	// Update is called once per frame
	void Update () {
        target = followObject.transform;
        if (target)
        {
            float temp = Mathf.Pow((Mathf.Abs(this.transform.position.x - followObject.transform.position.x) + Mathf.Abs(this.transform.position.y - followObject.transform.position.y)),2f);
            
                dampTime = temp/100 ;
            if (dampTime < .2)
                dampTime = .2f;

            this.transform.position = Vector3.SmoothDamp(transform.position, target.position, ref velocity, dampTime);
        }

    }
}
