using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health = 15f;
    public Animator animator;
    public EnemyController controller;
    public HumanoidEnemyController HumanoidEnemyController;

    public void TakeDamage(float amount)
    {
        if (HumanoidEnemyController!=null)
        {
            health -= amount;
            if(health <= 0)
            {
                animator.SetBool("Die", true);
                HumanoidEnemyController.enabled = false;
                HumanoidEnemyController.anim.enabled=false;
            
                GameObject.Destroy(this.gameObject, 0.5f);
                Debug.Log("Enemy dead!");
            }
            else
            {
                animator.SetTrigger("GetHit");
            }
        }
        if (controller!=null)
        {
            health -= amount;
            if (health <= 0)
            {
                animator.SetBool("Die", true);
                controller.enabled = false;
                controller.anim.enabled = false;

                GameObject.Destroy(this.gameObject, 0.5f);
                Debug.Log("Enemy dead!");
            }
            else
            {
                animator.SetTrigger("GetHit");
            }
        }

        Debug.Log("Enemy take some damage!");
    }
}
