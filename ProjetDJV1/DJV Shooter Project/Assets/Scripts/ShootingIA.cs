using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingIA : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Bullet bullet;
    [SerializeField] private Transform shootingSpot;
    
    [SerializeField] private float shootingCooldown = 0.7f;
    private EnemyIaCommon _enemyIaCommon;

    
    private int _isAttackingHash;

    
    private bool _canShoot;
    
    
    private void Shooting()
    {
        _canShoot = false;
        animator.SetBool(_isAttackingHash , true); 
        Bullet lastBullet = Instantiate(bullet , shootingSpot.position, Quaternion.identity);
        lastBullet.transform.eulerAngles = transform.eulerAngles;
        lastBullet.gameObject.layer = 7; //layerEnnemy
        StartCoroutine(AnimationCoroutine());
        StartCoroutine(CooldownShootingCoroutine());
    }
    
    
    private IEnumerator AnimationCoroutine()
    {
        yield return new WaitForSeconds(0.2f);
        animator.SetBool(_isAttackingHash , false);
    }

    private IEnumerator CooldownShootingCoroutine()
    {
        yield return new WaitForSeconds(shootingCooldown);
        _canShoot = true;
    }
    
    
    // Start is called before the first frame update
    void Start()
    {
        _enemyIaCommon = GetComponent<EnemyIaCommon>();
        
        _enemyIaCommon.shouldAttack = false;
        _canShoot = true;
        _isAttackingHash = Animator.StringToHash("isAttacking");

    }

    // Update is called once per frame
    void Update()
    {
        if (_enemyIaCommon.shouldAttack && _canShoot)
        {
            Shooting();
        }
    }
}
