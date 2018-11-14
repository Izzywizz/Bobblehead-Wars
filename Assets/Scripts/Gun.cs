using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

    public GameObject bulletPrefab;
    public Transform launchPositon; //barrel of the marine gun
    public bool isUpgraded; //tripple gun mode flag
    public float upgradedTime = 10.0f; 
    private float currentTime; // keeps track of how long it’s been since the gun was upgraded.
    private AudioSource audioSource;


	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>(); //reference to attached audioSource
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

        Rigidbody bullet = createBullet();
        // Direction is determined by the transform of the object to which this script is attached (space marine)
        bullet.velocity = transform.parent.forward * 100;

        if (isUpgraded) {
            Rigidbody bullet2 = createBullet();
            bullet2.velocity = (transform.right + transform.forward / 0.5f) * 100; //fire a bullet right and then left(below)

            Rigidbody bullet3 = createBullet();
            bullet3.velocity = ((transform.right * -1) + transform.forward / 0.5f) * 100; //-1 used because there is no left property
        }

        //sound fire
        if (isUpgraded) {
            audioSource.PlayOneShot(SoundManager.Instance.upgradedGunFire);

        } else {
            audioSource.PlayOneShot(SoundManager.Instance.gunFire); //playOneShot allows overlapping sound
        }
    }

    // 1 - create a bullet prefab object
    // 2 - bullet prefab position is set to the laucher positionn
    // 3 - WE can access the Rigdebody component bc its attached to the bullet/sphere prefab.
    private Rigidbody createBullet() {
        GameObject bullet = Instantiate(bulletPrefab) as GameObject;
        bullet.transform.position = launchPositon.position;

        return bullet.GetComponent<Rigidbody>();
    }

    public void UpgradedGun() {
        isUpgraded = true;
        currentTime = 0;
    }
}
