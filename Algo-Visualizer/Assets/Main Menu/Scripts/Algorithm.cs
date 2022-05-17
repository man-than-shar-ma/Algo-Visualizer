using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Algorithm : MonoBehaviour
{
    MenuManager menuManager;

    [SerializeField]
    string algoName = "";
    
    [SerializeField]
    string algoDescription = "";
    
    [SerializeField]
    string algoAlgorithm = "";
    
    [SerializeField]
    string algoTimeComplexity = "";
    
    [SerializeField]
    string algoSpaceComplexity = "";

    [SerializeField] 
    TextMeshProUGUI tmprovalue;

    [SerializeField]
    Button button;

    [SerializeField]
    string nextSceneName;

    public string getAlgoName(){
        return algoName;
    }

    public void setAlgoName(string text){
        algoName = text;
    }

    public string getAlgoDescription(){
        return algoDescription;
    }

    public void setAlgoDescription(string text){
        algoDescription = text;
    }

    public string getAlgoAlgorithm(){
        return algoAlgorithm;
    }

    public void setAlgoAlgorithm(string text){
        algoAlgorithm = text;
    }

    public string getAlgoTimeComplexity(){
        return algoTimeComplexity;
    }

    public void setAlgoTimeComplexity(string text){
        algoTimeComplexity = text;
    }

    public string getAlgoSpaceComplexity(){
        return algoSpaceComplexity;
    }

    public void setAlgoSpaceComplexity(string text){
        algoSpaceComplexity = text;
    }

    public string getNextSceneName(){
        return nextSceneName;
    }

    public void setNextSceneName(string text){
        nextSceneName = text;
    }


    public void setAlgorithmValue(string text){
        tmprovalue.SetText(text);
    }

    public void setAlgoDetailedText(AlgoDetailed algodetail){
        algodetail.tmproadName.SetText($"Name: \n{algoName}");
        algodetail.tmproadDescription.SetText($"Description: \n{algoDescription}");
        algodetail.tmproadAlgorithm.SetText($"Algorithm: \n{algoAlgorithm}");
        algodetail.tmproadTimeComplexity.SetText($"Time Complexity: \n{algoTimeComplexity}");
        algodetail.tmproadSpaceComplexity.SetText($"Space Complexity: \n{algoSpaceComplexity}");

        algodetail.nextSceneButton.onClick.AddListener(() => SceneManager.LoadScene(nextSceneName));

        LayoutRebuilder.ForceRebuildLayoutImmediate(algodetail.GetComponent<RectTransform>());
    }

    public void setButtonClick(AlgoDetailed algodetail){
        button.onClick.AddListener(()=> setAlgoDetailedText(algodetail));
    }
    

}
