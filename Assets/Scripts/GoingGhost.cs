using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoingGhost : MonoBehaviour
{
    public void DoPhysicalWait() {
        StartCoroutine(PhysicalWait());
    }

    IEnumerator PhysicalWait() {
        yield return new WaitForSeconds(5);
    }
}
