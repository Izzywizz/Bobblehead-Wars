using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //Methods

    // when the bullet goes off camera, remove it this includes the scence view camera (All cameras!)
    private void OnBeameVisible()
    {
        Destroy(gameObject);
    }

    //called during a collision, the collision object gives you info about the target collision as well.
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
