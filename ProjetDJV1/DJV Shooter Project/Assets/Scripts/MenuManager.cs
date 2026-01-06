using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    
    public void OnStartGame()
    {
        Debug.Log("Game Started");
        SceneManager.LoadScene("Level1Scene");
    }
    
    public void OnCreditClick()
    {
        Debug.Log("Crédits, mais pr le moment j'en ai pas, dommage");
    }
}
