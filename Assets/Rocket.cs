using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour {
    [SerializeField] float rcsThrust = 50f;
    [SerializeField] float mainThrust = 20f;
    Rigidbody rigidBody;
    AudioSource audioSource;
    enum State { Alive, Dying, Transcending}
    State state = State.Alive;

	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update ()
    {// todo somewhere stop sound on death
        if (state == State.Alive)
            {
            Thrust();
            Rotate();
            }

	}

    void OnCollisionEnter(Collision collision)
    {
        if (state != State.Alive)
        {
            return;
        }
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                //do nothing
                //print("OK");
                break;
            //case "Fuel":
                //print("Fuel");
                //break;
            case "Finish":
                state = State.Transcending;
                Invoke("LoadNextLevel", 1f); //parameterise time
                break;
            default:
                print("Hit something deadly");
                state = State.Dying;
                Invoke("LoadFirstLevel", 1f); //parameterise time
                break;
        }
    }

    private void LoadFirstLevel()
    {
        SceneManager.LoadScene(0);
    }

    private void LoadNextLevel()
    {
        SceneManager.LoadScene(1); // todo load more levels (n+1) 
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
