using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    GameObject mainMenuPanel;

    [SerializeField]
    GameObject algorithmsPanel;

    [SerializeField]
    GameObject optionsPanel;
    
    public void playscene(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    public void quit(){
        //function for quitting the application
        Application.Quit();
        Debug.Log("quit");
    }

    public void BackToMenu(){
        SceneManager.LoadScene(0);
    }

    public void GoToAlgorithmsPanel(){
        mainMenuPanel.SetActive(false);
        optionsPanel.SetActive(false);
        algorithmsPanel.SetActive(true);
    }

    public void GoToMainMenu(){
        algorithmsPanel.SetActive(false);
        optionsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    public void GoToOptionsPanel(){
        mainMenuPanel.SetActive(false);
        algorithmsPanel.SetActive(false);
        optionsPanel.SetActive(true);
    }
    
}
