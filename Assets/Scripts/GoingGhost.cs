using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoingGhost : MonoBehaviour {

    [SerializeField] Material material;
    [SerializeField] float waitTime = 5;

    MeshRenderer meshRenderer;
    BoxCollider boxCollider;


    void Start () {
        meshRenderer = GetComponent<MeshRenderer>();
        boxCollider = GetComponent<BoxCollider>();
     }

    void OnTriggerEnter(Collider other)
    {
        gameObject.tag = "Enemy";
    }

    void OnTriggerExit(Collider other)
    {
        meshRenderer.material = material;
        StartCoroutine(wait(waitTime));
    }

    void DoAwake()  {

    }

    IEnumerator wait(float waitTim)    {
        yield return new WaitForSeconds(waitTim);
        boxCollider.isTrigger = false;
    }
}
