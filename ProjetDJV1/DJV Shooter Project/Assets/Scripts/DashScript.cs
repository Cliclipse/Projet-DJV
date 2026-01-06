using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Cohérence de mettre un dash dans ce jeu : 0. Mais c'est tjr drôle so.... Toute façon j'ai une UI de pirate avec des chevaliers qui tirent des épées 
public class DashScript : MonoBehaviour
{
    [SerializeField] private KeyCode dashKey = KeyCode.Mouse1;

    [SerializeField] private float dashSpeedThreshhold = 50f;
    [SerializeField] private float dashDuration = 0.2f;
    [SerializeField] private float dashCooldown = 10f;
    
    private InformationsController _canvaPlayer;

    private float _previousSpeed;
    private PlayerController _playerController;
    
    private bool _canDash = true;

    void Awake()
    {
        _canDash = true;
        _playerController = GetComponent<PlayerController>();
        _canvaPlayer = GetComponentInChildren<InformationsController>();
    }
    void Update()
    {
        if (Input.GetKeyDown(dashKey) && _canDash)
        {
            StartCoroutine(DashCoroutine());
        }
    }

    private IEnumerator DashCoroutine()
    {
        _canDash = false;
        _playerController.isDashing = true;
        _canvaPlayer.isDashing = true;
        _canvaPlayer.canDash = false;

        
        _previousSpeed = _playerController.speed;
        
        _playerController.speed = dashSpeedThreshhold;
        yield return new WaitForSeconds(dashDuration);
        _playerController.speed = _previousSpeed;
        
        _previousSpeed = _playerController.speed;
        
        _canvaPlayer.isDashing = false;
        _playerController.isDashing = false;
        StartCoroutine(DashCooldownCoroutine());
    }

    private IEnumerator DashCooldownCoroutine()
    {
        yield return new WaitForSeconds(dashCooldown);
        _canDash = true;
        _canvaPlayer.canDash = true;

    }
}
