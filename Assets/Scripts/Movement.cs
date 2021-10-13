using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class Movement : MonoBehaviour
{

    [SerializeField] float thrustSpeed = 1000f;
    [SerializeField] float rotationSpeed = 20f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem jetParticle;

    AudioSource myAudioSource;
    Rigidbody myRigidbody;
    // Start is called before the first frame update
    void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
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
        if (Input.GetKey(KeyCode.Space))
        {
            myRigidbody.AddRelativeForce(Vector3.up * thrustSpeed * Time.deltaTime);
            if (!myAudioSource.isPlaying)
            {
                myAudioSource.PlayOneShot(mainEngine);
                
            }
            if (!jetParticle.isPlaying)
            {
                jetParticle.Play();
            }
        }
        else
        {
            myAudioSource.Stop();
            jetParticle.Stop();
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
        myRigidbody.freezeRotation = true; //freezing rotation so we cen manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        myRigidbody.freezeRotation = false;
    }
}
