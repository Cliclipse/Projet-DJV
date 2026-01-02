using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //public Vector3 direction;
    
    [SerializeField] private float speed;
    [SerializeField] private float tempsDeVieMax = 5f;
 
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

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("boumm");
        //Destroy(gameObject);
    }
}
