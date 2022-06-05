using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AI;


public class SelectionSortSetup : MonoBehaviour
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

    private int[] originalElementArray;
    private Transform[] originalElementObjectArray;

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
    public void StartSelectionSortSetup()
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


        bartext.SetText("Selection Sort Algorithm");

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

        originalElementArray = (int[]) elementArray.Clone();
        originalElementObjectArray = (Transform[]) elementObjectArray.Clone();
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
        float x = startposx;
        float y = startposy;
        float z = startposz-1;

        var startObject = Instantiate(element, new Vector3(x, y, z), Quaternion.identity);
        startObject.name = $"Start";
        startObject.tag = "Start";
        startObject.transform.parent = pointerHolder.transform;
        startObject.setElementValue("Start");
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

        if(agent.GetComponent<NavController>().isholdingLeft){
            GameObject gObj = agent.GetComponent<NavController>().returnHoldedGameObject("left");
            if(gObj.CompareTag("Start")){
                agent.GetComponent<NavController>().DropObject(Vector3.zero, pointerHolder.transform, "left");
            }
            else if(gObj.CompareTag("Element")){
                agent.GetComponent<NavController>().DropObject(Vector3.zero, elementsHolder.transform, "left");
            }
        }

        if(agent.GetComponent<NavController>().isholdingRight){
            GameObject gObj = agent.GetComponent<NavController>().returnHoldedGameObject("right");
            if(gObj.CompareTag("Start")){
                agent.GetComponent<NavController>().DropObject(Vector3.zero, pointerHolder.transform, "right");
            }
            else if(gObj.CompareTag("Element")){
                agent.GetComponent<NavController>().DropObject(Vector3.zero, elementsHolder.transform, "right");
            }
        }

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

        Transform endTrans = pointerHolder.transform.Find("Start");
        endTrans.position = new Vector3(x, y, z);
        endTrans.GetComponent<Element>().setDefaultMaterial();
        
        StartCoroutine(SelectionSort());
    }

    public void InvertPause(){
        pause = !pause;
    }

    IEnumerator SelectionSort(){
        // bartext.SetText($"Item to find : {key}");
        
        // yield return delay5;
        // yield return new WaitUntil(() => pause == false);

        float x = startposx;
        float y = startposy;
        float z = startposz;
        
        Transform startTrans = pointerHolder.transform.Find("Start");

        float startPos = startTrans.position.x;
        int start = 0;
        
        int elementArrayLength = elementArray.Length;
        // Debug.Log("hi");
        // Debug.Log(elementArrayLength);
        int step = 0;
        int i = 0;
        for(step = 0; step < elementArrayLength - 1; step++){

            int minIndex = step;
            float minPos = elementObjectArray[minIndex].position.x;

            // moving to element at start pointer
            Vector3 pos = new Vector3(elementObjectArray[minIndex].position.x, y, z);
            bartext.SetText($"Setting element at Start index {step} as minimum");
            elementObjectArray[minIndex].GetComponent<Element>().setYellowMaterial();
            agent.GetComponent<NavController>().moveToVector3(pos);
            yield return delay1;
            yield return new WaitUntil(() => (agent.transform.position - pos).magnitude < 0.1);

            //looking at the box
            Vector3 lookpos = elementObjectArray[minIndex].position;
            yield return StartCoroutine(agent.GetComponent<NavController>().lookAtPoint(lookpos, algoSpeed1to10));
            yield return delay1;
            yield return new WaitUntil(() => pause == false);

            
            //Lifting the box 1 up            
            yield return StartCoroutine(elementObjectArray[minIndex].GetComponent<Element>().LiftElementUp(algoSpeed1to10));
            yield return delay2;
            yield return new WaitUntil(() => pause == false);
            
            for(i = step + 1; i < elementArrayLength; i++){

                //moving to index i
                pos = new Vector3 (elementObjectArray[i].position.x, y, z);
                bartext.SetText($"Checking element at index {i}");
                elementObjectArray[i].GetComponent<Element>().setYellowMaterial();
                agent.GetComponent<NavController>().moveToVector3(pos);
                yield return new WaitUntil(() => (agent.transform.position - pos).magnitude < 0.1);
                yield return delay1;

                //looking at the box i
                lookpos = elementObjectArray[i].position;
                yield return StartCoroutine(agent.GetComponent<NavController>().lookAtPoint(lookpos, algoSpeed1to10));
                yield return delay1;
                yield return new WaitUntil(() => pause == false);

                //Lifting the box i up            
                yield return StartCoroutine(elementObjectArray[i].GetComponent<Element>().LiftElementUp(algoSpeed1to10));
                yield return delay2;
                yield return new WaitUntil(() => pause == false);

                //Finding min element between index i and minindex
                bartext.SetText($"is {elementArray[i]} < {elementArray[minIndex]} ?");
                yield return delay4;
                yield return new WaitUntil(() => pause == false);

                if(elementArray[i] < elementArray[minIndex]){
                    bartext.SetText($"Yes");
                    yield return delay2;
                    yield return new WaitUntil(() => pause == false);

                    bartext.SetText($"Setting element at index {i} as minimum");
                    yield return delay1;
                    yield return new WaitUntil(() => pause == false);

                    //droping old_min box down
                    yield return StartCoroutine(elementObjectArray[minIndex].GetComponent<Element>().DropElementDown(algoSpeed1to10));
                    elementObjectArray[minIndex].GetComponent<Element>().setDefaultMaterial();
                    yield return delay2;
                    yield return new WaitUntil(() => pause == false);

                    minIndex = i;  
                    minPos = elementObjectArray[minIndex].position.x;
                }
                else{
                    bartext.SetText($"No");
                    yield return delay2;
                    yield return new WaitUntil(() => pause == false);

                    //droping box at index i down
                    yield return StartCoroutine(elementObjectArray[i].GetComponent<Element>().DropElementDown(algoSpeed1to10));
                    elementObjectArray[i].GetComponent<Element>().setDefaultMaterial();
                    yield return delay2;
                    yield return new WaitUntil(() => pause == false);                    
                }
            }

            //swap min index and start
            bartext.SetText($"Swap element {minIndex} with {step}");
            yield return delay2;
            yield return new WaitUntil(() => pause == false);

            //moving to min index if its not at end
            if(minIndex != elementArrayLength - 1){
                //moving to min index
                pos = new Vector3 (elementObjectArray[minIndex].position.x, y, z);
                agent.GetComponent<NavController>().moveToVector3(pos);
                yield return new WaitUntil(() => (agent.transform.position - pos).magnitude < 0.1);
                yield return delay1;

                //looking at min index box
                lookpos = elementObjectArray[minIndex].position;
                yield return StartCoroutine(agent.GetComponent<NavController>().lookAtPoint(lookpos, algoSpeed1to10));
                yield return delay1;
                yield return new WaitUntil(() => pause == false);
            }

            //picking box at min index at left hand
            agent.GetComponent<NavController>().PickObject(elementObjectArray[minIndex], "left");
            yield return delay1;
            yield return new WaitUntil(() => pause == false);

            if(minIndex == step){
                //droping box at step / start
                pos = new Vector3(startPos, y, z+1);
                agent.GetComponent<NavController>().DropObject(pos, elementsHolder.transform, "left");
                elementObjectArray[minIndex].GetComponent<Element>().setDefaultMaterial();
                yield return delay1;
                yield return new WaitUntil(() => pause == false);
            }
            else{
                //moving to step index
                pos = new Vector3 (elementObjectArray[step].position.x, y, z);
                elementObjectArray[step].GetComponent<Element>().setYellowMaterial();
                agent.GetComponent<NavController>().moveToVector3(pos);
                yield return new WaitUntil(() => (agent.transform.position - pos).magnitude < 0.1);
                yield return delay1;

                //looking at the box step
                lookpos = elementObjectArray[step].position;
                yield return StartCoroutine(agent.GetComponent<NavController>().lookAtPoint(lookpos, algoSpeed1to10));
                yield return delay1;
                yield return new WaitUntil(() => pause == false);

                //picking box step at right hand
                agent.GetComponent<NavController>().PickObject(elementObjectArray[step], "right");
                yield return delay1;
                yield return new WaitUntil(() => pause == false);

                //droping min element from left hand
                pos = new Vector3(startPos, y, z+1);
                agent.GetComponent<NavController>().DropObject(pos, elementsHolder.transform, "left");
                elementObjectArray[minIndex].GetComponent<Element>().setDefaultMaterial();
                yield return delay1;
                yield return new WaitUntil(() => pause == false);

                //moving to the empty position
                pos = new Vector3 (minPos, y, z);
                agent.GetComponent<NavController>().moveToVector3(pos);
                yield return new WaitUntil(() => (agent.transform.position - pos).magnitude < 0.1);
                yield return delay1;

                //look at the position
                lookpos = new Vector3(minPos, y, z+1);
                yield return StartCoroutine(agent.GetComponent<NavController>().lookAtPoint(lookpos, algoSpeed1to10));
                yield return delay1;
                yield return new WaitUntil(() => pause == false);

                //droping box from right hand
                pos = new Vector3(minPos, y, z+1);
                agent.GetComponent<NavController>().DropObject(pos, elementsHolder.transform, "right");
                elementObjectArray[step].GetComponent<Element>().setDefaultMaterial();
                yield return delay1;
                yield return new WaitUntil(() => pause == false);

                Transform tempElementObject = elementObjectArray[step];
                elementObjectArray[step] = elementObjectArray[minIndex];
                elementObjectArray[minIndex] = tempElementObject;

                int tempElement = elementArray[step];
                elementArray[step] = elementArray[minIndex];
                elementArray[minIndex] = tempElement;
                yield return new WaitUntil(() => pause == false);
            }


            Vector3 spos = new Vector3(startPos, y, z);
            bartext.SetText($"Increasing Start pointer by 1");
            startTrans.GetComponent<Element>().setYellowMaterial();
            agent.GetComponent<NavController>().moveToVector3(spos);
            yield return new WaitUntil(() => (agent.transform.position - spos).magnitude < 0.1);
            yield return delay1;

            //looking at the box
            Vector3 slookpos = startTrans.position;
            yield return StartCoroutine(agent.GetComponent<NavController>().lookAtPoint(slookpos, algoSpeed1to10));
            yield return delay1;
            yield return new WaitUntil(() => pause == false);

            //picking start pointer
            agent.GetComponent<NavController>().PickObject(startTrans, "right");
            yield return delay1;
            yield return new WaitUntil(() => pause == false);

            startPos++;
            
            StartCoroutine(colorGreenBG(start++));

            spos = new Vector3(startPos, y, z);
            agent.GetComponent<NavController>().moveToVector3(spos);
            yield return new WaitUntil(() => (agent.transform.position - spos).magnitude < 0.1);
            yield return delay1;

            //looking at the drop location
            slookpos = new Vector3(startPos, y, z-1);
            yield return StartCoroutine(agent.GetComponent<NavController>().lookAtPoint(slookpos, algoSpeed1to10));
            yield return delay1;
            yield return new WaitUntil(() => pause == false);

            //droping start pointer
            agent.GetComponent<NavController>().DropObject(slookpos, pointerHolder.transform, "right");
            yield return delay1;
            yield return new WaitUntil(() => pause == false);

            startTrans.GetComponent<Element>().setDefaultMaterial();
            yield return delay1;
            yield return new WaitUntil(() => pause == false);

        }

        StartCoroutine(colorGreenBG(start++));
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

    IEnumerator colorGreenBG(int index){
        elementObjectArray[index].GetComponent<Element>().setGreenMaterial();
        yield return null;
    }
}
