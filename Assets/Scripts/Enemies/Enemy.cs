using StarterAssets;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    NavMeshAgent agent;
    FirstPersonController player;
    Animator anim;
    public NavMeshAgent Agent { get { return agent; } }
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }


    void Start()
    {
        player = FindFirstObjectByType<FirstPersonController>();
    }


    void Update()
    {
        if (!player) return;

        agent.SetDestination(player.transform.position);
        transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));

        Vector3 velocity = (agent.velocity).normalized;
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);

        anim.SetFloat("moveX", localVelocity.x);
        anim.SetFloat("moveZ", localVelocity.z);

        if (velocity.magnitude == 0)
        {
            anim.SetFloat("moveX", 0);
            anim.SetFloat("moveZ", 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            this.GetComponent<EnemyHealth>().Explode();
        }
    }
}
