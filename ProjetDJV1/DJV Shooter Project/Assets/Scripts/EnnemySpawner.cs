using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemySpawner : MonoBehaviour
{
    [SerializeField] private Vector3[] spawnPositions;
    [SerializeField] private EnemyIaCommon shootingIa;
    [SerializeField] private EnemyIaCommon cacIa;
    [SerializeField] private float cooldown;
    [SerializeField] private Transform playerTransformReference;
    [SerializeField] private float cacIaProba = 0.6f ;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnCoroutine()); 
    }

    private IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            EnemyIaCommon ennemyPrefab = Random.value < cacIaProba ? cacIa : shootingIa; 

            yield return new WaitForSeconds(cooldown);
            EnemyIaCommon ennemy = Instantiate(ennemyPrefab, spawnPositions[Random.Range(0, spawnPositions.Length)], Quaternion.identity);
            ennemy.playerTransformReference = playerTransformReference;
            cooldown -= 0.02f;
        }
    }
    
}
