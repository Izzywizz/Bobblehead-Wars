using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    //Properties
    public Rigidbody head;
    public LayerMask layerMask; //what layer the ray (for raycasting) should hit
    public float moveSpeed = 50.0f;
    public Animator bodyAnimator;
    private CharacterController characterController;
    private Vector3 currentLookTarget = Vector3.zero; //where the marine should look

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
        if (moveDirection == Vector3.zero) {
            bodyAnimator.SetBool("IsMoving", false);
        }
        else {
            head.AddForce(transform.right * 150, ForceMode.Acceleration);
            //you apply force in a paticular direction which is multipled by a force amount
            bodyAnimator.SetBool("IsMoving", true);
        }

        RaycastHit hit; //empty hit but will be filled with a hit when it does
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //casting a ray from the camera to the mouse pointer
        Debug.DrawRay(ray.origin, ray.direction * 1000, Color.green);// This will draw agreen ray on to the screen   

        if (Physics.Raycast(ray, out hit, 1000, layerMask, QueryTriggerInteraction.Ignore)) {
            //cast the ray with Pysics.Raycast
            // pass in the ray that I've created along with the hit, out is just passing the reference of hit
            // 1000 describe length of ray
            // layermask lets the raycast is what we are trying to hit
            // don't activate triggers

            if (hit.point != currentLookTarget) {
                currentLookTarget = hit.point; //impact point where the mouse is pointing thus update the marine where its pointing
            }
            //1 - obtain raycast/ target position, not the y coord becasue you want the marine to look fwd not at the floor
            Vector3 targetPosition = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            // 2 - quaternion is rotation, to find the current position you subtract the target position from current then tell it where 
            // to look using lookRotation
            Quaternion rotation = Quaternion.LookRotation(targetPosition - transform.position);
            // 3 - actaully do the turn, Lerp allows the turing to be smooth over time by finding its location over time
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * 10.0f);
        }
    }
}
