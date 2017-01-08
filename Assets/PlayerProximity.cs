using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProximity : MonoBehaviour
{
    private int frameCount;
    public int proximity;
    public GameObject playerObject;

    // Use this for initialization
    void Start()
    {
        frameCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        frameCount++;
        if (frameCount > 5)
        {
            foreach (GameObject enObj in GameObject.FindGameObjectsWithTag("Enemy"))
            {

                if (Mathf.Abs((enObj.transform.position.x - playerObject.transform.position.x)) < proximity && Mathf.Abs((enObj.transform.position.y - playerObject.transform.position.y)) < proximity)
                {
                    enObj.GetComponent<FollowObject>().followObject = playerObject;
                    enObj.GetComponent<FollowObject>().enabled = true;
                }
                    

            }
        }
    }
}
