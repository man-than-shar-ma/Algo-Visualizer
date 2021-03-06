using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AI;


public class LinearSearchSetup : MonoBehaviour
{
    [SerializeField] public TMP_InputField arraysizeCustom;

    [SerializeField] public TMP_InputField arrayValuesCustom;

    [SerializeField] public TMP_InputField arrayKeyCustom;

    [SerializeField] public TMP_InputField xxx;

    [SerializeField] private int numOfElements;

    [SerializeField] private Element element;

    [SerializeField] private GameObject elementsHolder;
    [SerializeField] private GameObject tilesHolder;

    [SerializeField] private TextMeshProUGUI bartext;

    [Range(0.01f, 1f)]
    [SerializeField] private float algoSpeed = 0.01f;
    private float algoSpeed1to10 = 1;
    

    bool pause = false;

    private int[] elementArray;
    private Transform[] elementObjectArray;

    float startposx = 2;
    float startposy = 1.5f;
    float startposz = 4;

    public GridManager gridManager;
    public RuntimeNavmesh runtimeNavmesh;

    NavMeshAgent agent;

    int key = 0;

    WaitForSeconds delay1;
    WaitForSeconds delay2;
    WaitForSeconds delay3;
    WaitForSeconds delay4;
    WaitForSeconds delay5;
    WaitForSeconds delay10;
    WaitForSeconds delay15;
    WaitForSeconds delay20;
    WaitForSeconds delay30;



    // Start is called before the first frame update
    public void StartLinearSearchSetup()
    {
        //checking whether the user has entered size or not
        if(arraysizeCustom.text.Length != 0){
            numOfElements = int.Parse(arraysizeCustom.text);
        }


        delay1 = new WaitForSeconds(0.01f + (1 - algoSpeed) * 1);
        delay2 = new WaitForSeconds(0.02f + (1 - algoSpeed) * 2);
        delay3 = new WaitForSeconds(0.03f + (1 - algoSpeed) * 3);
        delay4 = new WaitForSeconds(0.04f + (1 - algoSpeed) * 4);
        delay5 = new WaitForSeconds(0.05f + (1 - algoSpeed) * 5);
        delay10 = new WaitForSeconds(0.1f + (1 - algoSpeed) * 10);
        delay15 = new WaitForSeconds(0.15f + (1 - algoSpeed) * 15);
        delay20 = new WaitForSeconds(0.2f + (1 - algoSpeed) * 20);
        delay30 = new WaitForSeconds(0.3f + (1 - algoSpeed) * 30);


        bartext.SetText("Linear Search Algorithm");

        gridManager._width = numOfElements;
        gridManager._height = 3 ;
        gridManager.GenerateGrid();
        
        startposx = gridManager._extraGrids;
        startposz = gridManager._extraGrids+1;

        setTileIndex();

        runtimeNavmesh.buildMeshandAgent();

        agent = GameObject.FindGameObjectWithTag("Player").GetComponent<NavMeshAgent>();

        algoSpeed1to10 = 1 + algoSpeed * 9;
        CameraFollow.speed = algoSpeed1to10;
        agent.speed = algoSpeed1to10;

        startposy = agent.transform.position.y;
        
        //checking whether the user has entered the array values or not
        elementArray = new int[numOfElements];
        if(arrayValuesCustom.text.Trim().Length !=0){
                string trimmedString = arrayValuesCustom.text.Trim();
                string removedBrackets = trimmedString.Substring(1, trimmedString.Length-2);
                string[] arrayStrings = removedBrackets.Split(',');
                elementArray =  System.Array.ConvertAll<string, int>(arrayStrings, int.Parse);
        }
        else{
             fillRandomData(elementArray);
        }

        //checking whether user has entered the key value or not
        if(arrayKeyCustom.text.Trim().Length !=0){
            key = int.Parse(arrayKeyCustom.text);
        }
        else{
            key = itemToFind(elementArray);
        }

        elementObjectArray = new Transform[numOfElements];
        generateElements();
        
    }

