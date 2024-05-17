using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class ParticleArcher : MonoBehaviour
{
    public Transform characterToFollow; // Referencia al personaje que seguirá el objeto vacío


    void Update()
    {
        if (characterToFollow != null)
        {
            // Establece la posición y la rotación del objeto vacío igual que el personaje
            transform.position = characterToFollow.position;
            transform.rotation = characterToFollow.rotation;
        }
    }

    public IEnumerator DeactivateAfterDelayCoroutine()
    {
        yield return new WaitForSeconds(5f); // Esperar 5 segundos

        // Desactivar este script después del retraso
        gameObject.SetActive(false);
    }
}
