using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

    Rigidbody rigidBody;

	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        ProcessInput();
	}

    private void ProcessInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rigidBody.AddRelativeForce(Vector3.up);
        }

        if(Input.GetKey(KeyCode.A))
        {
            print("lewo");
            transform.Rotate(Vector3.forward*Time.deltaTime);

        }

        else if (Input.GetKey(KeyCode.D))
        {
            print("prawo");
            transform.Rotate(Vector3.back*Time.deltaTime);
        }
    }
}
