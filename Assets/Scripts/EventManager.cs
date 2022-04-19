using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    public static event Action OnExit, OnHalfway, OnRemoveTrigger;
    BoxCollider box;

    private void Awake() {
        box = GetComponent<BoxCollider>();
    }
    
    private void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "Player") {
            this.gameObject.tag = "Enemy";
            OnExit?.Invoke();
        }
    }

    private void OnCollisionEnter(Collision other) {
            DoesFlip flip = other.gameObject?.GetComponent<DoesFlip>();
            if(other.gameObject.tag == "Finish" && flip.isFlip()) {
                OnHalfway?.Invoke();
                this.gameObject.transform.Translate(0, 40, 0, Space.World);
                OnRemoveTrigger?.Invoke();
            }
        else{ return; }
    }
}