using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    
    private bool isPaused = false;
    public GameObject canva;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Pause"))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            canva.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            canva.SetActive(false);
            Time.timeScale = 1f;
        }
    }

}
