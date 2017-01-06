using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    public float speed = 20.0F;
    public float rotateSpeed = 2.0F;
    public float friction = 10.0F;
    private Vector2 moveDirection = Vector2.zero;
    private Vector3 velocity = Vector3.zero;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //If theres a controller
        
            //Feed moveDirection with input.
            transform.Translate(0,(Input.GetAxis("Vertical") * speed * Time.smoothDeltaTime),0);

        moveDirection = transform.TransformDirection(moveDirection);
            transform.Rotate(0, 0, Input.GetAxis("Horizontal") * rotateSpeed * -1);
        //Multiply it by speed.
        moveDirection *= speed;
       // print("X" + Input.GetAxis("Horizontal"));
        //print("Y" + Input.GetAxis("Vertical"));
        //Applying gravity to the controller
        moveDirection.y -= friction * Time.deltaTime;
        //Making the character move
        //transform.position += moveDirection * Time.deltaTime * speed;


    }
}
