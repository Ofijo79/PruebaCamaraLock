using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Curacion : MonoBehaviour
{
    public Text calabazaTexto;
    public int contador = 1;

    // Start is called before the first frame update
    void Start()
    {
        calabazaTexto.text = contador.ToString();
        ActualizarTexto();
    }

    // Método para actualizar el texto del UI
    public void ActualizarTexto()
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
}
