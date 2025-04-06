using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int startingHealth = 3;
    int currentHealth;

    [SerializeField] GameObject liveModel;
    [SerializeField] GameObject explodedModel;
    [SerializeField] GameObject explostionVFX;

    Enemy enemy;
    public int CurrentHealth { get { return currentHealth; } }

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
    }
    private void Start()
    {
        currentHealth = startingHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Explode();
        }
    }

    public void Explode()
    {
        enemy.Agent.SetDestination(transform.position);
        enemy.Agent.speed = 0;
        var go = Instantiate(explostionVFX, transform.position, Quaternion.identity);
        liveModel.SetActive(false);
        explodedModel.SetActive(true);
        Destroy(go.gameObject, 10f);
    }
}

