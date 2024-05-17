using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 5;

    SFXManager sfx;
    // Start is called before the first frame update
    void Awake()
    {
        sfx = GameObject.Find("SFX").GetComponent<SFXManager>();
    }

    void Start()
    {
        sfx.FireBall();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * bulletSpeed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider collision)
    {
        Destroy(gameObject);
    }
}
