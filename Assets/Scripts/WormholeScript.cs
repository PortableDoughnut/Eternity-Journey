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

    new EventManager eventM;

        /**
            This method excutes when the object is first loaded.
            In this method we setup the AudioSource instance and stop any audio that might be playing.
        **/
    private void Awake() {
            //Getting the Audio Source attached to the game object and attaching it to the AudioSource instance we set up earlier.
        audio = GetComponent<AudioSource>();
            //Stops any audio that might be playing just to be safe.
        audio.Stop();

        
    }

    void Start() {
        //EventManager.OnWormhole += DoWormhole;
        //EventManager.OnEnterWormhole += DoEnterWormhole;
        //EventManager.OnExitWormhole += DoExitWormhole;
    }

    void OneDisable() {
        //EventManager.OnWormhole -= DoWormhole;
        //EventManager.OnEnterWormhole -= DoEnterWormhole;
        //EventManager.OnExitWormhole -= DoExitWormhole;
    }

    /**
        This method checks if the tag is Player on the object. If it isn't it returns.
        After that it changes the location of the player to the position of the object we specified earlier.
        Finally it calls PlayAudio() to play the sound of teleporting.
    **/
    // private void OnCollisionEnter(Collision other) {
    //         //This makes sure that the tag is set to "Player". If it isn't it returns, cancelling the method.
    //     if(other.gameObject.tag != "Player") { return; }
    //         //This teleports the player to the location specified in the gui in wormholeTo.
    //     other.transform.position = wormholeTo.transform.position;
    //         //Calls PlayAudio(). This will play the sound specified for teleportation.
    //     PlayAudio();
    // }  

    private void DoWormhole() {

    }

    private void DoEnterWormhole() {
        eventM.GetPlayerObject().transform.position = wormholeTo.transform.position;
        PlayAudio();
    }

    private void DoExitWormhole() {

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
