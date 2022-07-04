using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    //This is the position that the object starts at.
    Vector3 startingPosition;
    //This is how much you want to GameObject to move.
    [SerializeField] Vector3 movementVector;
    //This is the how close the GameObject is to the starignPosition and the destination.
    float movementFactor;
    //This is the amount of the you want it to take for the GameObject to move.
    [SerializeField] float period = 2f;
    //Constant value. It's Pi * 2. Used in Sin. The amount of the radius of a circle that is in the circumference of the circle. 6.283...
    const float tau = Mathf.PI * 2;

    // Start is called before the first frame update
    void Start()
    {
        //This sets the startingPosition variable to the current position of the object.
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        /*
         * This checks if period is greater than the number Mathf.Epsilon.
         * If if is than it runs the Oscillate() method.
         * 
         * Mathf.Epsilon is the smallest a float can be.
         * We should always use this instead of 0 when unisng floats.
         */
        if(period > Mathf.Epsilon)
            Oscillate();
    }

    //This is the method that makes the GameObect go back and forth.
    void Oscillate() {
        //This is saying that the time that has passed is devied by how long we want it to take.
        float cycles = Time.time / period;
        //Going from -1 to 1. Did this by using Sin, which I'm pretty sure is going up and down of a circumference of a circle. Sin goes from -1 to 1. Added 1 so that it goes from 0 to 2. Then I divide that by 2 so it goes from 0 to 1.
        float rawSinWave = Mathf.Sin(cycles * tau);
        //This puts all the previous calculations within the values of the movementFactor variable.
        movementFactor = (rawSinWave + 1f) / 2f;
    
        /*
         * This sets the offest to the movementVector * movementFactor.
         * This will be used to move the GameObejct.
         */
        Vector3 offset = movementVector * movementFactor;
        //This adds the offest to the startingPosition and sets it to the GameObject to move it.
        transform.position = offset + startingPosition;
    }
}
