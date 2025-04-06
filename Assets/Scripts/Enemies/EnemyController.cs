using StarterAssets;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    FirstPersonController player;
    NavMeshAgent agent;
    Animator anim;


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }
    void Start()
    {
        player = FindFirstObjectByType<FirstPersonController>();
    }

    private void Update()
    {
        agent.SetDestination(player.transform.position);
        transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));

        // Get the agent's velocity (movement vector)
        Vector3 velocity = agent.velocity;

        // Normalize the velocity to avoid large values (you want direction, not magnitude)
        velocity = velocity.normalized;

        // Transform the velocity from world space to local space (relative to the character)
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);

        // Set the animation parameters based on the local velocity
        anim.SetFloat("moveX", localVelocity.x);
        anim.SetFloat("moveZ", localVelocity.z);

        // If there's no movement, stop the animation
        if (velocity.magnitude == 0)
        {
            anim.SetFloat("moveX", 0);
            anim.SetFloat("moveZ", 0);
        }
    }

    void OnDrawGizmos()
    {
        if (agent != null && agent.hasPath)
        {
            // Set Gizmo color
            Gizmos.color = Color.blue;

            // Draw a line from the enemy to the destination
            Gizmos.DrawLine(transform.position, agent.destination);

            // Draw a sphere at the destination
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(agent.destination, 0.3f);
        }
    }
}
