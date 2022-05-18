using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LinearSearchMenuManager : MonoBehaviour
{
    [SerializeField]
    GameObject customizePanel;

    [SerializeField]
    GameObject onPlayPanel;

    [SerializeField]
    LinearSearchSetup linearSearchSetup;

    public void goToMainMenu(){
        SceneManager.LoadScene("MainUI");
    }

    public void goToOnPlayPanel(){
        customizePanel.SetActive(false);
        onPlayPanel.SetActive(true);
        linearSearchSetup.StartLinearSearchSetup();
    }
}