    void generateElements(){
        float x = startposx;
        float y = startposy;
        float z = startposz;

        int totalElements = numOfElements;
        int tnumOfElements = numOfElements;
        int i=0;
        while(tnumOfElements!=0){
            var elementObject = Instantiate(element, new Vector3(x++,y,z), Quaternion.identity);
            elementObject.name = $"Element {totalElements - tnumOfElements}";
            elementObject.transform.parent = elementsHolder.transform;
            // elementObject.elementValueSet((totalElements - numOfElements).ToString());
            elementObject.setElementValue(elementArray[totalElements - tnumOfElements].ToString());
            tnumOfElements--;
            elementObjectArray[i] = elementObject.GetComponent<Transform>();
            i++;
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
        float x = startposx;
        float y = startposy;
        float z = startposz;
        int i=0;
        while(i<numOfElements){
            elementObjectArray[i].position = new Vector3(x++,y,z);
            elementObjectArray[i].GetComponent<Element>().setDefaultMaterial();
            i++;
        }
        StartCoroutine(LinearSearch());
    }

    public void InvertPause(){
        pause = !pause;
    }

    IEnumerator LinearSearch(){
        bartext.SetText($"Item to find : {key}");
        
        yield return delay2;
        yield return new WaitUntil(() => pause == false);

        float x = startposx;
        float y = startposy;
        float z = startposz-1;
        int index = 0;

        while(index<numOfElements){
            Vector3 pos = new Vector3(x++,y,z);
            
            bartext.SetText($"Moving to index {index}");

            elementObjectArray[index].GetComponent<Element>().setYellowMaterial();

            agent.GetComponent<NavController>().moveToVector3(pos);
            // Debug.Log(pos + " " + agent.transform.position);
            yield return new WaitUntil(() => (agent.transform.position - pos).magnitude < 0.1);
            yield return delay1;
            
            //looking at the box
            Vector3 lookpos = elementObjectArray[index].position;
            yield return StartCoroutine(agent.GetComponent<NavController>().lookAtPoint(lookpos, algoSpeed1to10));
            yield return delay1;
            yield return new WaitUntil(() => pause == false);


            //Lifting the box up            
            yield return StartCoroutine(elementObjectArray[index].GetComponent<Element>().LiftElementUp(algoSpeed1to10));
            yield return delay1;
            yield return new WaitUntil(() => pause == false);

            bartext.SetText($"is {elementArray[index]} == {key} ?");
            yield return delay2;
            yield return new WaitUntil(() => pause == false);

            if(elementArray[index] == key){
                bartext.SetText($"Yes");
                yield return delay2;
                yield return new WaitUntil(() => pause == false);
                bartext.SetText($"{key} found at index {index}");
                elementObjectArray[index].GetComponent<Element>().setGreenMaterial();
                break;
            }
            else{
                bartext.SetText($"No");
                yield return delay2;
                yield return new WaitUntil(() => pause == false);
                bartext.SetText($"{key} not found at index {index}");
                //Droping the box down
                yield return StartCoroutine(elementObjectArray[index].GetComponent<Element>().DropElementDown(algoSpeed1to10));
                elementObjectArray[index].GetComponent<Element>().setRedMaterial();
                yield return delay1;
                yield return new WaitUntil(() => pause == false);
                index++;
            }
        }
        if(index>= numOfElements){
            bartext.SetText($"{key} not present in the available elements");
        }
        yield return null;
    }

    public void setAlgoSpeed(float algoSpeed){
        delay1 = new WaitForSeconds(0.01f + (1 - algoSpeed) * 1);
        delay2 = new WaitForSeconds(0.02f + (1 - algoSpeed) * 2);
        delay3 = new WaitForSeconds(0.03f + (1 - algoSpeed) * 3);
        delay4 = new WaitForSeconds(0.04f + (1 - algoSpeed) * 4);
        delay5 = new WaitForSeconds(0.05f + (1 - algoSpeed) * 5);
        delay10 = new WaitForSeconds(0.1f + (1 - algoSpeed) * 10);
        delay15 = new WaitForSeconds(0.15f + (1 - algoSpeed) * 15);
        delay20 = new WaitForSeconds(0.2f + (1 - algoSpeed) * 20);
        delay30 = new WaitForSeconds(0.3f + (1 - algoSpeed) * 30);
        algoSpeed1to10 = 1 + algoSpeed * 9;
        CameraFollow.speed = algoSpeed1to10;
        agent.speed = algoSpeed1to10;
    }
}
