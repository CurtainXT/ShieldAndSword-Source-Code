using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HumanoidEnemyController : MonoBehaviour
{
    //public float lookRadius = 10f;//怪物探测范围
    public float followRadius = 20f;//怪物活动范围
    public GameObject player;  //拖入player模型
    public GameObject model; // 拖入怪物模型  
    public CapsuleCollider Body; // 碰撞器
    public PlayerHealth playerHealth;

    public BoxCollider collider;
    public GameObject BasePos; // 怪物初始点


    public TargetScanner targetScanner;//扫描器

    public Vector3 NowPosition;  //怪物现在的位置
    public Vector3 OriginPosition;//怪物初始位置

    public float distance_look;
    public float distance_follow;

    public float stopDistance;

    public Animator anim; 
    Transform target;//目标
    NavMeshAgent agent;
    bool isAttacking;
    bool faceTarget;

    // Start is called before the first frame update
    void Start()
    {
       
        OriginPosition = BasePos.transform.position;
        anim = model.GetComponent<Animator>();
        target = player.transform;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {

        bool findTarget = targetScanner.Detect(transform, player.transform);//扫描目标


        NowPosition = transform.position;
        float distance_follow= Vector3.Distance(target.position,OriginPosition);//player与怪物初始位置的距离
        float distance_look = Vector3.Distance(target.position, transform.position);//怪物与player的距离
        anim.SetFloat("Attack", distance_look);
        //Debug.Log(distance_look);
        if (findTarget&& distance_follow <= followRadius)
        {
            anim.SetBool("InPursuit", true);
            agent.SetDestination(target.position);// 追踪目标
            Debug.Log(isAttacking);
            if (distance_look <= stopDistance || isAttacking)
            {
                if(faceTarget)
                    FaceTarget();
                agent.SetDestination(transform.position);
            }
        }
        else
        {
            anim.SetBool("InPursuit", false);
            agent.SetDestination(OriginPosition);//怪物回到初始位置 
            Vector3 toBase = OriginPosition - transform.position;
            toBase.y = 0;
            anim.SetBool("NearBase", toBase.sqrMagnitude < 2 * 2f);
        }

        if(playerHealth.isDead)
        {
            agent.SetDestination(OriginPosition);//怪物回到初始位置 
            anim.SetFloat("Attack", 10f);
        }
    }

    void OnDrawGizmosSelected() //显示两个范围
    {
        targetScanner.EditorGizmo(transform);
        //Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(transform.position, lookRadius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(OriginPosition, followRadius);
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void EnableCollider()
    {
        collider.enabled = true;
    }
    void DisableCollider()
    {
        collider.enabled = false;
    }

    void FaceToTarget()
    {
        faceTarget = true;
    }

    void StopFaceToTarget()
    {
        faceTarget = false;
    }

    void StandStill()
    {
        isAttacking = true;
    }
    void Chase()
    {
        isAttacking = false;
    }
}

