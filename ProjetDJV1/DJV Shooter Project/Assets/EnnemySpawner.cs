using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemySpawner : MonoBehaviour
{
    [SerializeField] private Vector3[] spawnPositions;
    [SerializeField] private EnemyIa ennemyPrefab;
    [SerializeField] private float cooldown;
    [SerializeField] private Transform playerTransformReference;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnCoroutine()); 
    }

    private IEnumerator SpawnCoroutine()
    {
        yield return new WaitForSeconds(cooldown);
        EnemyIa ennemy = Instantiate(ennemyPrefab, spawnPositions[Random.Range(0, spawnPositions.Length)], Quaternion.identity);
        ennemy.playerTransformReference = playerTransformReference;
    }


}
