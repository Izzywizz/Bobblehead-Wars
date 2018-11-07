using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float moveSpeed = 50.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        Vector3 pos = transform.position; //current position of SpaceMarine in 3D Space (x, y, z)

        // retrives values from the input + 1(right) or - 1(left) based on key press, then multipiles this value with the moveSpeed
        // The Time.deltaTime takes into account the time passed in seconds from each frame, stopping the marine from going super fast
        pos.x += moveSpeed * Input.GetAxis("Horizontal") * Time.deltaTime;
        pos.z += moveSpeed * Input.GetAxis("Vertical") * Time.deltaTime;

        transform.position = pos; //update marine position with the new position (pos)
		
	}
}
