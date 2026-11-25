using UnityEngine;
using UnityEngine.AI;
public class EnemyAI : MonoBehaviour
{

    [SerializeField] Transform target;

    [SerializeField] float chaseRange = 5.0f;

    [SerializeField] float turnSpeed = 5.0f;

    float distanceToTarget = Mathf.Infinity;


    private bool isProvoked = false;

    NavMeshAgent nma;

    EnemyHealth health;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
        nma = GetComponent<NavMeshAgent>();

        health = GetComponent<EnemyHealth>();
    }

    // Update is called once per frame
    void Update()
    {
         if(health.isDead)
        {
            enabled = false;

            nma.enabled = false;

            return;
        }
        distanceToTarget = Vector3.Distance(target.position, transform.position);


        if(isProvoked)
        {
            EngageTarget(); // No defined yet
        }
        else if(distanceToTarget <= chaseRange)
        {
            isProvoked = true;
        }


       


    }

    private void EngageTarget()
    {
        FaceTarget();

        if(distanceToTarget >= nma.stoppingDistance)
        {
            ChaseTarget(); 
        }
        if(distanceToTarget <= nma.stoppingDistance)
        {
            AttackTarget(); // not defined yet
        }
    }

    private void ChaseTarget()
    {
        GetComponent<Animator>().SetBool("attack", false);

        GetComponent<Animator>().SetTrigger("run");

         nma.SetDestination(target.position);
    }

    private void AttackTarget()
    {
        GetComponent<Animator>().SetBool("attack", true);


        // temp for now to show enemy is attackgin
        // print(name + " is attacking " + target.name);
    }

        private void FaceTarget()
        {
            Vector3 direction = (target.position - transform.position).normalized;

            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));

            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
        }

    public void OnDamageTaken()
    {
        isProvoked = true;
    }

    void OnDrawGizmosSelected()
    {
        // displayer the chase radius when selected
        Gizmos.color = Color.red; // chosose a color
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}
