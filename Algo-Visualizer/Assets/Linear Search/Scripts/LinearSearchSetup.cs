using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearSearchSetup : MonoBehaviour
{
    [SerializeField] private int numOfElements;

    [SerializeField] private Element element;

    [SerializeField] private GameObject elementsHolder;
    [SerializeField] private GameObject tilesHolder;

    private int[] elementArray;

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

        setTileIndex();

        runtimeNavmesh.buildMeshandAgent();
        
        elementArray = new int[numOfElements];
        fillRandomData(elementArray);
        int key = itemToFind(elementArray);
        generateElements();
        
    }

    void generateElements(){
        int totalElements = numOfElements;
        while(numOfElements!=0){
            var elementObject = Instantiate(element, new Vector3(startposx++,startposy,startposz), Quaternion.identity);
            elementObject.name = $"Element {totalElements - numOfElements}";
            elementObject.transform.parent = elementsHolder.transform;
            // elementObject.elementValueSet((totalElements - numOfElements).ToString());
            elementObject.setElementValue(elementArray[totalElements - numOfElements].ToString());
            numOfElements--;
        }
    }

    void fillRandomData(int[] arr){
        for( int i = 0; i<arr.Length; i++){
            arr[i] = Random.Range(1,101);
        }
    }

    int itemToFind(int[] arr){
        int val;
        if(Random.Range(0,2) == 0){
            val = Random.Range(1,101);
        }
        else{
            val = arr[Random.Range(0,numOfElements)];
        }
        return val;
    }

    void setTileIndex(){
        int x = (int)startposx;
        int y = (int)startposz - 1;
        for (int i = 0; i < numOfElements ; i++){
            Tile tile = tilesHolder.transform.Find($"Tile {x++} {y}").GetComponent<Tile>();
            tile.setElementValue(i.ToString());
        }
    }
}
