using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AI;

public class LinearSearchSetup : MonoBehaviour
{
    [SerializeField] private int numOfElements;

    [SerializeField] private Element element;

    [SerializeField] private GameObject elementsHolder;
    [SerializeField] private GameObject tilesHolder;

    [SerializeField] private TextMeshProUGUI bartext;

    [SerializeField] private float boxoffset = 0.5f;

    bool pause = false;

    private int[] elementArray;

    float startposx = 2;
    float startposy = 1.5f;
    float startposz = 4;

    public GridManager gridManager;
    public RuntimeNavmesh runtimeNavmesh;

    NavMeshAgent agent;

    int key = 0;

    WaitForSeconds delay1 = new WaitForSeconds(1);
    WaitForSeconds delay2 = new WaitForSeconds(2);
    WaitForSeconds delay3 = new WaitForSeconds(3);
    WaitForSeconds delay4 = new WaitForSeconds(4);
    WaitForSeconds delay5 = new WaitForSeconds(5);
    WaitForSeconds delay10 = new WaitForSeconds(10);
    WaitForSeconds delay15 = new WaitForSeconds(15);
    WaitForSeconds delay20 = new WaitForSeconds(20);
    WaitForSeconds delay30 = new WaitForSeconds(30);



    // Start is called before the first frame update
    void Start()
    {
        bartext.SetText("Linear Search Algorithm");

        gridManager._width = numOfElements;
        gridManager._height = 3 ;
        gridManager.GenerateGrid();
        
        startposx = gridManager._extraGrids;
        startposz = gridManager._extraGrids+1;

        setTileIndex();

        runtimeNavmesh.buildMeshandAgent();

        agent = GameObject.FindGameObjectWithTag("Player").GetComponent<NavMeshAgent>();

        startposy = agent.transform.position.y;
        
        elementArray = new int[numOfElements];
        fillRandomData(elementArray);
        key = itemToFind(elementArray);
        generateElements();
        
    }

    void generateElements(){
        float x = startposx;
        float y = startposy + boxoffset;
        float z = startposz;

        int totalElements = numOfElements;
        int tnumOfElements = numOfElements;
        while(tnumOfElements!=0){
            var elementObject = Instantiate(element, new Vector3(x++,y,z), Quaternion.identity);
            elementObject.name = $"Element {totalElements - tnumOfElements}";
            elementObject.transform.parent = elementsHolder.transform;
            // elementObject.elementValueSet((totalElements - numOfElements).ToString());
            elementObject.setElementValue(elementArray[totalElements - tnumOfElements].ToString());
            tnumOfElements--;
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

    public void PlayAlgorithm(){
        StopAllCoroutines();
        pause = false;
        StartCoroutine(LinearSearch());
    }

    public void InvertPause(){
        pause = !pause;
    }

    IEnumerator LinearSearch(){
        bartext.SetText($"Item to find : {key}");
        
        yield return delay5;
        yield return new WaitUntil(() => pause == false);

        float x = startposx;
        float y = startposy;
        float z = startposz-1;
        int index = 0;

        while(index<numOfElements){
            Vector3 pos = new Vector3(x++,y,z);
            bartext.SetText($"Moving Agent to index {index}");
            NavController.moveToVector3(agent, pos);
            yield return new WaitUntil(() => agent.transform.position == pos);
            yield return delay4;
            yield return new WaitUntil(() => pause == false);

            bartext.SetText($"is {elementArray[index]} == {key} ?");
            yield return delay4;
            yield return new WaitUntil(() => pause == false);

            if(elementArray[index] == key){
                bartext.SetText($"Yes");
                yield return delay2;
                yield return new WaitUntil(() => pause == false);
                bartext.SetText($"{key} found at index {index}");
                break;
            }
            else{
                bartext.SetText($"No");
                yield return delay2;
                yield return new WaitUntil(() => pause == false);
                bartext.SetText($"{key} not found at index {index}");
                yield return delay4;
                yield return new WaitUntil(() => pause == false);
                index++;
            }
        }
        if(index>= numOfElements){
            bartext.SetText($"{key} not present in the available elements");
        }
        yield return null;
    }
}
