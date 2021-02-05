using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public Animator animator;
    public BoxCollider weaponCol;

    //float timer;

    // Update is called once per frame
    void Update()
    {
        PlayerAttack();
    }

    void PlayerAttack()
    {
        animator.SetFloat("MouseButton0", Input.GetAxisRaw("Fire1"));
        animator.SetFloat("MouseButton1", Input.GetAxisRaw("Fire2"));


        //if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        //{
        //    timer = animator.GetCurrentAnimatorStateInfo(0).length - 0.55f;
        //    weaponCol.enabled = true;
        //}
        //timer -= Time.deltaTime;
        //if (timer <= 0)
        //{
        //    weaponCol.enabled = false;
        //}
    }

    void EnableCollider()
    {
        weaponCol.enabled = true;
    }
    void DisableCollider()
    {
        weaponCol.enabled = false;
    }

}
