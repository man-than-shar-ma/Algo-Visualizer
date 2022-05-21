using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AI;

public class LinearSearchSetup : MonoBehaviour
{
    [SerializeField] private int numOfElements;

    [SerializeField] private TMP_InputField arraysizeCustom;

    [SerializeField] private Element element;

    [SerializeField] private GameObject elementsHolder;
    [SerializeField] private GameObject tilesHolder;

    [SerializeField] private TextMeshProUGUI bartext;

    [Range(1f, 0.01f)]
    [SerializeField] private float algoSpeed = 1;
    

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
        if(arraysizeCustom.text != ""){
            numOfElements = int.Parse(arraysizeCustom.text);
        }
        delay1 = new WaitForSeconds(1 * algoSpeed);
        delay2 = new WaitForSeconds(2 * algoSpeed);
        delay3 = new WaitForSeconds(3 * algoSpeed);
        delay4 = new WaitForSeconds(4 * algoSpeed);
        delay5 = new WaitForSeconds(5 * algoSpeed);
        delay10 = new WaitForSeconds(10 * algoSpeed);
        delay15 = new WaitForSeconds(15 * algoSpeed);
        delay20 = new WaitForSeconds(20 * algoSpeed);
        delay30 = new WaitForSeconds(30 * algoSpeed);

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
        while(i<numOfElements)
            elementObjectArray[i++].position = new Vector3(x++,y,z);
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
            agent.GetComponent<NavController>().moveToVector3(pos);
            // Debug.Log(pos + " " + agent.transform.position);
            yield return new WaitUntil(() => (agent.transform.position - pos).magnitude < 0.1);
            yield return delay1;
            
            //looking at the box
            Vector3 lookpos = elementObjectArray[index].position;
            yield return StartCoroutine(agent.GetComponent<NavController>().lookAtPoint(lookpos));
            yield return delay1;
            yield return new WaitUntil(() => pause == false);


            //Lifting the box up            
            yield return StartCoroutine(elementObjectArray[index].GetComponent<Element>().LiftElementUp());
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
                elementObjectArray[index].GetComponent<Element>().setGreenMaterial();
                break;
            }
            else{
                bartext.SetText($"No");
                yield return delay2;
                yield return new WaitUntil(() => pause == false);
                bartext.SetText($"{key} not found at index {index}");
                elementObjectArray[index].GetComponent<Element>().setRedMaterial();

                //Droping the box down
                StartCoroutine(elementObjectArray[index].GetComponent<Element>().DropElementDown());

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

    public void setAlgoSpeed(float algoSpeed){
        delay1 = new WaitForSeconds(1 * algoSpeed);
        delay2 = new WaitForSeconds(2 * algoSpeed);
        delay3 = new WaitForSeconds(3 * algoSpeed);
        delay4 = new WaitForSeconds(4 * algoSpeed);
        delay5 = new WaitForSeconds(5 * algoSpeed);
        delay10 = new WaitForSeconds(10 * algoSpeed);
        delay15 = new WaitForSeconds(15 * algoSpeed);
        delay20 = new WaitForSeconds(20 * algoSpeed);
        delay30 = new WaitForSeconds(30 * algoSpeed);
    }
}
