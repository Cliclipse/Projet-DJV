using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CacIa : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Transform hittingSpotSpawnOverlap;
    
    [SerializeField] private float hittingCooldown = 0.3f;
    private EnemyIaCommon _enemyIaCommon;

    
    private int _isAttackingHash;

    
    private bool _canHit;
    
    
    private void Hitting()
    {
        _canHit = false;
        animator.SetBool(_isAttackingHash , true); 
 
        //Et là je fais le process d'attack au cac c'est tout, à faire pour demain
        
        StartCoroutine(AnimationCoroutine());
        StartCoroutine(CooldownHittinggCoroutine());
    }
    
    
    private IEnumerator AnimationCoroutine()
    {
        yield return new WaitForSeconds(0.2f);
        animator.SetBool(_isAttackingHash , false);
    }

    private IEnumerator CooldownHittinggCoroutine()
    {
        yield return new WaitForSeconds(hittingCooldown);
        _canHit = true;
    }
    
    
    // Start is called before the first frame update
    void Start()
    {
        _enemyIaCommon = GetComponent<EnemyIaCommon>();
        
        _enemyIaCommon.shouldAttack = false;
        _canHit = true;
        _isAttackingHash = Animator.StringToHash("isAttacking");

    }

    // Update is called once per frame
    void Update()
    {
        if (_enemyIaCommon.shouldAttack && _canHit) Hitting();
    }
}
