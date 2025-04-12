using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int startingHealth = 3;
    int currentHealth;

    [SerializeField] GameObject liveModel;
    [SerializeField] GameObject explodedModel;
    [SerializeField] GameObject explosionVFX;

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
            ExplodeAndDie();
        }
    }

    public void ExplodeAndDie(float explosionForce = 100f, float explosionRadius = 5f, float freezeDelay = 2f)
    {
        /*#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPaused = true;
        #endif
        */
        var enemyCenter = transform.position + Vector3.up * 1.0f;           

        foreach (Collider col in GetComponentsInParent<Collider>())
        {
            col.enabled = false;
        }

        var goVFX = Instantiate(explosionVFX, enemyCenter, Quaternion.identity);
        Destroy(goVFX.gameObject, 10f);

        if (liveModel != null && explodedModel != null)
            ExplodeModel(explosionForce, explosionRadius, freezeDelay, enemyCenter);
        else
            Destroy(this.gameObject);

    }

    private void ExplodeModel(float explosionForce, float explosionRadius, float freezeDelay, Vector3 enemyCenter)
    {

        enemy.HandleDeath();
        liveModel.SetActive(false);
        explodedModel.SetActive(true);


        foreach (Rigidbody rb in explodedModel.GetComponentsInChildren<Rigidbody>())
        {
            rb.isKinematic = false;
            rb.detectCollisions = true;
            rb.AddExplosionForce(explosionForce, enemyCenter, explosionRadius, 1.5f);
        }


        StartCoroutine(DisableRigidbodiesAfterDelay(freezeDelay));
    }

    private IEnumerator DisableRigidbodiesAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        foreach (Rigidbody rb in explodedModel.GetComponentsInChildren<Rigidbody>())
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true;
            rb.detectCollisions = false; // Optional, if you want the body parts to be ghost-like
        }
    }


}

