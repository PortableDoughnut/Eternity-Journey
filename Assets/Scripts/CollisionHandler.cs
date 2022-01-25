using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] AudioClip deathSound, successSound;
    [SerializeField] float respawnTime, reloadTime = 0.5f;
    new AudioSource audio;
    int currentSceneIndex = 0;

    void Start() {
        audio = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision other)
    {
        //This variable is the index of the current scene
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
       switch (other.gameObject.tag) {
           case "Friendly":
               Debug.Log("Level Started.");
               break;
            
            //Loads the next level by calling the LoadNextLevel() method when the Landing Pad is touched.
            case "Finish":
                SuccessSequence();
                break;
            case "Fuel":
                Debug.Log("You got Fuel");
                break;
           default:
               Invoke("CrashSequence", respawnTime);
               break;
       }
    }

    void CrashSequence() {
        audio.PlayOneShot(deathSound);
        //TODO add particle effect
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", respawnTime);
    }

    void ReloadLevel() {
        //This loads the current scene again
        SceneManager.LoadScene(currentSceneIndex);
    }

    void SuccessSequence() {
        audio.PlayOneShot(successSound);
        //TODO add success particle effect
        Invoke("LoadNextLevel", reloadTime);
    }

    //This loads the next level
    void LoadNextLevel() {
        int nextSceneIndex = currentSceneIndex + 1;
        //If it's the last scene it will load the first one again
        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
            nextSceneIndex = 0;
        SceneManager.LoadScene(nextSceneIndex);
    }
}
