using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField ]float thrustSpeed = 1000f;
    [SerializeField] float rotationSpeed = 20f;
    

    Rigidbody myRigidbody;
    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    private void ProcessThrust()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            myRigidbody.AddRelativeForce(Vector3.up * thrustSpeed * Time.deltaTime);
        }
        
    }

    private void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            ApplyRotation(rotationSpeed);

        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            ApplyRotation(-rotationSpeed);
        }
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
    }
}
