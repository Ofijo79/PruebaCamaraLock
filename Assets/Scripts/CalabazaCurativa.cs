using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalabazaCurativa : MonoBehaviour
{
    Curacion cura;

    // Start is called before the first frame update
    void Start()
    {
        cura = GameObject.Find("KenjiroIdle").GetComponent<Curacion>();
        cura.calabazaTexto.text = cura.contador.ToString();
        cura.ActualizarTexto();
    }

    // Método para sumar calabazas y actualizar el texto
    public void SumarCalabaza()
    {
        cura.contador++;
        cura.ActualizarTexto();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            SumarCalabaza();
            Destroy(this.gameObject);
        }
    }
}
