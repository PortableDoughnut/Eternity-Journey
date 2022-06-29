using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class EventManager : MonoBehaviour
{
    enum Axis { xLocal, yLocal, zLocal };

    [SerializeField] private UnityEvent OnExit;
    [SerializeField] private UnityEvent OnEnter;
    [SerializeField] private UnityEvent OnHalfway;
    [SerializeField] private UnityEvent OnRemoveTrigger;
    [SerializeField] private UnityEvent OnWormhole;
    
    BoxCollider box;
    GameObject player;

    void Awake() {
        box = GetComponent<BoxCollider>();
        player = this.gameObject;
    }
    
    void OnTriggerEnter(Collider other) {
        OnEnter.Invoke();
    }

    void OnTriggerExit(Collider other) {
        OnExit.Invoke();
    }

    void OnCollisionEnter(Collision other) {
            if(other.gameObject.tag == "Start" || other.gameObject.tag == "Finish") {
            FlipCollide(other);
            }
    }

    void GhostCollideExit(Collider other) {
    }

    void FlipCollide(Collision other) {
        DoesFlip flip = other.gameObject?.GetComponent<DoesFlip>();
            if(other.gameObject.tag == "Finish" && flip.isFlip()) {
                OnHalfway?.Invoke();

                if(this.gameObject.tag == "Player")
                    TeleportPlayerRelative(this.gameObject, Axis.yLocal, 40);
                
                OnRemoveTrigger?.Invoke();
            }
        else{ return; }
    }

    void TeleportPlayerRelative(GameObject player, Axis ax, int diffrence) {
        player.transform.position = newPosition(player, ax, diffrence);
    }

    Vector3 newPosition(GameObject itemObject, Axis ax, int diffrence) {
        Vector3 toReturn;
        switch(ax) {
            case Axis.xLocal:
            {
                toReturn = new Vector3(itemObject.transform.position.x + diffrence, itemObject.transform.position.y, itemObject.transform.position.z);
                break;
            }
            
            case Axis.yLocal:
            {
                toReturn = new Vector3(itemObject.transform.position.x, itemObject.transform.position.y + diffrence, itemObject.transform.position.z);
                break;
            }
            
            case Axis.zLocal:
            {
                toReturn = new Vector3(itemObject.transform.position.x, itemObject.transform.position.y, itemObject.transform.position.z + diffrence);
                break;
            }
            
            default:
            {
                toReturn = new Vector3(itemObject.transform.position.x, itemObject.transform.position.y, itemObject.transform.position.z);
                break;
            }
        }
        return toReturn;
    }

    public GameObject GetPlayerObject() {
        return player;
    }
}
