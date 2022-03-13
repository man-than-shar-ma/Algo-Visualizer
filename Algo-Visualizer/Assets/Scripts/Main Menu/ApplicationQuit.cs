using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationQuit : MonoBehaviour
{
    public void quit(){
        Application.Quit();
        Debug.Log("quit");
    }
}
