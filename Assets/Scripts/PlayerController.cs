using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    //Properties
    public float moveSpeed = 50.0f;
    private CharacterController characterController;

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
             
        /* Manual way of doing momvemnt with RidgeBody using ChracterController component
        Vector3 pos = transform.position; //current position of SpaceMarine in 3D Space (x, y, z)

        // retrives values from the input + 1(right) or - 1(left) based on key press, then multipiles this value with the moveSpeed
        // The Time.deltaTime takes into account the time passed in seconds from each frame, stopping the marine from going super fast
        pos.x += moveSpeed * Input.GetAxis("Horizontal") * Time.deltaTime;
        pos.z += moveSpeed * Input.GetAxis("Vertical") * Time.deltaTime;

        transform.position = pos; //update marine position with the new position (pos)
        */
		
	}
}
