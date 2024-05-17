using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivationBoss : MonoBehaviour
{
    EnemyBoss enemyScriptObject;
    public GameObject vidaBoss;
    BoxCollider bloqueo;
    BossBattle sfx;
    SoundManager music;
    // Start is called before the first frame update
    void Start()
    {
        enemyScriptObject = GameObject.Find("Oni").GetComponent<EnemyBoss>();
        bloqueo = GameObject.Find("Bloqueo").GetComponent<BoxCollider>();
        sfx = GameObject.Find("BattleBoss").GetComponent<BossBattle>();
        music = GameObject.Find("Music").GetComponent<SoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            enemyScriptObject.enabled = true;
            vidaBoss.SetActive(true);
            bloqueo.enabled = true;
            music.StopBGM();
            sfx.StartBattle();
        }
    }
}
