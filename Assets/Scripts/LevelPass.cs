using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPass : MonoBehaviour
{
    MenuManagement lvlPass;
    // Start is called before the first frame update
    void Start()
    {
        lvlPass = GameObject.Find("MenuManagement").GetComponent<MenuManagement>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            lvlPass.Nivel2();
        }
    }
}
