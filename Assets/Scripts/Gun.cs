using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

    public GameObject bulletPrefab;
    public Transform launchPositon; //barrel of the marine gun

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0)) { //is left-mouse button being held
            if (!IsInvoking("fireBullet")) { //if method is not being started, call it with Invoke
                InvokeRepeating("fireBullet", 0f, 0.1f); //repeatedly call method to fire until cancelInvoke() is called.
                //parameters inc startTime and repeat rate
            }
        }

        if (Input.GetMouseButtonUp(0)) {
            CancelInvoke("fireBullet");//camcel method call thus stopping the firing of the bullet when the left mouse button is released
        }
    }

    //Methods
    void fireBullet() {
        // 1 - create a bullet prefab object
        GameObject bullet = Instantiate(bulletPrefab) as GameObject;
        // 2 - bullet prefab position is set to the laucher positionn
        bullet.transform.position = launchPositon.position;
        // 3 - WE can access the Rigdebody component bc its attached to the bullet/sphere prefab.
        // Direction is determined by the transform of the object to which this script is attached (space marine)
        bullet.GetComponent<Rigidbody>().velocity = transform.parent.forward * 100;
    }
}
