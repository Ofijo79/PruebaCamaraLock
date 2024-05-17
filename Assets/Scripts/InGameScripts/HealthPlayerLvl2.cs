using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthPlayerLvl2 : MonoBehaviour
{
    public int maxHealth = 100;
    [SerializeField]float health = 100;
    float damageAmount = 10;
    float damageAmountBullet = 20;
    //float damageAmountBlocking = 5;
    //float damageAmountBulletBlocking = 10;

    public Slider healthSlider;

    Isometriccontroller dash;

    MenuManagement death;

    SFXManager sfx;

    Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        dash = GetComponent<Isometriccontroller>();
        death = GameObject.Find("MenuManagement").GetComponent<MenuManagement>();
        sfx = GameObject.Find("SFX").GetComponent<SFXManager>();
        _animator = GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider other) 
    { 
        if(other.gameObject.tag == "Arma" && dash.isDashing == false && dash.isBlocking == false)
        {     
            GetDamage();
        }
        
        if(other.gameObject.tag == "Bullet" && dash.isDashing == false && dash.isBlocking == false)
        {     
            GetDamageBullet();
        }
        if(other.gameObject.tag == "Arma" && dash.isBlocking == true)
        {     
            GetDamageBlocking();
        }
        
        if(other.gameObject.tag == "Bullet" && dash.isBlocking == true)
        {     
            GetDamageBulletBlocking();
        }
    }

    public void GetDamageBulletBlocking()
    {
        health -= damageAmountBullet / 2;
        ActualHealth();
        if(health <= 0)
        {
            Die();
        }
    }

    public void GetDamageBlocking()
    {
        health -= damageAmount / 2;
        sfx.GetHit();
        ActualHealth();
        if(health <= 0)
        {
            Die();
        }
    }
    public void GetDamageBullet()
    {
        health -= damageAmountBullet;
        ActualHealth();
        if(health <= 0)
        {
            Die();
        }
    }

    public void GetDamage()
    {
        health -= damageAmount;
        sfx.GetHit();
        ActualHealth();
        if(health <= 0)
        {
            Die();
            sfx.DeathSound();
        }
    }

    void Die()
    {
        dash.enabled = false;
        _animator.SetBool("IsDead", true);
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.enabled = false;
        }

        StartCoroutine(DeathRoutine());
    }

    IEnumerator DeathRoutine()
    {
        // Espera 2 segundos
        yield return new WaitForSeconds(4f);

        // Reproduce el sonido de muerte
        sfx.DeathSound();

        // Espera un corto tiempo adicional si es necesario
        yield return new WaitForSeconds(0.5f); // Por ejemplo, espera 0.5 segundos más

        // Muestra la pantalla de muerte
        death.DeathScreenLvl2();
    }
    
    void ActualHealth()
    {
        healthSlider.value = health / maxHealth;
    }
}
