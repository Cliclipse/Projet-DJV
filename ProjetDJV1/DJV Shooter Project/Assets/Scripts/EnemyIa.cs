using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyIa : MonoBehaviour , IDamageable
{
    [SerializeField] private Animator animator;
    [SerializeField] private float shootingCooldown = 0.5f;
    public Transform playerTransformReference;
    
    private NavMeshAgent _navMeshAgent;

    
    [SerializeField] private Bullet bullet;
    [SerializeField] private int maxHealth = 2;
    private int _currentHealth;

    [SerializeField] private Transform shootingSpot;
    [SerializeField] private float angularSpeed = 50f;
    
    [SerializeField] private float maxMagnitudePlayerToShoot = 30f;
    private float _magnitudePlayer;

    
    
    private Vector3 _direction;
    private bool _canShoot;
    private bool _isAlive;

    private bool _playerOnSight;
    
    private int _isMovingHash;
    private int _isAttackingHash;
    
    private void Shooting()
    {
        if (_canShoot && _playerOnSight && _magnitudePlayer < maxMagnitudePlayerToShoot )
        {
            animator.SetBool(_isAttackingHash , true);
            Bullet lastBullet = Instantiate(bullet , shootingSpot.position, Quaternion.identity);
            lastBullet.transform.eulerAngles = transform.eulerAngles;
            StartCoroutine(AnimationCoroutine());
            StartCoroutine(CooldownShootingCoroutine());
        }
    }
    
    private IEnumerator AnimationCoroutine()
    {
        yield return new WaitForSeconds(0.2f);
        animator.SetBool(_isAttackingHash , false);
    }

    private IEnumerator CooldownShootingCoroutine()
    {
        _canShoot = false;
        yield return new WaitForSeconds(shootingCooldown);
        _canShoot = true;
    }
    
    // Start is called before the first frame update
    void Awake()
    {
        _isAlive = true;
        _navMeshAgent = GetComponent<NavMeshAgent>();

        _currentHealth = maxHealth;
        _canShoot = true;
        
        _isMovingHash = Animator.StringToHash("isMoving");
        _isAttackingHash = Animator.StringToHash("isAttacking");
        
    }

    private void DetectionJoueur()
    {
        RaycastHit hit;

        Vector3 origin = transform.position + Vector3.up;
        if (Physics.Raycast(origin, _direction, out hit, _direction.magnitude ))
        {
            Debug.DrawLine(origin, hit.point, Color.red);
            _playerOnSight = hit.collider.gameObject.TryGetComponent<PlayerController>(out var component);
            
        }
        else
        {
            _playerOnSight = false;
        }
    }
    // Update is called once per frame
    void Update()
    {

        _magnitudePlayer = (playerTransformReference.position - transform.position).magnitude;
        Debug.DrawRay(transform.position + Vector3.up, _direction, Color.red);

        DetectionJoueur();
        
        
        
        _direction = new Vector3(playerTransformReference.position.x - transform.position.x , transform.position.y , playerTransformReference.position.z - transform.position.z);
        Shooting();
    }
    
    public void ApplyDamage(int value)
    {
        _currentHealth -= value;
        if (_currentHealth <= 0) Death();
    }

    private void Death()
    {
        PlayerController.Score += 1;
        Destroy(gameObject);
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
