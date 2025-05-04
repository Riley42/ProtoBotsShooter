using Cinemachine;
using StarterAssets;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int startingHealth = 6;
    [SerializeField] CinemachineVirtualCamera deathCamera;
    [SerializeField] Transform weaponCamera;
    [SerializeField] Image[] shieldBars;
    [SerializeField] GameObject gameOverUI;

    int currentHealth;
    int deathCameraPriority = 20;
    
    private void Start()
    {
        currentHealth = startingHealth;
        UpdateShieldBars();
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        UpdateShieldBars();

        if (currentHealth <= 0)
        {
            weaponCamera.parent = null;
            deathCamera.Priority = deathCameraPriority;
            gameOverUI.SetActive(true);

            StarterAssetsInputs starterAssetsInput = FindFirstObjectByType<StarterAssetsInputs>();
            starterAssetsInput.SetCursorState(false);
            Destroy(this.gameObject);
        }
    }

    void UpdateShieldBars()
    {
        for (int i = 0; i < shieldBars.Length; i++)
        {
            if (i < currentHealth)
            {
                shieldBars[i].gameObject.SetActive(true);
            }
            else
            {
                shieldBars[i].gameObject.SetActive(false);
            }
        }
    }
}
