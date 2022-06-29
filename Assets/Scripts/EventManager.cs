using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class EventManager : MonoBehaviour
{
    [SerializeField] int waitTime = 5;

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
        DoesFlip flip = other.gameObject?.GetComponent<DoesFlip>();
        if (other.gameObject.tag == "Finish" && flip.isFlip)
        {
            OnCollide?.Invoke();
            }
            else
            {
                return;
            }
    }

    public void DoPhysicalWait()
    {
        StartCoroutine(PhysicalWait());
    }

    IEnumerator PhysicalWait()
    {
        yield return new WaitForSeconds(waitTime);
    }
}
