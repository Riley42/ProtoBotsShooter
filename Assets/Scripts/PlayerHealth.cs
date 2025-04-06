using Cinemachine;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int startingHealth = 3;
    [SerializeField] CinemachineVirtualCamera deathCamera;
    [SerializeField] Transform weaponCamera;
    int currentHealth;
    int deathCameraPriority = 20;
    
    private void Start()
    {
        currentHealth = startingHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            weaponCamera.parent = null;
            deathCamera.Priority = deathCameraPriority;
            Destroy(this.gameObject);
        }
    }
}
