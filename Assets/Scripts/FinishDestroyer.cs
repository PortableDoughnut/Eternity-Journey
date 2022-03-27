using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishDestroyer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void onCollisionEnter(Collision other) {
        if(other.gameObject.tag != "Player") {
            Destroy(other.gameObject);
        }
    }
}
