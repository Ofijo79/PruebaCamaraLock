using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Archer : MonoBehaviour
{
    public int maxHealth = 50;
    [SerializeField] float health = 50;
    float damageAmount = 10;

    public Slider healthSlider;
    GameObject player;

    bool isDead = false;
    float fadeDuration = 1.0f;
    Renderer renderer;

    public ParticleArcher desactivar;

    public GameObject deathParticlesObject;

    SFXManager sfx;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        renderer = GetComponent<Renderer>();
        sfx = GameObject.Find("SFX").GetComponent<SFXManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Katana")
        {
            TakeDamage();
            PushBack();
            sfx.SwordHit();
        }
    }

    void PushBack()
    {
        Vector3 pushDirection = transform.position - player.transform.position;
        pushDirection.Normalize();

        float pushForce = 50f;
        transform.position += pushDirection * pushForce * Time.deltaTime;
    }

    public void TakeDamage()
    {
        if (!isDead)
        {
            health -= damageAmount;
            UpdateHealthUI();
            if (health <= 0)
            {
                Die();
            }
        }
    }

    public void Die()
    {
        isDead = true;
        gameObject.SetActive(false);
        if (deathParticlesObject != null)
        {
            deathParticlesObject.SetActive(true); // Activa las partículas de muerte
            desactivar.StartCoroutine(desactivar.DeactivateAfterDelayCoroutine());
        }
    }

    void UpdateHealthUI()
    {
        healthSlider.value = health / maxHealth;
    }
}
