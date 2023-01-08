using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
            QuitApplication();
    }

    //Quits Game
    private void QuitApplication()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }
}
