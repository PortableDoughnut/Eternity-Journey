using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    //This is the Audio Clip used when the player is moving for the thrust sound.
    [SerializeField] AudioClip thrustSound;
    //This variable is used for how fast the player will move.
    [SerializeField] float thrust = 100f;
    //This variable is used for how fast the player will rotate.
    [SerializeField] float rotateThrust = 1f;
    // Set the distance from the camera that the player will be locked to
    [SerializeField] public float zDistance = 1.0f;
    //These are the particle systems for when the player is moving for thrust.
    [SerializeField] ParticleSystem mainThrustParticle;

    //This is the varible used for accessing the Rigidbody attached to the GameObject.
    Rigidbody rb;
    //This is the varible for playing sounds.
    new AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        //This is setting the rb variable to the Rigidbody attached to the GameObject.
        rb = GetComponent<Rigidbody>();
        //This is setting the audio variable to the Audio Source attached to the GameObject.
        audio = GetComponent<AudioSource>();
        //This stops all audio
        audio.Stop();
        //This locks the cursor to the window.
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotate();
        // Set the player's position to be locked to the 2D plane
        transform.position = new Vector3(transform.position.x, transform.position.y, zDistance);
    }

    //If player is pushing the space key move the player forward
    void ProcessThrust()
    {
        //If the space key is pressed it runs OnThrust() otherwise it runs OnThrustStop()
        if (Input.GetKey(KeyCode.Space))
        {
            OnThrust();
        }
        else
        {
            OnThrustStop();
        }
    }

    //If the player is pushing left or right it will rotate the player. you cannot rotate both ways at the same time. I used an elf if statement on the right if statement to make this happen because that is the simplest way I have found to do it. Also the video online used it.
    void ProcessRotate()
    {
        //Calls RotateLeft() if the A key is pressed.
        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        //Calls RotateRight() if the D key is pressed.
        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
    }

    /*
     * This runs when the space key is presssed.
     * This moves the player forward.
     */
    void OnThrust()
    {
        //This adds force to the player moving them forward.
        rb.AddRelativeForce(Vector3.up * thrust * Time.deltaTime);
        //Will play the rocket thrust audio if it is not already playing and the space bar is being pressed
        if (!audio.isPlaying)
        {
            OnMainThrust();
        }
    }

    /*
     * This runs if the A key is pressed.
     * This rotates the player left.
     */
    void RotateLeft()
    {
        //This calls the Rotate() method with the variable roatateThrust as the param.
        Rotate(rotateThrust);
    }

    /*
     * This runs if the D key is pressed.
     * This rotates the player right.
     */
    void RotateRight()
    {
        //This calls the Rotate() method with the negitive of the variable roatateThrust as the param.
        Rotate(-rotateThrust);
    }

    //This runs when the player stops pressing the spacebar.
    void OnThrustStop()
    {
        //This stops all audio.
        audio.Stop();
        //This stops the mainThrustParticle.
        mainThrustParticle.Stop();
    }

    //This runs when the plyaer presses the spacebar.
    private void OnMainThrust()
    {
        //This plays the thrustSound once.
        audio.PlayOneShot(thrustSound);
        //This plays the mainThrustParticle.
        mainThrustParticle.Play();
    }

    /*
     * This rotates the player.
     * The flaot param rotateThisFrame is used to say how fast to rotate and in which direction.
     */
    void Rotate(float rotateThisFrame)
    {
        //Freezing rotation so we can manually rotate without bugs
        rb.freezeRotation = true;  // freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotateThisFrame * Time.deltaTime);
        rb.freezeRotation = false;  // unfreezing rotation so the physics system can take over

    }
}