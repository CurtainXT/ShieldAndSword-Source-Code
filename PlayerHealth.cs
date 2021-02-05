using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float health = 100f;
    public Animator animator;
    public bool isDead; //给敌人看的

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            animator.SetBool("Die", true);
            GameObject.Destroy(this.gameObject, 3f);
            Debug.Log("Player dead!");
            isDead = true;
        }
        else
        {
            animator.SetTrigger("GetHit");
        }
        Debug.Log("We got some damage!");
    }
}

