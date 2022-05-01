using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
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
}
