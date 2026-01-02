using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
