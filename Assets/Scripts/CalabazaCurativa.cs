using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalabazaCurativa : MonoBehaviour
{
    public Text calabazaTexto;
    private int contador = 1;

    // Start is called before the first frame update
    void Start()
    {
        ActualizarTexto();
    }

    // Método para sumar calabazas y actualizar el texto
    public void SumarCalabaza()
    {
        contador++;
        ActualizarTexto();
    }

    // Método para actualizar el texto del UI
    private void ActualizarTexto()
    {
        calabazaTexto.text = contador.ToString();
    }

    public void RestarCalabaza()
    {
        if (contador > 0)
        {
            contador--;
            ActualizarTexto();
        }
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
