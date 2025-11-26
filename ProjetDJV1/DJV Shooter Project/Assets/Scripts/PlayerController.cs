using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speedAcceleration = 10f;
    [SerializeField] private float jumpForce = 20f;
    
    //Non utilisés pour le moment
    [SerializeField] private float initialSpeed = 10f;
    [SerializeField] private float maxSpeed = 30f;
    
    [SerializeField] private int maxHealth = 3;
    
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;
    [SerializeField] private KeyCode leftKey = KeyCode.A;
    [SerializeField] private KeyCode rightKey = KeyCode.D;
    [SerializeField] private KeyCode upKey = KeyCode.W;
    [SerializeField] private KeyCode downKey = KeyCode.S;
    
    private Rigidbody _rb;
    private CapsuleCollider _collider;
    
    
    private bool _isGrounded;
    
    
    private int _currentHealth;

    private void CheckGround()
    {
        string[] layerMaskJumpable = { "Ground" };
        int layerMask = LayerMask.GetMask(layerMaskJumpable);
        _isGrounded = Physics.Raycast(transform.position + Vector3.up * 0.01f, Vector3.down, 0.5f , layerMask);
    }

    private void MovementManager()
    {
        if (Input.GetKey(leftKey)) _rb.velocity -= speedAcceleration * Time.deltaTime * transform.right;  
        if (Input.GetKey(rightKey)) _rb.velocity += speedAcceleration * Time.deltaTime * transform.right;
        if (Input.GetKey(downKey)) _rb.velocity -= speedAcceleration * Time.deltaTime * transform.forward;
        if (Input.GetKey(upKey)) _rb.velocity += speedAcceleration * Time.deltaTime * transform.forward;
    }

    private void JumpManager()
    {
        if (Input.GetKeyDown(jumpKey) && _isGrounded)
        {
            //Debug.Log("Jump");
            _rb.velocity += jumpForce * transform.up;
        }
    }

    
    
    // Start is called before the first frame update
    void Awake()
    {
        _currentHealth = maxHealth;
        _rb = GetComponentInChildren<Rigidbody>();
        _collider = GetComponentInChildren<CapsuleCollider>();
        




    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(_isGrounded);
        CheckGround();
        MovementManager();
        JumpManager();
    }
}
