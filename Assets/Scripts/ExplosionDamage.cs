using UnityEngine;

public class ExplosionDamage : MonoBehaviour
{

    [SerializeField] float radius = 5f;
    private void Start()
    {
        Explode();
    }
    
    void Explode()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
        
        foreach (Collider hit in hitColliders)
        {
            PlayerHealth player = hit.GetComponent<PlayerHealth>();
            if (!player) continue;

            player.TakeDamage(4);
            break;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

}
