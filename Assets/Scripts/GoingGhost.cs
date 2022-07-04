using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoingGhost : MonoBehaviour {

    //This is the material that the ghost will turn into.
    [SerializeField] Material material;
    //This is the time that the script will wait for before making the ghost physical.
    [SerializeField] float waitTime = 5;

    //This is the Mesh Renderer that is attached to the GameObject.
    MeshRenderer meshRenderer;
    //This is the Box Collider that is attached to the GameObject.
    BoxCollider boxCollider;

    //This starts before the game only once
    void Awake() {
        //This is setting the meshRenderer value to the Mesh Renderer attached to the GameObject.
        meshRenderer = GetComponent<MeshRenderer>();
        //This is setting the boxCollider value to the Box Collider attached to the GameObject.
        boxCollider = GetComponent<BoxCollider>();
     }

    //This is called when something collides with the GameObject.
    void OnTriggerEnter(Collider other)
    {
        //This sets the tag of the GameObject to "Enemy".
        gameObject.tag = "Enemy";
    }

    //This is called when something stops colliding with the GameObject.
    void OnTriggerExit(Collider other)
    {
        //This sets the material of the GameObject to the material value.
        meshRenderer.material = material;
        //This starts the coroutine that waits for the amount of time specified and then makes the GameObject physical.
        StartCoroutine(wait(waitTime));
    }

    /*
     * This will wait for the time specified in the waitTim param and then will set the Box Collider trigger boolean to false.
     * I used an IEnumerator so that it would wait for the other methods to finish and so that I could add the WaitForSeconds() function.
     */
    IEnumerator wait(float waitTim)    {
        yield return new WaitForSeconds(waitTim);
        boxCollider.isTrigger = false;
    }
}
