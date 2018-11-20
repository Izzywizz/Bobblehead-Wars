using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arena : MonoBehaviour {

    public GameObject player;
    public Transform elevator;
    private Animator arenaAnimator;
    private SphereCollider sphereCollider;

	// Use this for initialization
	void Start () {

        arenaAnimator = GetComponent<Animator>();
        sphereCollider = GetComponent<SphereCollider>();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        //disbale camera & make the player a child of the lift
        Camera.main.transform.parent.gameObject.GetComponent<CameraMovement>().enabled = false;
        player.transform.parent = elevator.transform;
        //disable player's movement
        player.GetComponent<PlayerController>().enabled = false;
        SoundManager.Instance.PlayOneShot(SoundManager.Instance.elevatorArrived);
        arenaAnimator.SetBool("OnElevator", true);
    }

    //methods
    public void ActivatePlatform()
    {
        sphereCollider.enabled = true;
    }
}
