using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Alien : MonoBehaviour {

    public Rigidbody head;
    public bool isAlive = true;
    public UnityEvent OnDestroy;
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

        if (isAlive)
        {
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



	}

    void OnTriggerEnter(Collider other)
    {

        if (isAlive)
        {
            //sound for alien death can't be attached to the alien bc when the object is destroyed there goes the audio for it
            //instread we pass it to the gameManager to handle
            SoundManager.Instance.PlayOneShot(SoundManager.Instance.alienDeath);

            //Remember, you set the alien’s Rigidbody to Is Kinematic, so the Rigidbody won't respond to collision events because the navigation system is in control.
            //That said, you can still be informed when a Rigidbody crosses a collider through trigger events. Thus destroying the alien obj
            Die();
        }

    }

    public void Die() {

        isAlive = false;
        head.GetComponent<Animator>().enabled = false;
        head.isKinematic = false;
        head.useGravity = true;
        head.GetComponent<SphereCollider>().enabled = true;
        head.gameObject.transform.parent = null;
        head.velocity = new Vector3(0, 26.0f, 3.0f); //lanuches head off

        OnDestroy.Invoke(); //generate the event, gameManager is listening for it and it notifies other listeners of the event
        OnDestroy.RemoveAllListeners(); //prevent reference cycle memory leak
        SoundManager.Instance.PlayOneShot(SoundManager.Instance.alienDeath);
        Destroy(gameObject);
    }
}
