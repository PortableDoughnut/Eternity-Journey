using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishDestroyer : MonoBehaviour
{
    //This is called when the GameObject has collision with something.
    private void OnCollisionEnter(Collision collision)  {
        //If the tag of the other object is set to player the method is not ran.
        if (collision.gameObject.tag == "Player")
            return;

        //This destroys the object it is touching.
        Destroy(collision.gameObject);
    }
}
