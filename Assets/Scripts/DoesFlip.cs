using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoesFlip : MonoBehaviour {
    [SerializeField] bool flip = false;

    GameObject start, finish;
    Vector3 startPosition, finishPosition;

    private void Awake() {


        start = GameObject.FindWithTag("Start");
        finish = GameObject.FindWithTag("Finish");
        startPosition = start.transform.position;
        finishPosition = finish.transform.position;
    }
    private void Start() {
        EventManager.OnHalfway += DoFlip;
    }
    private void OnDisable() {
        EventManager.OnHalfway -= DoFlip;
    }
    private void DoFlip() {
        if(flip) {
            finish.transform.position = startPosition;
            start.transform.position = finishPosition;
            flip = false;
        }
    }

    public bool isFlip() {
        return flip;
    }
}
