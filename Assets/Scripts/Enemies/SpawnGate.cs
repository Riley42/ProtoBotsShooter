using UnityEngine;
using System.Collections;

public class SpawnGate : MonoBehaviour
{
    [SerializeField] GameObject spiderDroidPrefab;
    [SerializeField] float spawnTime = 5f;
    [SerializeField] Transform spawnPoint;

    PlayerHealth player;
    private void Start()
    {
        player = FindFirstObjectByType<PlayerHealth>();
        StartCoroutine(SpawnRoutine());
    }
    IEnumerator SpawnRoutine()
    {
        yield return new WaitForSeconds(spawnTime);
        while (player)
        { 
            Instantiate(spiderDroidPrefab, spawnPoint.position, transform.rotation);
            yield return new WaitForSeconds(spawnTime);
        }
    }
}
