using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoesFlip : MonoBehaviour {
    [SerializeField] bool flip = false;
    [SerializeField] AudioClip flipSound;

    GameObject start, finish;
    Vector3 startPosition, finishPosition;
    new AudioSource audio;

    private void Awake() {


        start = GameObject.FindWithTag("Start");
        finish = GameObject.FindWithTag("Finish");
        startPosition = start.transform.position;
        finishPosition = finish.transform.position;

        audio = GetComponent<AudioSource>();
        audio.Stop();
    }
    private void Start() {
        //EventManager.OnHalfway += DoFlip;
    }
    private void OnDisable() {
        //EventManager.OnHalfway -= DoFlip;
    }
    private void DoFlip() {
        if(flip) {
            finish.transform.position = startPosition;
            start.transform.position = finishPosition;
            flip = false;
            PlayAudio();
        }
    }

    public bool isFlip() {
        return flip;
    }

    public void PlayAudio() {
        if(audio.isPlaying) {return;}
        audio.PlayOneShot(flipSound);
    }
}
