using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManagement : MonoBehaviour
{
    SoundManager sound;
    // Start is called before the first frame update
    void Start()
    {
        sound = GameObject.Find("Sonidos").GetComponent<SoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(4);
        sound.StopBGM();
    }

    public void Nivel2()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(2);
    }

    public void DeathScreenLvl1()
    {
        SceneManager.LoadScene(3);
    }

    public void DeathScreenLvl2()
    {
        SceneManager.LoadScene(5);
    }

    public void ThanksForPlaying()
    {
        SceneManager.LoadScene(6);
    }
}
