using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    /*
    * These are the sounds that are played on death and sucess on level finish.
    * They need to be added in the unity gui.
    */
    [SerializeField] AudioClip deathSound, successSound;
    /*
    * These are the particles initiated on death and sucess on level finish.
    * They need to be added in the unity gui.
    */
    [SerializeField] ParticleSystem deathParticle, successParticle;
    /*
    * This is the amount of time needed to respawn on death, or to load the next level.
    * They need to be added in the unity gui.
    */
    [SerializeField] float respawnTime, reloadTime = 0.5f;
    //You put the landing pad in this spot so that collision can check wether to see the collision as a success or not.
    [SerializeField] GameObject finish;

    //AudioSource variable used to play aduio.
    new AudioSource audio;

    /*
     * isTrans is for telling if the game is in a transiion period during the success sequence or the crash sequence. 
     * This will disable any collision in the OnCollisionEnter() method.
     */ 
    bool isTrans;
    /*
    * This currentSceneIndex variable is used for getting the current scene.
    * This is used in OnCollisionEnter() and ReloadLevel() methods.
    */ 
    int currentSceneIndex = 0;

    //The Start method called at the Start of the program.
    void Start() {
        //setting the AudioSource vairable here for use with the AudioClips.
        audio = GetComponent<AudioSource>();
    }

    //This method is called whenever the player collides with something.
    void OnCollisionEnter(Collision other) {
        /*
         * We are checking if we are in a transition period either in a crash sequence or a success sequence.
         * If we are then it returns the method and nothing else in this method is ran.
         */
        if(isTrans)
            return;

        /*
         * This Variable is used for setting the currentSceneIndex to the current level.
         */
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        //This is a Switch statement where it decides what to do depending on the tag on the object it is colliding with.
        switch (other.gameObject.tag) {
            case "Start":
                Debug.Log("Level Started.");
                break;
                
            case "Obstacle":
            case "Ghost": 
            case "Wormhole":
            case "Flip":
                Debug.Log("Tag is " + this.gameObject.tag);
                break;

            //Loads the next level by calling the LoadNextLevel() method when the Landing Pad is touched.
            case "Finish":
                Debug.Log("Coll Tag is " + other.gameObject.tag);
                DoesFlip flippy = GameObject.Find("Landing Pad").GetComponent<DoesFlip>();
                Debug.Log("Flippy is " + flippy.flip);
                if(!flippy.flip)
                    SuccessSequence(other);
                break;

            case "Enemy":
                StartCoroutine(PhysicalWait());
                CrashSequence();
                break;

            default:
                CrashSequence();
                break;
        }
    }

     //This loads the next level
    void LoadNextLevel() {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings) {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }


    void ReloadLevel() {
        //This loads the current scene again
        SceneManager.LoadScene(currentSceneIndex);
    }

    void CrashSequence() {
        isTrans = true;
        GetComponent<Movement>().enabled = false;
        audio.Stop();
        audio.PlayOneShot(deathSound);
        deathParticle.Play();
        Invoke("ReloadLevel", respawnTime);
    }

    void SuccessSequence(Collision other) {
            Debug.Log("Finished");
            isTrans = true;
            GetComponent<Movement>().enabled = false;
            audio.Stop();
            audio.PlayOneShot(successSound);
            successParticle.Play();
            Invoke("LoadNextLevel", reloadTime);
    }

    IEnumerator PhysicalWait()
    {
        yield return new WaitForSeconds(5);
    }
}
