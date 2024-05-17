using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResistencePlayer : MonoBehaviour
{

    public float resistenceInitial = 100f;
    public float velocityRunning = 1f;
    public float resistenceXHit = 10f;
    public Slider sliderResistence;

    public float resistenceXDash = 25f;

    public float resistanceXJump = 25f;

    public float actualResistance;

    public float velocityRegeneration = 30f;

    private float tiemeWithoutActivity;

    public float tiemeOfInactivity = 2f;

    public float baseVelocityRegeneration = 1.0f;
    
    public float enhancedVelocityRegeneration = 2.0f;

    Isometriccontroller run;
    // Start is called before the first frame update
    void Start()
    {
        actualResistance = resistenceInitial;
        ActualizeResistance();
        run = GameObject.Find("KenjiroIdle").GetComponent<Isometriccontroller>();
    }

    // Update is called once per frame
    void Update()
    {
        MovementRes();

        RestartResistance();
    }

    public void takeResistance()
    {
        actualResistance -= resistenceXHit;
        actualResistance = Mathf.Max(0, actualResistance);
        ActualizeResistance();
        ResetTime();
    }
    
    public void dashResistance()
    {
        actualResistance -= resistenceXDash;
        actualResistance = Mathf.Max(0, actualResistance);
        ActualizeResistance();
        ResetTime();
    }

    public void jumpResistance()
    {
        actualResistance -= resistanceXJump;
        actualResistance = Mathf.Max(0, actualResistance);
        ActualizeResistance();
        ResetTime();
    }

    void ActualizeResistance()
    {
        sliderResistence.value = actualResistance / resistenceInitial;
    }

    public void MovementRes()
    {
        if (Input.GetKey(KeyCode.LeftShift) && actualResistance > 0 && run.direction != Vector3.zero)
        {
            actualResistance -= velocityRunning * Time.deltaTime;
            ActualizeResistance();
            ResetTime();
        }
    }

    void RestartResistance()
    {
        if (actualResistance < resistenceInitial)
        {
            tiemeWithoutActivity += Time.deltaTime;
            if (tiemeWithoutActivity >= tiemeOfInactivity)
            {
                // Usa la velocidad base de regeneración como valor predeterminado
                float regenerationVelocity = baseVelocityRegeneration;

                // Si la resistencia está por debajo de su valor inicial, aumenta la velocidad de regeneración
                if (actualResistance < resistenceInitial)
                {
                    regenerationVelocity = enhancedVelocityRegeneration;
                }

                actualResistance += regenerationVelocity * Time.deltaTime;
                actualResistance = Mathf.Min(actualResistance, resistenceInitial);
                ActualizeResistance();
            }
        }
    }

    void ResetTime()
    {
        tiemeWithoutActivity = 0.5f;
    }
}
