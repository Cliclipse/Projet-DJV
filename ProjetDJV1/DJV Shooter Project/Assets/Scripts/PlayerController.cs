using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speedAcceleration = 0.5f;
    [SerializeField] private float initialSpeed = 10f;
    [SerializeField] private float maxSpeed = 30f;
    [SerializeField] private float jumpForce = 10f;
    
    
    [SerializeField] private int maxHealth = 3;
    
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;
    [SerializeField] private KeyCode leftKey = KeyCode.A;
    [SerializeField] private KeyCode rightKey = KeyCode.D;
    [SerializeField] private KeyCode upKey = KeyCode.W;
    [SerializeField] private KeyCode downKey = KeyCode.S;
    
    private Rigidbody _rb;
    
    private int _currentHealth;


    private void MovementManager()
    {
        if (Input.GetKey(leftKey)) _rb.velocity -= 0.5f * transform.right * speedAcceleration;
        if (Input.GetKey(rightKey)) _rb.velocity += 0.5f * transform.right * speedAcceleration;
        if (Input.GetKey(downKey)) _rb.velocity -= 0.5f * transform.up * speedAcceleration;
        if (Input.GetKey(upKey)) _rb.velocity += 0.5f * transform.up * speedAcceleration;
    }
    
    
    
    
    // Start is called before the first frame update
    void Awake()
    {
        _currentHealth = maxHealth;
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
