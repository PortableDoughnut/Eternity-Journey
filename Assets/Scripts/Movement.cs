using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    Rigidbody rb;
    new AudioSource audio;
    [SerializeField] float thrust = 100f;
    [SerializeField] float rotateThrust = 1f;

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
        if(Input.GetKey(KeyCode.Space)) {
            rb.AddRelativeForce(Vector3.up * thrust * Time.deltaTime);
            //Will play the rocket thrust audio if it is not already playing and the space bar is being pressed
            if(!audio.isPlaying) {
                audio.Play();
            }
        } else {
            //Will stop playing the rocket thrust sound if it is playing and the space bar is not being pressed
            if(audio.isPlaying) {
                audio.Stop();
            }
        }
    }

    //If the player is pushing left or right it will rotate the player. you cannot rotate both ways at the same time. I used an elf if statement on the right if statement to make this happen because that is the simplest way I have found to do it. Also the video online used it.
    void ProcessRotate() {
        if(Input.GetKey(KeyCode.A)) {
            Rotate(rotateThrust);
        }
        else if(Input.GetKey(KeyCode.D)) { 
            Rotate(-rotateThrust);
        }
    }

    void Rotate(float rotateThisFrame) {
        //Freezing rotation so we can manually rotate without bugs
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotateThisFrame * Time.deltaTime);
        //Unfreezing so the game can use it's physics engine again
        rb.freezeRotation = false;
    }
}