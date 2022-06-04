using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AI;


public class BubbleSortSetup : MonoBehaviour
{
    [SerializeField] public TMP_InputField arraysizeCustom;

    [SerializeField] public TMP_InputField arrayValuesCustom;

    [SerializeField] public TMP_InputField xxx;

    [SerializeField] private int numOfElements;

    [SerializeField] private Element element;

    [SerializeField] private GameObject elementsHolder;
    [SerializeField] private GameObject tilesHolder;
    [SerializeField] private GameObject pointerHolder;

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
    public void StartBubbleSortSetup()
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


        bartext.SetText("Bubble Sort Algorithm");

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

        elementObjectArray = new Transform[numOfElements];
        generateElements();

        generatePointers();
        
        // Vector3 originalPos = elementObjectArray[0].position;
        // agent.GetComponent<NavController>().PickObject(elementObjectArray[0], "right");
        // agent.GetComponent<NavController>().DropObject(originalPos, elementsHolder.transform);
    }

    void generateElements(){
        float x = startposx;
        float y = startposy;
        float z = startposz+1;

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

    void generatePointers(){
        float x = startposx+numOfElements-1;
        float y = startposy;
        float z = startposz-1;

        var endObject = Instantiate(element, new Vector3(x, y, z), Quaternion.identity);
        endObject.name = $"End";
        endObject.transform.parent = pointerHolder.transform;
        endObject.setElementValue("End");
    }

    void fillRandomData(int[] arr){
        for( int i = 0; i<arr.Length; i++){
            arr[i] = Random.Range(1,101);
        }
    }

    void setTileIndex(){
        int x = (int)startposx;
        int y = (int)startposz;
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
        float z = startposz+1;
        int i=0;
        while(i<numOfElements){
            elementObjectArray[i].position = new Vector3(x++,y,z);
            elementObjectArray[i].GetComponent<Element>().setDefaultMaterial();
            i++;
        }
        x = startposx;
        y = startposy;
        z = startposz-1;

        if(agent.GetComponent<NavController>().isholdingRight)
            agent.GetComponent<NavController>().DropObject(Vector3.zero, pointerHolder.transform, "right");

        Transform endTrans = pointerHolder.transform.Find("End");
        endTrans.position = new Vector3(x+numOfElements-1, y, z);
        endTrans.GetComponent<Element>().setDefaultMaterial();
        
        StartCoroutine(BubbleSort());
    }

    public void InvertPause(){
        pause = !pause;
    }

    IEnumerator BubbleSort(){
        // bartext.SetText($"Item to find : {key}");
        
        // yield return delay5;
        // yield return new WaitUntil(() => pause == false);

        float x = startposx;
        float y = startposy;
        float z = startposz;
        
        Transform endTrans = pointerHolder.transform.Find("End");

        int endPos = (int)endTrans.position.x;
        int end = elementArray.Length - 1;
        
        int elementArrayLength = elementArray.Length;
        // Debug.Log("hi");
        // Debug.Log(elementArrayLength);
        int step = 0;
        int i = 0;
        for(step = 0; step < elementArrayLength - 1; step++){
            for(i = 0; i < elementArrayLength - step - 1; i++){
                Vector3 pos1 = elementObjectArray[i].position;
                Vector3 pos2 = elementObjectArray[i+1].position;

                // Debug.Log("hi");
                float midloc = (pos1.x + pos2.x) / 2;
                Vector3 pos = new Vector3(midloc, y, z);
                bartext.SetText($"Checking element at index {i} and {i+1}");
                elementObjectArray[i].GetComponent<Element>().setYellowMaterial();
                elementObjectArray[i+1].GetComponent<Element>().setYellowMaterial();
                agent.GetComponent<NavController>().moveToVector3(pos);
                yield return new WaitUntil(() => (agent.transform.position - pos).magnitude < 0.1);
                yield return delay1;

                Vector3 lookpos = Vector3.zero;

                //looking at the box 1
                lookpos = pos1;
                yield return StartCoroutine(agent.GetComponent<NavController>().lookAtPoint(lookpos, algoSpeed1to10));
                yield return delay1;
                yield return new WaitUntil(() => pause == false);

                //Lifting the box 1 up            
                yield return StartCoroutine(elementObjectArray[i].GetComponent<Element>().LiftElementUp(algoSpeed1to10));
                yield return delay2;
                yield return new WaitUntil(() => pause == false);

                //looking at the box 2
                lookpos = pos2;
                yield return StartCoroutine(agent.GetComponent<NavController>().lookAtPoint(lookpos, algoSpeed1to10));
                yield return delay1;
                yield return new WaitUntil(() => pause == false);

                //Lifting the box 2 up            
                yield return StartCoroutine(elementObjectArray[i+1].GetComponent<Element>().LiftElementUp(algoSpeed1to10));
                yield return delay2;
                yield return new WaitUntil(() => pause == false);

                //looking between boxes
                lookpos = Vector3.Lerp(pos1, pos2, 0.5f);
                yield return StartCoroutine(agent.GetComponent<NavController>().lookAtPoint(lookpos, algoSpeed1to10));
                yield return delay1;
                yield return new WaitUntil(() => pause == false);

                bartext.SetText($"is {elementArray[i]} > {elementArray[i+1]} ?");
                yield return delay4;
                yield return new WaitUntil(() => pause == false);

                if(elementArray[i] > elementArray[i+1]){

                    bartext.SetText($"Yes");
                    yield return delay2;
                    yield return new WaitUntil(() => pause == false);

                    bartext.SetText($"Swap {elementArray[i]} with {elementArray[i+1]}");
                    yield return delay1;
                    yield return new WaitUntil(() => pause == false);

                    //looking at the box 1
                    lookpos = pos1;
                    yield return StartCoroutine(agent.GetComponent<NavController>().lookAtPoint(lookpos, algoSpeed1to10));
                    yield return delay1;
                    yield return new WaitUntil(() => pause == false);
                    
                    //picking box 1 in left hand
                    agent.GetComponent<NavController>().PickObject(elementObjectArray[i], "left");
                    yield return delay1;
                    yield return new WaitUntil(() => pause == false);

                    //looking at the box 2
                    lookpos = pos2;
                    yield return StartCoroutine(agent.GetComponent<NavController>().lookAtPoint(lookpos, algoSpeed1to10));
                    yield return delay1;
                    yield return new WaitUntil(() => pause == false);

                    //picking box 2 in right hand
                    agent.GetComponent<NavController>().PickObject(elementObjectArray[i+1], "right");
                    yield return delay1;
                    yield return new WaitUntil(() => pause == false);


                    //looking at the pos 1
                    lookpos = pos1;
                    yield return StartCoroutine(agent.GetComponent<NavController>().lookAtPoint(lookpos, algoSpeed1to10));
                    yield return delay1;
                    yield return new WaitUntil(() => pause == false);

                    //droping box from right hand
                    agent.GetComponent<NavController>().DropObject(lookpos, pointerHolder.transform, "right");
                    yield return delay1;
                    yield return new WaitUntil(() => pause == false);

                    //looking at the pos 2
                    lookpos = pos2;
                    yield return StartCoroutine(agent.GetComponent<NavController>().lookAtPoint(lookpos, algoSpeed1to10));
                    yield return delay1;
                    yield return new WaitUntil(() => pause == false);

                    //droping box from left hand
                    agent.GetComponent<NavController>().DropObject(lookpos, pointerHolder.transform, "left");
                    yield return delay1;
                    yield return new WaitUntil(() => pause == false);

                    // for(int d=0;d<elementObjectArray.Length;d++){
                    //     Debug.Log(elementObjectArray[d].name);
                    // }
                    
                    // Debug.Log("--------------");
                    Transform tempElementObject = elementObjectArray[i];
                    elementObjectArray[i] = elementObjectArray[i+1];
                    elementObjectArray[i+1] = tempElementObject;

                    int tempElement = elementArray[i];
                    elementArray[i] = elementArray[i+1];
                    elementArray[i+1] = tempElement;

                    // for(int d=0;d<elementObjectArray.Length;d++){
                    //     Debug.Log(elementObjectArray[d].name);
                    // }
                    yield return new WaitUntil(() => pause == false);

                }
                else{

                    bartext.SetText($"No");
                    yield return delay2;
                    yield return new WaitUntil(() => pause == false);

                    //looking at the box 1
                    lookpos = pos1;
                    yield return StartCoroutine(agent.GetComponent<NavController>().lookAtPoint(lookpos, algoSpeed1to10));
                    yield return delay1;
                    yield return new WaitUntil(() => pause == false);

                    //Dropping box 1 down           
                    yield return StartCoroutine(elementObjectArray[i].GetComponent<Element>().DropElementDown(algoSpeed1to10));
                    yield return delay2;
                    yield return new WaitUntil(() => pause == false);

                    //looking at the box 2
                    lookpos = pos2;
                    yield return StartCoroutine(agent.GetComponent<NavController>().lookAtPoint(lookpos, algoSpeed1to10));
                    yield return delay1;
                    yield return new WaitUntil(() => pause == false);

                    //Dropping box 1 down           
                    yield return StartCoroutine(elementObjectArray[i+1].GetComponent<Element>().DropElementDown(algoSpeed1to10));
                    yield return delay2;
                    yield return new WaitUntil(() => pause == false);
                }
                elementObjectArray[i].GetComponent<Element>().setDefaultMaterial();
                elementObjectArray[i+1].GetComponent<Element>().setDefaultMaterial();
            }

            Vector3 epos = new Vector3(endPos, y, z);
            bartext.SetText($"Decreasing End pointer by 1");
            endTrans.GetComponent<Element>().setYellowMaterial();
            agent.GetComponent<NavController>().moveToVector3(epos);
            yield return new WaitUntil(() => (agent.transform.position - epos).magnitude < 0.1);
            yield return delay1;

            //looking at the box
            Vector3 elookpos = endTrans.position;
            yield return StartCoroutine(agent.GetComponent<NavController>().lookAtPoint(elookpos, algoSpeed1to10));
            yield return delay1;
            yield return new WaitUntil(() => pause == false);

            //picking end pointer
            agent.GetComponent<NavController>().PickObject(endTrans, "right");
            yield return delay1;
            yield return new WaitUntil(() => pause == false);

            endPos--;
            
            StartCoroutine(colorRedBG(end--));

            epos = new Vector3(endPos, y, z);
            agent.GetComponent<NavController>().moveToVector3(epos);
            yield return new WaitUntil(() => (agent.transform.position - epos).magnitude < 0.1);
            yield return delay1;

            //looking at the drop location
            elookpos = new Vector3(endPos, y, z-1);
            yield return StartCoroutine(agent.GetComponent<NavController>().lookAtPoint(elookpos, algoSpeed1to10));
            yield return delay1;
            yield return new WaitUntil(() => pause == false);

            //droping end pointer
            agent.GetComponent<NavController>().DropObject(elookpos, pointerHolder.transform, "right");
            yield return delay1;
            yield return new WaitUntil(() => pause == false);

            endTrans.GetComponent<Element>().setDefaultMaterial();
            yield return delay1;
            yield return new WaitUntil(() => pause == false);

        }

        StartCoroutine(colorRedBG(end--));
        bartext.SetText($"Array Completely Sorted");
        yield return delay1;
        yield return new WaitUntil(() => pause == false);
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

    IEnumerator colorRedBG(int index){
        elementObjectArray[index].GetComponent<Element>().setGreenMaterial();
        yield return null;
    }
}
