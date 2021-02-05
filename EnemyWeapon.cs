using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    public BoxCollider collider;
    public float damagePoint = 10f;

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Shield"||other.tag=="Weapon")
        {
            collider.isTrigger = false;
            return;
        }
        if (other.tag == "Player")
        {
            Debug.Log("AAAAA!");
            PlayerHealth ph = other.GetComponent<PlayerHealth>();
            ph.TakeDamage(damagePoint);
        }       
    }

    
}
