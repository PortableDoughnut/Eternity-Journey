using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] AudioClip thrustSound;
    [SerializeField] float thrust = 100f;
    [SerializeField] float rotateThrust = 1f;
    [SerializeField] ParticleSystem leftThrustParticle, rightThrustParticle, mainThrustParticle;

    bool noClip = false;

    Rigidbody rb;
    new AudioSource audio;

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
        audio.Stop();
    }

    // Update is called once per frame
    void Update() {
        ProcessThrust();
        ProcessRotate();
    }

    //If player is pushing the space key move the player forward
    void ProcessThrust() {
        if(Input.GetKey(KeyCode.Space))
        {
            OnThrust();
        }
        else
        {
            OnThrustStop();
        }
    }

        //If the player is pushing left or right it will rotate the player. you cannot rotate both ways at the same time. I used an elf if statement on the right if statement to make this happen because that is the simplest way I have found to do it. Also the video online used it.
    void ProcessRotate() {
        if(Input.GetKeyDown(KeyCode.A)) {
            RotateLeft();
        }
        else if(Input.GetKey(KeyCode.D)) { 
            RotateRight();
        }
    }

    void OnThrust()
    {
        rb.AddRelativeForce(Vector3.up * thrust * Time.deltaTime);
        //Will play the rocket thrust audio if it is not already playing and the space bar is being pressed
        if (!audio.isPlaying)
        {
            OnAudioPlay();
        }
    }

    void RotateLeft() {
        Rotate(rotateThrust);
        if(!leftThrustParticle.isEmitting)
            leftThrustParticle.Play();
        else
            leftThrustParticle.Stop();
    }

    void RotateRight() {
        Rotate(-rotateThrust);
        if(!rightThrustParticle.isEmitting)
            rightThrustParticle.Play();
        else
            rightThrustParticle.Stop();
    }
    
    void OnThrustStop()
    {
        audio.Stop();
        mainThrustParticle.Stop();
    }

    private void OnAudioPlay()
    {
        audio.PlayOneShot(thrustSound);
        mainThrustParticle.Play();
    }

    void Rotate(float rotateThisFrame) {
        //Freezing rotation so we can manually rotate without bugs
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotateThisFrame * Time.deltaTime);
        //Unfreezing so the game can use it's physics engine again
        rb.freezeRotation = false;
    }
}