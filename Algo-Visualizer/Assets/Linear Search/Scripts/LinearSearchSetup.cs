using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearSearchSetup : MonoBehaviour
{
    [SerializeField] private int numOfElements;

    [SerializeField] private Element element;

    [SerializeField] private GameObject elementsHolder;

    float startposx = 2;
    float startposy = 1.5f;
    float startposz = 4;

    public GridManager gridManager;
    public RuntimeNavmesh runtimeNavmesh;


    // Start is called before the first frame update
    void Start()
    {

        gridManager._width = numOfElements;
        gridManager._height = 3 ;
        gridManager.GenerateGrid();

        startposx = gridManager._extraGrids;
        startposz = gridManager._extraGrids+1;

        runtimeNavmesh.buildMeshandAgent();
        
        generateElements();
        
    }

    void generateElements(){
        int totalElements = numOfElements;
        while(numOfElements!=0){
            var elementObject = Instantiate(element, new Vector3(startposx++,startposy,startposz), Quaternion.identity);
            elementObject.name = $"Element {totalElements - numOfElements}";
            elementObject.transform.parent = elementsHolder.transform;
            elementObject.elementValueSet((totalElements - numOfElements).ToString());
            numOfElements--;
        }
    }
}
