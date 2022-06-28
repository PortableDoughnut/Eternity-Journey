using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoingGhost : MonoBehaviour
{
    MeshRenderer mesh;
    BoxCollider box;
    [SerializeField] private Material toRender;

    void Awake() {
        mesh = GetComponent<MeshRenderer>();
        box = GetComponent<BoxCollider>();
    }
    void Start()
    {
        EventManager.OnExitGhost += GoGhost;
        EventManager.OnRemoveTrigger += GoPhysical;
    }
    void OnDisable() {
        EventManager.OnExitGhost -= GoGhost;
        EventManager.OnRemoveTrigger -= GoPhysical;
    }
    void GoGhost() {
        if(gameObject.tag == "Enemy")
            mesh.material = toRender;
    }
    void GoPhysical() {
        if(gameObject.tag == "Enemy")
            box.isTrigger = false;
        else {
            mesh.enabled = false;
        }
    }
}
