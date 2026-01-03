using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class DeathMenuManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    void Start()
    {
        scoreText.text = "Score: " + PlayerController.Score;
    }
    public void OnResetClick()
    {
        SceneManager.LoadScene("Level1Scene");
    }
    
    public void OnMenuClick()
    {
        SceneManager.LoadScene("MenuScene");
    }
    
    
}
