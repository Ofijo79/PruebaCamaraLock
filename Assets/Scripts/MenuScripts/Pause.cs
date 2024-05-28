using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Pause : MonoBehaviour
{
    private bool isPaused = false;
    public GameObject canva;
    public GameObject Resume;

    void Start()
    {
        Time.timeScale = 1f;
        Cursor.visible = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Pause") || Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Cursor.visible = true;
            canva.SetActive(true);
            Time.timeScale = 0f;
            EventSystem.current.SetSelectedGameObject(Resume);
        }
        else
        {
            canva.SetActive(false);
            Time.timeScale = 1f;
            Cursor.visible = false;
            EventSystem.current.SetSelectedGameObject(null);
        }
    }

}
