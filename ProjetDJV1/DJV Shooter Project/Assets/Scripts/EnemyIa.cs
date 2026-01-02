using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyIa : MonoBehaviour , IDamageable
{
    [SerializeField] private float shootingCooldown = 0.2f;
    public Transform playerTransformReference;
    
    private NavMeshAgent _navMeshAgent;

    
    [SerializeField] private Bullet bullet;
    [SerializeField] private int maxHealth = 2;
    private int _currentHealth;

    private Coroutine _movingProcess;
    [SerializeField] private float angularSpeed = 50f;

    private Vector3 _direction;
    private bool _canShoot;
    private bool _isAlive;

    private bool _playerNotOnSight;
    
    private void Shooting()
    {
        if (_canShoot && !_playerNotOnSight)
        {
            Bullet lastBullet = Instantiate(bullet , transform.position + Vector3.up + Vector3.forward, Quaternion.identity);
            lastBullet.transform.eulerAngles = transform.eulerAngles;
            StartCoroutine(CooldownShootingCoroutine());
        }
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
    }

    // Update is called once per frame
    void Update()
    {
        _playerNotOnSight = Physics.Raycast(transform.position + Vector3.up, _direction, _direction.magnitude);
        _direction = new Vector3(playerTransformReference.position.x - transform.position.x , transform.position.y , playerTransformReference.position.z - transform.position.z).normalized;
        Shooting();
    }
    
    public void ApplyDamage(int value)
    {
        _currentHealth -= value;
        if (_currentHealth <= 0) Death();
    }

    private void Death()
    {
        //ici faudra rajouter tout ce qui est effet de mort et rajout d'un point au score
        Destroy(this.gameObject);
    }


    protected void OnEnable()
    {
        Debug.Log("Moving");

        _movingProcess = StartCoroutine(MovingCoroutine());
        IEnumerator MovingCoroutine()
        {
            
            yield return null;
            _navMeshAgent.enabled = true;

            while (_isAlive)
            {
                _navMeshAgent.SetDestination(playerTransformReference.position);

                do yield return null;
                while (_navMeshAgent.hasPath);

                do
                {
                    var direction = (playerTransformReference.position - transform.position);
                    var lookRotation = Quaternion.LookRotation(direction.normalized);

                    transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, angularSpeed * Time.deltaTime);

                    
                    yield return null;
                    
                } while (_playerNotOnSight); // En gros il bouge tant qu'il a pas le joueur en vue

            }
        }
    }
    
    
}
