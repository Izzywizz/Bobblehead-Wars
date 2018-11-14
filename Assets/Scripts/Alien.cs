using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Alien : MonoBehaviour {

    public Transform target;
    public float navigationUpdate; // the time, when the alien should update its path
    private float navigationTime = 0; //time since last path updated
    private NavMeshAgent agent;

	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {

        if (target != null)
        {
            navigationTime += Time.deltaTime; //checks the passage of time
            if (navigationTime > navigationUpdate)
            {
                agent.destination = target.position; //marine position
                navigationTime = 0;//reset counter 
            }
        }

	}

    void OnTriggerEnter(Collider other)
    {
        //sound for alien death can't be attached to the alien bc when the object is destroyed there goes the audio for it
        //instread we pass it to the gameManager to handle
        SoundManager.Instance.PlayOneShot(SoundManager.Instance.alienDeath);

        //Remember, you set the alien’s Rigidbody to Is Kinematic, so the Rigidbody won't respond to collision events because the navigation system is in control.
        //That said, you can still be informed when a Rigidbody crosses a collider through trigger events. Thus destroying the alien obj
        Destroy(gameObject);

    }
}
