using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIa : MonoBehaviour
{
    [SerializeField] private float shootingCooldown = 0.2f;
    public Transform playerTransformReference;
    
    [SerializeField] private Bullet bullet;

    
    

    private Vector3 direction;
    private bool _canShoot;
    
    private void Shooting()
    {
        if (_canShoot)
        {
            bool hitWall = Physics.Raycast(transform.position + Vector3.up, direction, direction.magnitude);
            if (!hitWall)
            {
                Bullet lastBullet = Instantiate(bullet , transform.position + Vector3.up + Vector3.forward, Quaternion.identity);
                lastBullet.transform.eulerAngles = transform.eulerAngles;
                StartCoroutine(CooldownShootingCoroutine());
            }
        }
    }

    private IEnumerator CooldownShootingCoroutine()
    {
        _canShoot = false;
        yield return new WaitForSeconds(shootingCooldown);
        _canShoot = true;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        _canShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        direction = (playerTransformReference.position - transform.position).normalized;
        Shooting();
    }
}
