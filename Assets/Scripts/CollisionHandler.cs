using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] AudioClip deathSound, successSound;
    [SerializeField] ParticleSystem deathParticle, successParticle;
    [SerializeField] float respawnTime, reloadTime = 0.5f;
    new AudioSource audio;

    bool isTrans, isSuccess, noClip = false;
    int currentSceneIndex = 0;

    void Start() {
        audio = GetComponent<AudioSource>();
    }

    void Update() {
        ProcessLevelSkip();
        ProcessNoClip();
    }

    void ProcessNoClip()
    {
        if(Input.GetKeyDown(KeyCode.C))
        //Toggles COllision
            noClip = !noClip;
    }

    void ProcessLevelSkip() {
        if(Input.GetKeyDown(KeyCode.L))
            LoadNextLevel();
    }

    void OnCollisionEnter(Collision other) {
        if(isTrans || noClip) { return; }
        //This variable is the index of the current scene
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        switch (other.gameObject.tag) {
            case "Start":
                Debug.Log("Level Started.");
                break;
                
            case "Obstacle":
                break;
            
            case "Ghost": 
                break;

            case "Wormhole": 
                break;
            //Loads the next level by calling the LoadNextLevel() method when the Landing Pad is touched.
            case "Finish":
                DoesFlip flip = (DoesFlip)other.gameObject?.GetComponent<DoesFlip>();
                if(!flip.isFlip())
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
        DoesFlip flip = (DoesFlip)other.gameObject?.GetComponent<DoesFlip>();
        if(!flip.isFlip()) {
            Debug.Log("Finished");
            isTrans = true;
            GetComponent<Movement>().enabled = false;
            audio.Stop();
            audio.PlayOneShot(successSound);
            successParticle.Play();
            Invoke("LoadNextLevel", reloadTime);
        }
    }

    IEnumerator PhysicalWait()
    {
        yield return new WaitForSeconds(5);
    }
}
