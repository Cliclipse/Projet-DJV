using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathIA : MonoBehaviour
{
    [SerializeField] private SwordBonus swordBonus;
    [SerializeField] private ShieldBonus shieldBonus;
    
    [SerializeField] private float shieldDropChance = 0.07f;
    [SerializeField] private float swordDropChance = 0.1f;

    public void Death()
    {
        float tirage = Random.value;
        if (tirage < shieldDropChance + swordDropChance)
        {
            if (tirage < shieldDropChance) Instantiate(shieldBonus , transform.position + Vector3.up , transform.rotation);
            else Instantiate(swordBonus , transform.position , transform.rotation);
        }
        PlayerController.Score += 1;
        Destroy(gameObject);
    }


}
