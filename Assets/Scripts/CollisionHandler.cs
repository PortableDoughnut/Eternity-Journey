using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{

    void OnCollisionEnter(Collision other)
    {
       switch (other.gameObject.tag) {
           case "Friendly":
               Debug.Log("Level Started.");
               break;
            
            case "Finish":
                Debug.Log("Level Finished");
                break;
            case "Fuel":
                Debug.Log("You gto Fuel");
                break;
           default:
               Debug.Log("Too Bad");
               break;
       }
    }
}
