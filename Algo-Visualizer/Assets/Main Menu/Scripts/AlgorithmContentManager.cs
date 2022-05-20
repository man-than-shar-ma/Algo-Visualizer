using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlgorithmContentManager : MonoBehaviour
{
    public Algorithm[] algorithm;
    int numOfAlgorithms = 2;

    public GameObject algoPrefab;
    public GameObject algoContentHolder;

    public AlgoDetailed algoDetailed;

    string[] algoName = 
    {
        "Linear Search",
        "Binary Search"
    };

    string[] algoDescription = 
    {

@"Linear search is a very basic and simple search algorithm and is also known as sequential search. 
In Linear search, we search an element or value in a given array by traversing the array from the starting, till the desired element or value is found and then return the index of that element. 
If no match is found then -1 is returned.",

@"Binary search is the most popular Search algorithm.
It is efficient and also one of the most commonly used techniques that is used to solve problems.
Binary search works only on a sorted set of elements, that means to use binary search on a collection, the collection must first be sorted.
This searching technique follows the divide and conquer strategy. 
The search space always reduces to half in every iteration."

    };
    string[] algoAlgorithm = 
    {   

@"Linear Search ( Array A, Value x)
    Step 1: Set i to 1
    Step 2: if i > n then 
        go to step 7
    Step 3: if A[i] = x then 
        go to step 6
    Step 4: Set i to i + 1
    Step 5: Go to Step 2
    Step 6: Print Element x Found at index i and 
        go to step 8
    Step 7: Print element not found
    Step 8: Exit",

@"Binary_Search(a, lower_bound, 
    upper_bound, val)
    Step 1: set beg = lower_bound, 
        end = upper_bound, pos = - 1  
    Step 2: repeat steps 3 and 4 while beg <=end  
    Step 3: set mid = (beg + end)/2  
    Step 4: if a[mid] = val  
        set pos = mid  
        print pos  
        go to step 6  
        else if a[mid] > val  
        set end = mid - 1  
        else  
        set beg = mid + 1  
        [end of if]  
        [end of loop]  
    Step 5: if pos = -1  
        print 'value is not present in the array'
        [end of if]  
    Step 6: exit"

    };

    string[] algoTimeComplexity = 
    {
        "Best: O(1)\nAverage: O(n)\nWorst: O(n)",
        "Best: O(1)\nAverage: O(logn)\nWorst: O(logn)"
    };

    string[] algoSpaceComplexity = 
    {
        "Worst: O(n)",
        "Worst: O(n)"
    };

    string[] nextSceneName = {
        "LinearSearch",
        "MainUI"
    };



    // Start is called before the first frame update
    void Start()
    {
        algorithm = new Algorithm[numOfAlgorithms];

        for (int i=0; i<numOfAlgorithms;i++){
            var algo = Instantiate(algoPrefab, Vector3.zero, Quaternion.identity);
            algo.name = algoName[i];
            algo.transform.SetParent(algoContentHolder.transform);
            algo.transform.localScale = Vector3.one;

            algorithm[i] = algo.GetComponent<Algorithm>();
            algorithm[i].setAlgoName("\t"+algoName[i]);
            algorithm[i].setAlgoDescription("\t"+algoDescription[i]);
            algorithm[i].setAlgoAlgorithm(algoAlgorithm[i]);
            algorithm[i].setAlgoTimeComplexity(algoTimeComplexity[i]);
            algorithm[i].setAlgoSpaceComplexity(algoSpaceComplexity[i]);
            algorithm[i].setNextSceneName(nextSceneName[i]);

            algorithm[i].setAlgorithmValue(algoName[i]);
            
            algorithm[i].setButtonClick(algoDetailed);
            // algorithm[i].GetComponent<Button>().onClick.AddListener(() => algorithm[i].setAlgoDetailedText(algoDetailed));
        }

        Algorithm[] cSoon = new Algorithm[10];
        for(int i=0;i<cSoon.Length;i++){
            var algo = Instantiate(algoPrefab, Vector3.zero, Quaternion.identity);
            algo.name = "Coming Soon";
            algo.transform.SetParent(algoContentHolder.transform);
            algo.transform.localScale = Vector3.one;
            cSoon[i] = algo.GetComponent<Algorithm>();

            cSoon[i].setAlgorithmValue(algo.name);
            cSoon[i].setNextSceneName("MainUi");
            cSoon[i].setButtonClick(algoDetailed);
        }
    }
}
