using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class EventManager : MonoBehaviour
{

    [SerializeField] private UnityEvent OnAwake;
    [SerializeField] private UnityEvent OnExit;
    [SerializeField] private UnityEvent OnEnter;
    [SerializeField] private UnityEvent OnCollide;
    
    void Awake() {
        OnAwake.Invoke();
    }

    void OnTriggerEnter(Collider other) {
        OnEnter.Invoke();
    }

    void OnTriggerExit(Collider other) {
        OnExit.Invoke();
    }

    void OnCollisionEnter(Collision other) {
        OnCollide.Invoke();
    }

    public void DoPhysicalWait()
    {
        StartCoroutine(PhysicalWait());
    }

    IEnumerator PhysicalWait()
    {
        yield return new WaitForSeconds(5);
    }
}
