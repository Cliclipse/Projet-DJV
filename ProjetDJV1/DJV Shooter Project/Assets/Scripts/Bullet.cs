using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //public Vector3 direction;
    
    [SerializeField] private float speed;
    [SerializeField] private float tempsDeVieMax = 5f;    //Temps de vie de la balle avant sa disparition pour pas faire laguer en laissant 500 balles dans la scene.

    [SerializeField] private int damageDone = 1;
    
    private bool _oneHitSecurity; //Pour corriger un problème de double dégat
 
    private IEnumerator PurgeCoroutine()
    {
        yield return new WaitForSeconds(tempsDeVieMax);
        Destroy(this.gameObject);
    }
    
    void Start()
    {
        _oneHitSecurity = true;
        StartCoroutine(PurgeCoroutine());
    }
    
    private void Update()
    {
        transform.position += transform.forward * (speed * Time.fixedDeltaTime);
    }

    private void AgentHit(IDamageable damageable)
    {
        damageable.ApplyDamage(damageDone); 
        Explode();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (_oneHitSecurity)
        {
            _oneHitSecurity = false;
            IDamageable damageable;
            if (other.gameObject.TryGetComponent<IDamageable>(out damageable) || other.gameObject.transform.parent.TryGetComponent<IDamageable>(out damageable) ) AgentHit(damageable); //Ici j'ai un bug de bullet mais je trouve pas pk
            else Explode();
        }

    }

    private void Explode()
    {
        Destroy(gameObject);
    }
    
}
