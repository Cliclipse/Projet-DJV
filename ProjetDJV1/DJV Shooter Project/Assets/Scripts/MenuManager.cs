using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    
    [SerializeField] RectTransform panel;
    
    // Start is called before the first frame update
    void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        panel.gameObject.SetActive(false);

    }
    
    public void OnStartGame()
    {
        Debug.Log("Game Started");
        SceneManager.LoadScene("Level1Scene");
    }
    
    public void OnCommandClick()
    {
        panel.gameObject.SetActive(true);
    }
    public void OnCommandClosed()
    {
        panel.gameObject.SetActive(false);
    }
}
