using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float damagePoint;

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            Debug.Log("Chomper Hit!");
            EnemyHealth e = other.GetComponent<EnemyHealth>();
            e.TakeDamage(damagePoint);
        }
    }
}
