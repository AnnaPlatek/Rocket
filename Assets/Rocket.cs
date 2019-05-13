﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

    Rigidbody rigidBody;
    AudioSource audioSource;

	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        Thrust();
        Rotate();
	}

    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rigidBody.AddRelativeForce(Vector3.up);

            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }

        }

        if (Input.GetKeyUp(KeyCode.Space))
        {

            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }

        }
    }


    private void Rotate()
    {
        rigidBody.freezeRotation = true; //take manual control of rotation

        if (Input.GetKey(KeyCode.A))
        {
            //print("lewo");
            transform.Rotate(Vector3.forward * Time.deltaTime * 50);

        }

        else if (Input.GetKey(KeyCode.D))
        {
            //print("prawo");
            transform.Rotate(Vector3.back * Time.deltaTime * 50);
        }

        rigidBody.freezeRotation = false; //resume physics controls of rotation
    }
}
