using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
               ReloadLevel();
               break;
       }
    }

    void ReloadLevel() {
        //This variable is the index of the current scene
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        //This loads the current scene again
        SceneManager.LoadScene(currentSceneIndex);
    }
}
