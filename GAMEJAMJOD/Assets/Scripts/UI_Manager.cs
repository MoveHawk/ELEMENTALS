using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class UI_Manager : MonoBehaviour
{
    public GameObject PauseMenu;
    
   

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            PauseMenu.SetActive(true);
        }
    }

    public void ResumeGame()
    {
        PauseMenu.SetActive(false);
    }

    public void LoadMainMenu()
    {
        //sceneManager load main menu
        Debug.Log("Load main menu from (UI_Manager (menus)");
    }

    public void Quit()
    {
        //Quit Game
        Debug.Log("Quit from (UI_Manager (menus)");
    }

    public void RestartGame()
    {
        //open Active scene
        Debug.Log("Open active scene using Scene Manager (UI_Manager (menus)");
    }

   
}
