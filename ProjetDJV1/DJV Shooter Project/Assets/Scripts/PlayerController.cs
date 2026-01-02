using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Vector3 = UnityEngine.Vector3;

public class PlayerController : MonoBehaviour , IDamageable
{
    [SerializeField] private float speedAcceleration = 1f;
    [SerializeField] private float jumpForce = 2f;
    
    //Non utilisés pour le moment
    [SerializeField] private float initialSpeed = 10f;
    [SerializeField] private float maxSpeed = 30f;
    private float _speed;
    
    [SerializeField] private int maxHealth = 3;
    
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;
    [SerializeField] private KeyCode leftKey = KeyCode.A;
    [SerializeField] private KeyCode rightKey = KeyCode.D;
    [SerializeField] private KeyCode upKey = KeyCode.W;
    [SerializeField] private KeyCode downKey = KeyCode.S;
    [SerializeField] private KeyCode shootingKey = KeyCode.Mouse0;

    [SerializeField] private Bullet bullet;

    [SerializeField] int maxBulletsNumber = 12;
    public int bulletsNumberLeft;
    
    [SerializeField] private float reloadingCooldown = 2f;
    [SerializeField] private float shootingCooldown = 0.1f;

    
     
    private CharacterController _characterController;
    private CapsuleCollider _collider;

    private bool _canShoot;
    private bool _isGrounded;
    public int currentHealth;

    //résidu de quand je voulais faire des sauts mais on ne peut pas en faire avec un character Controller j'ai l'impression
    private void CheckGround()
    {
        string[] layerMaskJumpable = { "Ground" };
        int layerMask = LayerMask.GetMask(layerMaskJumpable);
        _isGrounded = Physics.Raycast(transform.position + Vector3.up * 0.01f, Vector3.down, 0.5f , layerMask);
    }

    private void MovementManager()
    {
        Vector3 movement = Vector3.zero;
        if (Input.GetKey(leftKey)) movement -= _speed * Time.deltaTime * transform.right;  
        if (Input.GetKey(rightKey)) movement += _speed * Time.deltaTime * transform.right;
        if (Input.GetKey(downKey)) movement -= _speed * Time.deltaTime * transform.forward;
        if (Input.GetKey(upKey)) movement += _speed * Time.deltaTime * transform.forward;

        _characterController.Move(movement);

        if (movement == Vector3.zero) _speed = initialSpeed;
        else _speed = Math.Min(_speed + speedAcceleration * Time.deltaTime , maxSpeed);
    }

    private void ShootManager()
    {
        if (Input.GetKeyDown(shootingKey) && _canShoot)
        {
            Bullet lastBullet = Instantiate(bullet);
            lastBullet.transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
            lastBullet.transform.eulerAngles = transform.eulerAngles;
    
            bulletsNumberLeft -= 1;

            if (bulletsNumberLeft <= 0) StartCoroutine(ReloadCoroutine());
            else StartCoroutine(ShootCooldownCoroutine());

        }
    }

    private IEnumerator ShootCooldownCoroutine()
    {
        _canShoot = false;
        yield return new WaitForSeconds(shootingCooldown);
        _canShoot = true;
    }
    private IEnumerator ReloadCoroutine()
    {
        _canShoot = false;
        yield return new WaitForSeconds(reloadingCooldown);
        bulletsNumberLeft = maxBulletsNumber;
        _canShoot = true;

    }

    private void Death()
    {
        Debug.Log("Death");
    }

    public void ApplyDamage(int value)
    {
        currentHealth -= value;
        if (currentHealth <= 0) Death();
    }
        
        
        
        
    /*
    private void JumpManager()
    {
        if (Input.GetKeyDown(jumpKey) && _isGrounded)
        {
            Debug.Log("Jump");
            _rb.velocity += jumpForce * transform.up;
        }
    }
*/
    
    
    // Start is called before the first frame update
    void Awake()
    {
        //_rb = GetComponentInChildren<Rigidbody>();
        
        _canShoot = true;
        bulletsNumberLeft = maxBulletsNumber;
        currentHealth = maxHealth;
        _characterController = GetComponent<CharacterController>();
        _collider = GetComponentInChildren<CapsuleCollider>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(_isGrounded);
        //CheckGround();
        //JumpManager();
        ShootManager();
        MovementManager();

    }
}
