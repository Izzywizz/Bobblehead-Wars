using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    public GameObject followTarget; //marine
    public float moveSpeed; // of the camera

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
        if (followTarget != null) {

            //Lerp, takes a start position (cameraMount) and end position(player) and a 3rd value between the two to give the camera its position
            //The Time.deltaTime takes into account time passed since last frame by moveSpeed to make the camera movement smooth.
            transform.position = Vector3.Lerp(transform.position, followTarget.transform.position, Time.deltaTime * moveSpeed);
        }
    }
}
