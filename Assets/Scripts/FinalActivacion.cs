using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalActivacion : MonoBehaviour
{
    public GameObject Final;
    public GameObject obstruccion;

    Enemy vida;

    // Start is called before the first frame update
    void Start()
    {
        vida = GameObject.Find("TenguFinal").GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        SiguienteNivel();
    }

    void SiguienteNivel()
    {
        if(vida.isDead == true)
        {
            obstruccion.SetActive(false);
            Final.SetActive(true);
        }
    }
}
