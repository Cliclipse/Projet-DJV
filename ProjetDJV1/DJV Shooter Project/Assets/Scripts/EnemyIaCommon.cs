using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyIaCommon : MonoBehaviour , IDamageable
{
    [SerializeField] private Animator animator;
    public Transform playerTransformReference;
    
    private DeathIA _deathIA;
    private NavMeshAgent _navMeshAgent;

    
    [SerializeField] private int maxHealth = 2;
    private int _currentHealth;
    [SerializeField] private float angularSpeed = 50f;
    [SerializeField] private float maxMagnitudePlayerToShoot = 30f;
    
    
    
    private bool _isAlive;

    
    private int _isMovingHash;

    public bool shouldAttack;

    
    // Start is called before the first frame update
    void Awake()
    {
        _isAlive = true;
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _deathIA = GetComponent<DeathIA>();
        
        _currentHealth = maxHealth;
        
        _isMovingHash = Animator.StringToHash("isMoving");
        
    }


    // Update is called once per frame
    void Update()
    {
        shouldAttack = _navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance;
    }
    
    public void ApplyDamage(int value)
    {
        _currentHealth -= value;
        if (_currentHealth <= 0) _deathIA.Death();
    }


    protected void OnEnable()
    {
        StartCoroutine(MovingCoroutine());
    }

    IEnumerator MovingCoroutine()
    {
        if (enabled)
        {
            _navMeshAgent.enabled = true;
            _navMeshAgent.updateRotation = false;

            yield return null;
            animator.SetBool(_isMovingHash , false);
            
            if (!_navMeshAgent.isOnNavMesh)
                yield break;
        

            while (_isAlive)
            {            

                while (_navMeshAgent.pathPending) yield return null;
            
                _navMeshAgent.updateRotation = true;
                _navMeshAgent.SetDestination(playerTransformReference.position);

                if (_navMeshAgent.remainingDistance > _navMeshAgent.stoppingDistance)
                {
                    animator.SetBool(_isMovingHash , true); 
                }
                else
                {
                    _navMeshAgent.updateRotation = false;
                    animator.SetBool(_isMovingHash , false);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(playerTransformReference.position - transform.position), angularSpeed * Time.deltaTime);
                }
                yield return null;
            }
        } 
    }
    
}
