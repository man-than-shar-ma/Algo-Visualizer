using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlgorithmContentManager : MonoBehaviour
{
    public Algorithm[] algorithm;
    int numOfAlgorithms;

    public GameObject algoPrefab;
    public GameObject algoContentHolder;

    public AlgoDetailed algoDetailed;

    string[] algoName = 
    {
        "Linear Search",
        "Binary Search",
        "Bubble Sort",
        "Selection Sort",
        "Insertion Sort"
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
The search space always reduces to half in every iteration.",

@"Bubble sort is a simple sorting algorithm. 
This sorting algorithm is comparison-based algorithm in which each pair of adjacent elements is compared and the elements are swapped if they are not in order.
It is called bubble sort because the movement of array elements is just like the movement of air bubbles in the water. 
Similar to the bubbles in water that rise up to the surface, the array elements also move to the end in each iteration.",

@"In selection sort, the smallest value among the unsorted elements of the array is selected in every pass and places that element at the beginning of the unsorted list. 
It is also the simplest algorithm and is divided into two parts, the sorted part at the left end and the unsorted part at the right end.",

@"Insertion sort is a sorting algorithm that places an unsorted element at its suitable place in each iteration.
Insertion sort works similarly as we sort cards in our hand in a card game.
We assume that the first card is already sorted then, we select an unsorted card. If the unsorted card is greater than the card in hand, it is placed on the right otherwise, to the left. In the same way, other unsorted cards are taken and put in their right place.
A similar approach is used by insertion sort."

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
        else if a[mid] < val  
        set beg = mid + 1  
        else  
        set end = mid - 1  
        [end of if]  
        [end of loop]  
    Step 5: if pos = -1  
        print 'value is not present in the array'
        [end of if]  
    Step 6: exit",

@"Step 1: First Iteration (Compare and Swap)
    a. Starting from the first index, compare the first and the second elements.
    b. If the first element is greater than the second element, they are swapped.
    c. Now, compare the second and the third elements. Swap them if they are not in order.
    d. The above process goes on until the last element.

Step 2: Remaining Iteration
    a. The same above process goes on for the remaining iterations.
    b. After each iteration, the largest element among the unsorted elements is placed at the end.
    c. In each iteration, the comparison takes place up to the last unsorted element.
    d. The array is sorted when all the unsorted elements are placed at their correct positions.",

@"Step 1: Set the first element as minimum.

Step 2: Compare minimum with the second element. If the second element is smaller than minimum, assign the second element as minimum.

Step 3: Compare minimum with the third element. Again, if the third element is smaller, then assign minimum to the third element otherwise do nothing. The process goes on until the last element.

Step 4: After each iteration, minimum is placed in the front of the unsorted list.

Step 5: For each iteration, indexing starts from the first unsorted element. Step 1 to 3 are repeated until all the elements are placed at their correct positions.",

@"Step 1: The first element in the array is assumed to be sorted. Take the second element and store it separately in key.
Compare key with the first element. If the first element is greater than key, then key is placed in front of the first element.

Step 2: Now, the first two elements are sorted.
Take the third element and compare it with the elements on the left of it. Placed it just behind the element smaller than it. If there is no element smaller than it, then place it at the beginning of the array.

Step 3: Similarly, place every unsorted element at its correct position.
"

    };

    string[] algoTimeComplexity = 
    {
        "Best: \u03A9(1)\nAverage: \u0398(n)\nWorst: \u039F(n)",
        "Best: \u03A9(1)\nAverage: \u0398(logn)\nWorst: \u039F(logn)",
        "Best: \u03A9(n<sup>2</sup>)\nAverage: \u0398(n<sup>2</sup>)\nWorst: \u039F(n<sup>2</sup>)",
        "Best: \u03A9(n<sup>2</sup>)\nAverage: \u0398(n<sup>2</sup>)\nWorst: \u039F(n<sup>2</sup>)",
        "Best: \u03A9(n)\nAverage: \u0398(n<sup>2</sup>)\nWorst: \u039F(n<sup>2</sup>)"
    };

    string[] algoSpaceComplexity = 
    {
        "Worst: \u039F(1)",
        "Worst: \u039F(1)",
        "Worst: \u039F(1)",
        "Worst: \u039F(1)",
        "Worst: \u039F(1)"
    };

    string[] algoCppProgram = 
    {
@"
#include <iostream>
using namespace std;
int main()
{
    int a[20], i, x, n;
    cout << 'Number of elements in an array are : ';
    cin >> n;
    cout << endl;
    cout << 'Enter array elements : ' << endl;
    for (i = 0; i < n; i++)
    {
        cout << 'Enter arr[' << i << '] Element : ';
        cin >> a[i];
    }
    cout << endl;
    cout << 'Enter element to search : ';
    cin >> x;
    for (i = 0; i < n; i++)
        if (a[i] == x)
            break;
    if (i < n)
        cout << 'Element ' << x << ' found at index ' << i;
    else
        cout << 'Element not found';
    return 0;
}
",

@"
#include <iostream>
using namespace std;
const int maxsize = 10;
int arr[maxsize];

int binarySearch(int left, int right, int x)
{
    while (left <= right)
    {
        int mid = (left + right) / 2;
        if (arr[mid] == x)
        {
            return mid;
        }
        else if (arr[mid] < x)
        {
            left = mid + 1;
        }
        else
        {
            right = mid - 1;
        }
    }
    return -1;
}

int main()
{
    int num;
    int output;
    cout << 'Please enter ' << maxsize << ' elements ascending order' << endl;
    for (int i = 0; i < maxsize; i++)
    {
        cin >> arr[i];
    }
    cout << 'Please enter an element to search' << endl;
    cin >> num;
    output = binarySearch(0, 9, num);
    if (output == -1)
    {
        cout << 'No Match Found' << endl;
    }
    else
    {
        cout << 'Match found at position: ' << output << endl;
    }
    return 0;
}
",

@"#include <iostream>
using namespace std;

void bubbleSort(int array[], int size)
{
    for (int step = 0; step < size - 1; ++step)
    {
        for (int i = 0; i < size - step - 1; ++i)
        {
            if (array[i] > array[i + 1])
            {
                int temp = array[i];
                array[i] = array[i + 1];
                array[i + 1] = temp;
            }
        }
    }
}

void printArray(int array[], int size)
{
    for (int i = 0; i < size; ++i)
    {
        cout << ' ' << array[i];
    }
    cout << '\n';
}

int main()
{
    int data[] = {-23, 75, 10, 51, -13};
    int size = sizeof(data) / sizeof(data[0]);
    cout << 'Initial array : \n';
    for (int i = 0; i < size; i++)
    {
        cout << ' ' << data[i];
    }
    bubbleSort(data, size);
    cout << '\n\nSorted Array in Ascending Order:\n';
    printArray(data, size);
    return 0;
}",

@"#include <iostream>
using namespace std;

void swap(int *a, int *b)
{
    int temp = *a;
    *a = *b;
    *b = temp;
}

void printArray(int array[], int size)
{
    for (int i = 0; i < size; i++)
    {
        cout << array[i] << ' ';
    }
    cout << endl;
}

void selectionSort(int array[], int size)
{
    for (int step = 0; step < size - 1; step++)
    {
        int min_idx = step;
        for (int i = step + 1; i < size; i++)
        {
            if (array[i] < array[min_idx])
                min_idx = i;
        }
        swap(&array[min_idx], &array[step]);
    }
}

int main()
{
    int data[] = {31, 19, 7, 35, 3};
    int size = sizeof(data) / sizeof(data[0]);
    cout << 'Initial array : \n';
    for (int i = 0; i < size; i++)
    {
        cout << data[i] << ' ';
    }
    selectionSort(data, size);
    cout << '\n\nSorted array in Acsending Order:\n';
    printArray(data, size);
    return 0;
}",

@"#include <iostream>
using namespace std;

void printArray(int array[], int size)
{
    for (int i = 0; i < size; i++)
    {
        cout << array[i] << ' ';
    }
    cout << endl;
}

void insertionSort(int array[], int size)
{
    for (int step = 1; step < size; step++)
    {
        int key = array[step];
        int j = step - 1;
        while (j >= 0 && key < array[j])
        {
            array[j + 1] = array[j];
            --j;
        }
        array[j + 1] = key;
    }
}

int main()
{
    int data[] = {19, 35, 14, 54, 35};
    int size = sizeof(data) / sizeof(data[0]);
    cout << 'Initial array : \n';
    for (int i = 0; i < size; i++)
    {
        cout << data[i] << ' ';
    }
    insertionSort(data, size);
    cout << '\n\nSorted array in ascending order:\n';
    printArray(data, size);
    return 0;
}
"
    };

    string[] nextSceneName = {
        "LinearSearch",
        "BinarySearch",
        "BubbleSort",
        "SelectionSort",
        "InsertionSort"
    };

    // Start is called before the first frame update
    void Start()
    {
        numOfAlgorithms = algoName.Length;
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
            algorithm[i].setCppProgram(algoCppProgram[i]);
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
