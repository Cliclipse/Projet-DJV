using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Vector3 = UnityEngine.Vector3;

public class PlayerController : MonoBehaviour , IDamageable
{
    [SerializeField] private Animator animator;

    
    [SerializeField] private float speedAcceleration = 1f;
    [SerializeField] private float jumpForce = 2f;
    
    //Non utilisés pour le moment
    [SerializeField] private float initialSpeed = 10f;
    [SerializeField] private float maxSpeed = 30f;

    public float speed;
    
    [SerializeField] private int maxHealth = 3;
    
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;
    [SerializeField] private KeyCode leftKey = KeyCode.A;
    [SerializeField] private KeyCode rightKey = KeyCode.D;
    [SerializeField] private KeyCode upKey = KeyCode.W;
    [SerializeField] private KeyCode downKey = KeyCode.S;
    [SerializeField] private KeyCode shootingKey = KeyCode.Mouse0;
    [SerializeField] private Bullet bullet;

    [SerializeField] private Transform shootingSpot;
    
    [SerializeField] int maxBulletsNumber = 12;
    public int bulletsNumberLeft;
    
    [SerializeField] private float reloadingCooldown = 2f;
    [SerializeField] private float shootingCooldown = 0.1f;
    
    
    public static int Score = 0;
    
     
    private CharacterController _characterController;
    private CapsuleCollider _collider;

    private bool _canShoot;
    private bool _canMove;
    private bool _isGrounded;
    public bool isDashing = false;

    private int _isAttackingHash;
    
    private int _isMovingHash;
    private int _isMovingForwardHash;
    private int _isMovingBackHash;
    private int _isMovingLeftHash;
    private int _isMovingRightHash;


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
        if (_canMove)
        {
            Vector3 movement = Vector3.zero;
            if (Input.GetKey(leftKey)) movement -= speed * Time.deltaTime * transform.right/1.5f;  
            if (Input.GetKey(rightKey)) movement += (speed * Time.deltaTime * transform.right)/1.5f;
            if (Input.GetKey(downKey))
            {
                speed = initialSpeed;
                movement -= (speed * Time.deltaTime * transform.forward)/2f;
            }
            if (Input.GetKey(upKey)) movement += (speed * Time.deltaTime * transform.forward);

            _characterController.Move(movement);
            transform.position = new Vector3(transform.position.x, 1, transform.position.z); // ligne horrible mais je comprends pas mon persos fait que de se mettre en 1.1 de hauteur
            //même avec ça il vole j'en peux plus !!!
            if (movement == Vector3.zero)
            {
                animator.SetBool(_isMovingHash , false);
                animator.SetBool(_isMovingLeftHash , false);
                animator.SetBool(_isMovingRightHash , false);
                animator.SetBool(_isMovingBackHash , false);
                animator.SetBool(_isMovingForwardHash , false);
                speed = initialSpeed;
            }
            else
            {                
                animator.SetBool(_isMovingHash , true);
                GestionAnimationDeplacement(transform.InverseTransformDirection(movement));

                if (!isDashing) speed = Math.Min(speed + speedAcceleration * Time.deltaTime , maxSpeed);
            } 
        }
    }

    private void GestionAnimationDeplacement(Vector3 movement)
    {
        animator.SetBool(_isMovingForwardHash , false);
        animator.SetBool(_isMovingRightHash , false);
        animator.SetBool(_isMovingBackHash , false);
        animator.SetBool(_isMovingLeftHash , false);
        
        float absX = Mathf.Abs(movement.x);
        float absZ = Mathf.Abs(movement.z);

        if (absX >= absZ)
        {
            if (movement.x > 0) animator.SetBool(_isMovingRightHash , true);
            else animator.SetBool(_isMovingLeftHash , true);
        }
        
        if (absZ >= absX)
        {
            if (movement.z > 0) animator.SetBool(_isMovingForwardHash, true);
            else animator.SetBool(_isMovingBackHash , true);
        }
    }


    private void ShootManager()
    {
        if (Input.GetKeyDown(shootingKey) && _canShoot)
        {
            animator.SetBool(_isAttackingHash , true);
            Bullet lastBullet = Instantiate(bullet);
            lastBullet.transform.position = shootingSpot.position; // jsp pk forward c'est ma droite
            lastBullet.transform.eulerAngles = new Vector3(lastBullet.transform.eulerAngles.x , transform.eulerAngles.y , lastBullet.transform.eulerAngles.z) ;
    
            bulletsNumberLeft -= 1;

            StartCoroutine(AnimationCoroutine());
            if (bulletsNumberLeft <= 0) StartCoroutine(ReloadCoroutine());
            else StartCoroutine(ShootCooldownCoroutine());

        }
    }

    private IEnumerator AnimationCoroutine()
    {
        yield return new WaitForSeconds(0.2f);
        animator.SetBool(_isAttackingHash , false);
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

    private IEnumerator DeathCoroutine()
    {
        _canShoot = false;
        _canMove = false;
        Debug.Log("Death");
        //Là faudra que je mette de quoi activer le booleen d'animation de mort ensuite.
        yield return new WaitForSeconds(0.5f);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene("DeathScene");
    }

    public void ApplyDamage(int value)
    {
        currentHealth -= value;
        if (currentHealth <= 0) StartCoroutine(DeathCoroutine());
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
        Score = 0;
        _canShoot = true;
        _canMove = true;
        bulletsNumberLeft = maxBulletsNumber;
        currentHealth = maxHealth;
        _characterController = GetComponent<CharacterController>();
        _collider = GetComponentInChildren<CapsuleCollider>();
        _isAttackingHash = Animator.StringToHash("isAttacking");
        
        
        _isMovingHash = Animator.StringToHash("isMoving");
        
        _isMovingForwardHash = Animator.StringToHash("isMovingForward");
        _isMovingBackHash = Animator.StringToHash("isMovingBack");
        _isMovingRightHash = Animator.StringToHash("isMovingRight");
        _isMovingLeftHash = Animator.StringToHash("isMovingLeft");

        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(_isGrounded);
        //CheckGround();
        //JumpManager();
        MovementManager();
        ShootManager();

    }
}
