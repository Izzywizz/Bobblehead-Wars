using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaWall : MonoBehaviour {

    private Animator arenaAnimator;

    // Use this for initialization
    void Start() {
        //because WallTrigger is a child of the BobbleArena, you can get a reference to the animator by accessing the WallTrigger’s parent property
        GameObject arena = transform.parent.gameObject;
        arenaAnimator = arena.GetComponent<Animator>();

        //First, this gets the parent GameObject by accessing the parent property on the transform.
        //Once it has a reference to the arena GameObject, it calls GetComponent() for a reference to the animator.
    }

    //When marine hits the trigger the boolean is set to true and the walls is lwoerd by the animator bc of the condition we set on it earlier
    private void OnTriggerEnter(Collider other)
    {
        arenaAnimator.SetBool("IsLowered", true);
    }

    // When the hero leaves the trigger, this code tells the Animator to set IsLowered to false to raise the walls.
    private void OnTriggerExit(Collider other)
    {
        arenaAnimator.SetBool("IsLowered", false);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
