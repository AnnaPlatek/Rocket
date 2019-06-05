using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour {
    [SerializeField] float rcsThrust = 50f;
    [SerializeField] float mainThrust = 20f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip audioSuccess;
    [SerializeField] AudioClip audioFail;
    [SerializeField] float levelLoadDelay = 2.5f;
    [SerializeField] ParticleSystem ThrusterSmoke;
    [SerializeField] ParticleSystem particleSuccess;
    [SerializeField] ParticleSystem particleDeath;
    Rigidbody rigidBody;
    AudioSource audioSource;
    enum State { Alive, Dying, Transcending}
    State state = State.Alive;
    enum Crash { On, Off }
    Crash crash = Crash.On;

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
            RespondToThrustInput();
            RespondToRotateInput();
            }

        RespondToDebugKeys();

	}

    private void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            if (crash == Crash.On)
            {
                crash = Crash.Off;
            }
            else
            {
                crash = Crash.On;
            }

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
                StartSuccessSequence();
                break;
            default:
                //print("Hit something deadly");
                StartDeathSequence();
                break;
        }
    }

    private void StartDeathSequence()
    {
        if (crash == Crash.On)
        {
            state = State.Dying;
            Invoke("LoadFirstLevel", levelLoadDelay); //parameterise time
            audioSource.Stop();
            audioSource.PlayOneShot(audioFail);
            particleDeath.Play();
        }

        else
        {
            return;
        }

    }

    private void StartSuccessSequence()
    {
        state = State.Transcending;
        Invoke("LoadNextLevel", levelLoadDelay); //parameterise time
        audioSource.Stop();
        audioSource.PlayOneShot(audioSuccess);
        particleSuccess.Play();
    }

    private void LoadFirstLevel()
    {
        SceneManager.LoadScene(0);
    }

    private void LoadNextLevel()
    {
        SceneManager.LoadScene(1); // todo load more levels (n+1) 
    }

    private void RespondToThrustInput()
    {
        //float movementSpeed = mainThrust * Time.deltaTime;
        if (Input.GetKey(KeyCode.Space))
        {
            ThrustForward();
        }

        else
        {
            audioSource.Stop();
            ThrusterSmoke.Stop();
        }

        //if (Input.GetKeyUp(KeyCode.Space))
        //{if (audioSource.isPlaying)
        //    {audioSource.Stop();}}
    }

    private void ThrustForward()
    {
        rigidBody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);

        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }

        ThrusterSmoke.Play();
    }

    private void RespondToRotateInput()
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
