﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    //sound effects library
    public AudioClip gunFire;
    public AudioClip upgradedGunFire;
    public AudioClip hurt;
    public AudioClip alienDeath;
    public AudioClip marineDeath;
    public AudioClip victory;
    public AudioClip elevatorArrived;
    public AudioClip powerUpPickup;
    public AudioClip powerUpAppear;


    //access is needed throughtout the game for the sound manager
    public static SoundManager Instance = null;
    private AudioSource soundEffectAudio;


	// Use this for initialization
	void Start () {

        //singleton pattern, only one copy of SoundManager should exist
        if (Instance == null)
        {
            Instance = this;
        } else if (Instance != null)
        {
            Destroy(gameObject);
        }

        AudioSource[] sources = GetComponents<AudioSource>(); //this will get both audio sources
        foreach (AudioSource source in sources) {

            if (source.clip == null){//remember we've only set the background audio on one audio source, On the other AudioSource source.clip its null
                soundEffectAudio = source;
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
