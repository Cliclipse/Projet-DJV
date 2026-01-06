using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InformationsController : MonoBehaviour
{
    [SerializeField] PlayerController player;
    [SerializeField] TextMeshProUGUI textBullets;
    [SerializeField] TextMeshProUGUI textLives;
    [SerializeField] TextMeshProUGUI textScore;

    [SerializeField] private RawImage dashSpeedLine;
    [SerializeField] private RawImage dashIndication;

    
    public bool isDashing;
    public bool canDash;
    
    // bon faire le truc qui récup son nombre de balles et l'affiche
    
    // Start is called before the first frame update
    void Start()
    {
        isDashing = false;
        canDash = true;
    }

    // Update is called once per frame
    void Update()
    {
        textBullets.text =  player.bulletsNumberLeft.ToString();
        textLives.text = player.currentHealth.ToString();
        textScore.text = "Score: " + PlayerController.Score;

        dashSpeedLine.gameObject.SetActive(isDashing);
        dashIndication.gameObject.SetActive(canDash);
    }
}
