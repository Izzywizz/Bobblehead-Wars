﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathParticles : MonoBehaviour {

    private ParticleSystem deathParticles;
    private bool didStart = false;

    // Use this for initialization
    void Start () {
        deathParticles = GetComponent<ParticleSystem>();
    }
	
	// Update is called once per frame
	void Update () {
        if (didStart && deathParticles.isStopped)
        {
            Destroy(gameObject);
        }
    }

    public void Activate()
    {
        didStart = true;
        deathParticles.Play();
    }

    public void SetDeathFloor(GameObject deathFloor)
    {
        if (deathParticles == null)
        {
            deathParticles = GetComponent<ParticleSystem>();
        }
        deathParticles.collision.SetPlane(0, deathFloor.transform);
    }
}
