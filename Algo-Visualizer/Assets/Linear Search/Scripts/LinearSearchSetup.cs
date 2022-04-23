using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearSearchSetup : MonoBehaviour
{
    [SerializeField] private int numOfElements;
    // [SerializeField] private 
    // [SerializeField] private GridManager gridManager;
    [SerializeField] private GameObject element;

    float startposx =2;
    float startposy = 1;
    float startposz = 4;


    // Start is called before the first frame update
    void Start()
    {
        while(numOfElements!=0){
            Instantiate(element, new Vector3(startposx++,startposy,startposz), Quaternion.identity);
            numOfElements--;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
