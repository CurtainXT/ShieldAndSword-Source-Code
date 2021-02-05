using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    //public float lookRadius = 10f;//怪物探测范围
    public float followRadius = 20f;//怪物活动范围
    public GameObject player;  //拖入player模型
    public GameObject model; // 拖入怪物模型  
    public CapsuleCollider Body; // 碰撞器

    public ChomerEnemyWeapon m_ChomerEnemyWeapon;
    public TargetScanner targetScanner;
    
    public GameObject BasePos; // 怪物初始点

    public Vector3 NowPosition;  //怪物现在的位置
    public Vector3 OriginPosition;//怪物初始位置


    public Animator anim; 
    Transform target;//目标
    NavMeshAgent agent;



    // Start is called before the first frame update
    void Start()
    {
       
        OriginPosition = BasePos.transform.position;
        //Debug.Log("OriginPosition"+OriginPosition);
        anim = model.GetComponent<Animator>();
        target = player.transform;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
       // Debug.Log(target.position);

        bool findTarget=targetScanner.Detect(transform,player.transform);
        NowPosition = transform.position;
        float distance_follow= Vector3.Distance(target.position,OriginPosition);//player与怪物初始位置的距离
        float distance_look = Vector3.Distance(target.position, transform.position);//怪物与player的距离

        if (findTarget && distance_follow <= followRadius)
        {
            anim.SetTrigger("Spotted");
            anim.SetBool("Grounded", true);
            anim.SetBool("InPursuit", true);
            //if(distance_look<=1.8){
                FaceTarget();
                agent.SetDestination(target.position);// 追踪目标
            //}

        }
        else
        {
            anim.SetBool("InPursuit", false);
            agent.SetDestination(OriginPosition);//怪物回到初始位置 
            Vector3 toBase = OriginPosition - transform.position;
            toBase.y = 0;
            anim.SetBool("NearBase", toBase.sqrMagnitude < 2 * 2f);
        }

        if((player.transform.position-this.transform.position).sqrMagnitude<5){
            anim.SetTrigger("Attack");
        }


    }

#if UNITY_EDITOR
    void OnDrawGizmosSelected() //显示两个范围
    {
        targetScanner.EditorGizmo(transform);
       // Gizmos.color = Color.red;
       // Gizmos.DrawWireSphere(transform.position, lookRadius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(OriginPosition, followRadius);
        
    }
#endif

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void AttackDetect()


    {

    }



    //Animation Event 
    public void AttackStart(){
        m_ChomerEnemyWeapon.collider.enabled=true;
    }

    //Animation Event 
    public void AttackEnd(){
        m_ChomerEnemyWeapon.collider.enabled=false;

    }
}


