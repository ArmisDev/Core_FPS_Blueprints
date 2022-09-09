using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{

#region - Variables - 
    [SerializeField] public Transform target;
    [SerializeField] private float chaseRange = 5f;
    [SerializeField] private float turnSpeed = 5f;

    // public AudioClip zombieChase;
    // public AudioClip zombieAttack;
    // public AudioSource zombieAudioSource;

    [SerializeField] public bool enemyIsDead = false;

    UnityEngine.AI.NavMeshAgent navMeshAgent;
    float distanceToTarget = Mathf.Infinity;
    bool isProvoked = false;

#endregion


    void Start()
    {
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        GetComponent<EnemyHealth>();
        //zombieAudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        MainAiBehavior();
        DeathCheckFinder();
    }

#region - AI Behavior -

    public void MainAiBehavior()
    {
        distanceToTarget = Vector3.Distance(target.position, transform.position);

        if (isProvoked && !enemyIsDead)
        {
            FaceTarget();
            EngageTarget();
        }

        else if (distanceToTarget <= chaseRange)
        {
            isProvoked = true;
        }

        else
        {
            return;
        }
    }

    public void OnDamageTaken()
    {
        isProvoked = true;
    }

    private void EngageTarget()
    {

        if (distanceToTarget >= navMeshAgent.stoppingDistance)
        {
            ChaseTarget();
            //zombieAudioSource.PlayOneShot(zombieChase);
        }
            
        if (distanceToTarget <= navMeshAgent.stoppingDistance)
        {
            AttackTarget();
            //zombieAudioSource.PlayOneShot(zombieAttack);
        }
    }

    private void ChaseTarget()
    {
        //Debug.Log(name + " is chasing" + target.name);
        GetComponent<Animator>().SetBool("attack", false);
        GetComponent<Animator>().SetTrigger("move");
        navMeshAgent.SetDestination(target.position);
    }

    private void AttackTarget()
    {
        //Debug.Log(name + " has seeked and is destroying" + target.name);
        GetComponent<Animator>().SetBool("attack", true);
    }

    private void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }

#endregion

    public void DeathCheckFinder()
    {
        EnemyHealth grabber = GetComponent<EnemyHealth>();
        grabber.DeathCheck(enemyIsDead);

        if (enemyIsDead)
        {
            GetComponent<Animator>().ResetTrigger("move");
            navMeshAgent.enabled = false;
            chaseRange = 0;
            Debug.Log("Enemy is Dead");
        }

        else
        {
            return;
        }
    }

#region - Gizmos -

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }

#endregion
}
