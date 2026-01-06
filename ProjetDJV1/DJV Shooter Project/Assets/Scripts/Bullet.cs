using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //public Vector3 direction;
    
    [SerializeField] private float speed;
    [SerializeField] private float tempsDeVieMax = 5f;
    [SerializeField] private int damageDone = 1;
 
    //Temps de vie de la balle avant sa disparition pour pas faire laguer en laissant 500 balles dans la scene.
    private IEnumerator PurgeCoroutine()
    {
        yield return new WaitForSeconds(tempsDeVieMax);
        Destroy(this.gameObject);
    }
    
    void Start()
    {
        StartCoroutine(PurgeCoroutine());
    }
    
    private void Update()
    {
        transform.position += transform.forward * (speed * Time.fixedDeltaTime);
    }

    private void AgentHit(IDamageable damageable)
    {
        damageable.ApplyDamage(damageDone); // Pou
        Explode();
    }
    private void OnTriggerEnter(Collider other)
    {
        IDamageable damageable;
        if (other.gameObject.TryGetComponent<IDamageable>(out damageable) || other.gameObject.transform.parent.TryGetComponent<IDamageable>(out damageable) )
        {
            Debug.Log("test1");
            AgentHit(damageable);
        }
        else
        {
            Debug.Log(other);
            Explode();
        }
    }

    private void Explode()
    {
        Debug.Log("boumm");
        Destroy(gameObject);
    }
    
}
