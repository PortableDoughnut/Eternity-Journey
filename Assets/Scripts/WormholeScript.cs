using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormholeScript : MonoBehaviour
{
        //Gets the Object which is to be teleported to from Unity gui.
    [SerializeField] GameObject wormholeTo;
        //Gets the Audio Clip that is to be played when the teleportation happens
    [SerializeField] AudioClip wormholeSound;

    //Creates a AudioSource instance to use for playing the audio
    new AudioSource audio;

    GameObject player;

        /**
            This method excutes when the object is first loaded.
            In this method we setup the AudioSource instance and stop any audio that might be playing.
        **/
    private void Awake() {
        //Getting the Audio Source attached to the game object and attaching it to the AudioSource instance we set up earlier.
        audio = GetComponent<AudioSource>();
        player = GameObject.FindWithTag("Player");
        //Stops any audio that might be playing just to be safe.
        audio.Stop();

        
    }

    void OnTriggerEnter(Collider other)
    {
        player.transform.position = wormholeTo.transform.position;
        PlayAudio();
    }

    /**
        This method checks if audio is playing. If it is it returns cancelling the method.
        After that it plays the specifed sound with the PlayOneShot() method.
    **/
    public void PlayAudio() {
            //Checks if audio is playing. if it is it returns.
        if(audio.isPlaying) { return; }
            //Uses the PlayOneShot() Method to play the sound specified in the Unity gui.
        audio.PlayOneShot(wormholeSound);
    }
}
