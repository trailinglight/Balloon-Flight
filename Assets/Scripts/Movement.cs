using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    //components
    private AudioSource playerAudioSource;
    private Rigidbody playerRigidbody;

    //internal (serialized) variables
    [SerializeField] private float mainThrust = 5.0f;
    [SerializeField] private float rotationThrust = 5.0f;
    [SerializeField] private float maxAngularVelocity = 1.5f;
    [SerializeField] [Tooltip ("maximum upward velocity (m/s)")] private float maxVelocity = 5.0f;

    [SerializeField] private AudioClip mainEngineSound;

    private bool isThrusting = false;
    private bool isRotationAllowed = false;

    // Start is called before the first frame update
    void Start()
    {
        playerAudioSource = GetComponent<AudioSource>();
        playerRigidbody = GetComponent<Rigidbody>();
        playerRigidbody.maxAngularVelocity = maxAngularVelocity;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isThrusting = true;
        }

        //if (transform.rotation.z <= 25.0f && transform.rotation.z >= -25.0f)
        //{
        //    isRotationAllowed = true;
        //}
        //ProcessRotation();
    }

    private void FixedUpdate() 
    {
        ProcessThrust();
        ProcessTorque();
    }

    //it would be good to figure out directly how to calculate acceleration wanted based on a clamped max speed
    //should read through different force modes and understand how things are calculated
    private void ProcessThrust()
    {
        //getaxis max be better
        if (isThrusting)
        {
            //Vector3 thrustForce = Vector3.up * mainThrust; //Time.fixedDeltaTime isn't needed here as calculations are already made frame rate independent
            //Debug.Log("Pressed");
            //applies thrust relative to local coordinates 
            //playerRigidbody.AddRelativeForce(thrustForce, ForceMode.Impulse);
            playerRigidbody.AddRelativeForce(Vector3.up * mainThrust, ForceMode.Impulse);

            if (!playerAudioSource.isPlaying) 
            {
                playerAudioSource.PlayOneShot(mainEngineSound);
            }
            
            isThrusting = false;
            //playerRigidbody.AddRelativeForce(thrustForce);  
            //Debug.Log("Apply thrust.");
        }
        else if(playerAudioSource.isPlaying)
        {
            playerAudioSource.Stop();
        }
        
        //playerRigidbody.velocity = Vector3.ClampMagnitude(playerRigidbody.velocity, maxVelocity);
        
        //if(playerRigidbody.velocity.magnitude > maxVelocity)
        //{
        //    playerRigidbody.velocity = playerRigidbody.velocity * maxVelocity;
        //}
    }

    //need to figure out how to prevent rotation going beyond certain degrees
    private void ProcessTorque()
    {
        if (Input.GetKey(KeyCode.A))
        {
            //Debug.Log("Swinging Left");
            playerRigidbody.AddRelativeTorque(Vector3.forward * rotationThrust, ForceMode.Force);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            //Debug.Log("Swinging Right");
            playerRigidbody.AddRelativeTorque(Vector3.back * rotationThrust, ForceMode.Force);
        }

       
    }



    //may be wise to have no left and right movement with the player (or background) automatically translating for appearances sake)
    private void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            //Debug.Log("Swinging Left");
            ApplyRotation(rotationThrust);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            //Debug.Log("Swinging Right");
            ApplyRotation(-rotationThrust);
        } 
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
    }

}
