using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    public static event Action OnExitGhost, OnHalfway, OnRemoveTrigger, OnWormhole, 
    OnExitWormhole, OnEnterWormhole;
    BoxCollider box;

    GameObject player = null;

    enum Axis {xLocal, yLocal, zLocal};

    private void Awake() {
        box = GetComponent<BoxCollider>();
    }
    
    private void OnTriggerEnter(Collider other) {
        switch(other.gameObject.tag) {
            case "Ghost": {
                GhostCollideEnter(other);
                break;
            }
            case "Wormhole": {
                WormholeCollideEnter(other);
                break;
            }
            default: {
                break;
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        switch(other.gameObject.tag) {
            case "Ghost": {
                GhostCollideExit(other);
                break;
            }
            case "Wormhole": {
                WormholeCollideExit(other);
                break;
            }
            default: {
                break;
            }
        }
    }

    private void OnCollisionEnter(Collision other) {
        switch(other.gameObject.tag) {
            case "Start":
            case "Finish": {
            FlipCollide(other); 
            break;
            }
            default: {
                break;
            }
        }

    }

    private void GhostCollideEnter(Collider other) {

    }

    private void GhostCollideExit(Collider other) {
        if (other.gameObject.tag == "Player") {
            this.gameObject.tag = "Enemy";
            OnExitGhost?.Invoke();
        }
    }

    private void WormholeCollideEnter(Collider other) {
        SetPlayerObject(this.gameObject);
        OnEnterWormhole?.Invoke();
    }

    private void WormholeCollideExit(Collider other) {
        OnExitWormhole?.Invoke();
    }

    private void FlipCollide(Collision other) {
        DoesFlip flip = other.gameObject?.GetComponent<DoesFlip>();
            if(other.gameObject.tag == "Finish" && flip.isFlip()) {
                OnHalfway?.Invoke();

                if(this.gameObject.tag == "Player")
                    TeleportPlayerRelative(this.gameObject, Axis.yLocal, 40);
                
                OnRemoveTrigger?.Invoke();
            }
        else{ return; }
    }

    private void TeleportPlayerRelative(GameObject player, Axis ax, int diffrence) {
        player.transform.position = newPosition(player, ax, diffrence);
    }

    private Vector3 newPosition(GameObject itemObject, Axis ax, int diffrence) {
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

    priave void SetPlayerObject(GameObject player) {
        player = p;
    }

    private GameObject GetPlayerObject() {
        return player;
    }
}