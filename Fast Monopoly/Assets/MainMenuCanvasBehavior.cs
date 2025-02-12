using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuCanvasBehavior : MonoBehaviour
{
    public GameObject currentMenu;
    public GameObject mainMenu;
    public GameObject creditsMenu;
    public GameObject customizablesMenu;
    public GameObject optionsMenu;

    void Start()
    {
        currentMenu = mainMenu;
    }

    void Update()
    {
        
    }


    public void StartNewGame(){
        Time.timeScale = 1;
        SceneManager.LoadScene("Game");
    }

    public void credits(){
        creditsMenu.SetActive(true);
        currentMenu.SetActive(false);
        currentMenu = creditsMenu;
    }

    public void options(){
        optionsMenu.SetActive(true);
        currentMenu.SetActive(false);
        currentMenu = optionsMenu;
    }

    public void customizables(){
        customizablesMenu.SetActive(true);
        currentMenu.SetActive(false);
        currentMenu = customizablesMenu;
    }

    public void backButton(){
        currentMenu.SetActive(false);
        mainMenu.SetActive(true);
        currentMenu = mainMenu;
    }

}
