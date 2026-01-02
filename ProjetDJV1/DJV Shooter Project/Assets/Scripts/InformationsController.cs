using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InformationsController : MonoBehaviour
{
    [SerializeField] PlayerController player;
    [SerializeField] TextMeshProUGUI textBullets;
    [SerializeField] TextMeshProUGUI textLives;

    // bon faire le truc qui récup son nombre de balles et l'affiche
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        textBullets.text =  player.bulletsNumberLeft.ToString();
        textLives.text = player.currentHealth.ToString();

    }
}
