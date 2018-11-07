using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    //Properties
    public float moveSpeed = 50.0f;
    private CharacterController characterController;
    public Rigidbody head;

	// Use this for initialization
	void Start () {
        characterController = GetComponent<CharacterController>(); //init and get a refereence to current component CharacterController attached to the GameObject
    }
	
	// Update is called once per frame
	void Update () {

        //Create a new Vector3 and store the movement direction, then call the simpleMove method which takes the direction and speed and does the rest
        // and also stops the character from going through walls/ obstecals
        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        characterController.SimpleMove(moveDirection * moveSpeed);
		
	}

    void FixedUpdate() {
        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")); //cal movement direction

        // Moving the marine head when the marine actually moves
        if (moveDirection == Vector3.zero)
        {
            // TODO
            //marine standing still
        }
        else
        {
            head.AddForce(transform.right * 150, ForceMode.Acceleration);
            //you apply force in a paticular direction which is multipled by a force amount

        }
    }
}
