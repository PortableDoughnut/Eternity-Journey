using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
Vector3 startingPosition;
[SerializeField] Vector3 movementVector;
float movementFactor;
[SerializeField] float period = 2f;
//Constant value. It's Pi * 2. Used in Sin. The amount of the radius of a circle that is in the circumference of the circle. 6.283...
const float tau = Mathf.PI * 2;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(period > Mathf.Epsilon)
            Oscillate();
    }

    void Oscillate() {
        float cycles = Time.time / period;
        //Going from -1 to 1. Did this by using Sin, which I'm pretty sure is going up and down of a circumference of a circle. Sin goes from -1 to 1. Added 1 so that it goes from 0 to 2. Then I divide that by 2 so it goes from 0 to 1.
        float rawSinWave = Mathf.Sin(cycles * tau);
        movementFactor = (rawSinWave + 1f) / 2f;

        Vector3 offset = movementVector * movementFactor;
        transform.position = offset + startingPosition;
    }
}
