using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {
    [SerializeField] float rcsThrust = 50f;
    [SerializeField] float mainThrust = 20f;
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

    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                //do nothing
                print("OK");
                break;
            case "Fuel":
                print("Fuel");
                break;
            default:
                print("Dead");
                //kill player
                break;
        }
    }

    private void Thrust()
    {
        //float movementSpeed = mainThrust * Time.deltaTime;
        if (Input.GetKey(KeyCode.Space))
        {
            rigidBody.AddRelativeForce(Vector3.up * mainThrust);

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
       
        float rotateThisFrame = rcsThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.A))
        {
            //print("lewo");
            transform.Rotate(Vector3.forward * rotateThisFrame);

        }

        else if (Input.GetKey(KeyCode.D))
        {
            //print("prawo");
            
            transform.Rotate(Vector3.back * rotateThisFrame);
        }

        rigidBody.freezeRotation = false; //resume physics controls of rotation
    }
}
