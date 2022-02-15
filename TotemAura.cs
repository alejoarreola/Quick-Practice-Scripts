using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotemAura : MonoBehaviour
{
    //particle effect here[SerializeField] private GameObject ParticleName;
    bool damaging = false;
    void Update()
    {
        CheckForPlayerColliders();
        CheckForBossColliders();
    } 

    //next step: add a stop to damage and healing when player is no longer in damage area
    private void CheckForPlayerColliders() //checks for colliders within circumference
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 5f);
        foreach (Collider c in colliders)
        {
            if (c.CompareTag("Player")) //checks for the "Playher" tag and, if found, calls DOT and Dmg Flash
            {
                damaging = true;
                c.GetComponent<HealthManager>().DamageOverTime(10, 5);
                c.GetComponent<HealthManager>().DamageFlash();
            }
        }
    }
    
    private void CheckForBossColliders()
    {
        Collider[] bossColliders = Physics.OverlapSphere(transform.position, 20f);
        foreach (Collider b in bossColliders)
        {
            if (damaging == true && b.CompareTag("Boss"))
            {
                b.GetComponent<HealthManager>().HealOverTime(5, 5);
                b.GetComponent<HealthManager>().HealFlash();
            }
        }
    }
}
