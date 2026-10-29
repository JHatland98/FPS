using UnityEngine;
using UnityEngine.AI;
public class EnemyAI : MonoBehaviour
{

    [SerializeField] Transform target;

    [SerializeField] float chaseRange = 5.0f;

    float distanceToTarget = Mathf.Infinity;


    private bool isProvoked = false;

    NavMeshAgent nma;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        nma = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
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
         nma.SetDestination(target.position);
    }

    private void AttackTarget()
    {
        // temp for now to show enemy is attackgin
        print(name + " is attacking " + target.name);
    }

    void OnDrawGizmosSelected()
    {
        // displayer the chase radius when selected
        Gizmos.color = Color.red; // chosose a color
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}
