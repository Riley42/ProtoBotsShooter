using System.Collections;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] Transform turretHead;
    [SerializeField] Transform targetPoint;
    [SerializeField] Transform spawnPoint;
    [SerializeField] GameObject projectilePrefab;

    PlayerHealth player;
    int damage = 2;
    float fireRate = 2f;


    private void Start()
    {
        player = FindFirstObjectByType<PlayerHealth>();
        StartCoroutine(FireRoutine());
    }

    private void Update()
    {
        turretHead.LookAt(targetPoint);
    }

    IEnumerator FireRoutine()
    {
        while (player)
        {
            yield return new WaitForSeconds(fireRate);
            Projectile projectile = Instantiate(projectilePrefab, spawnPoint.position, Quaternion.identity).GetComponent<Projectile>();
            projectile.transform.LookAt(targetPoint);
            projectile.OnSpawn(damage);
        }
    }
}
