using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordBonus : MonoBehaviour
{
    [SerializeField] private int angularSpeed = 10;
        


    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, angularSpeed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<PlayerController>(out PlayerController playerController))
        {
            playerController.maxBulletsNumber++;
            Destroy(gameObject);
        }
    }
}
