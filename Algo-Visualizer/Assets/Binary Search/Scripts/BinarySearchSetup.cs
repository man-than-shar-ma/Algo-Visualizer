using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AI;


public class BinarySearchSetup : MonoBehaviour
{
    [SerializeField] public TMP_InputField arraysizeCustom;

    [SerializeField] public TMP_InputField arrayValuesCustom;

    [SerializeField] public TMP_InputField arrayKeyCustom;

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
    public void StartBinarySearchSetup()
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


        bartext.SetText("Binary Search Algorithm");

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
        System.Array.Sort(elementArray);

        //checking whether user has entered the key value or not
        if(arrayKeyCustom.text.Trim().Length !=0){
            key = int.Parse(arrayKeyCustom.text);
        }
        else{
            key = itemToFind(elementArray);
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
        float x = startposx;
        float y = startposy;
        float z = startposz-1;

        var leftObject = Instantiate(element, new Vector3(x, y, z), Quaternion.identity);
        leftObject.name = $"Left";
        leftObject.transform.parent = pointerHolder.transform;
        leftObject.setElementValue("Left");

        var rightObject = Instantiate(element, new Vector3(x+numOfElements-1, y, z), Quaternion.identity);
        rightObject.name = $"Right";
        rightObject.transform.parent = pointerHolder.transform;
        rightObject.setElementValue("Right");
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

        Transform leftTrans = pointerHolder.transform.Find("Left");
        leftTrans.position = new Vector3(x,y,z);
        leftTrans.GetComponent<Element>().setDefaultMaterial();

        Transform rightTrans = pointerHolder.transform.Find("Right");
        rightTrans.position = new Vector3(x+numOfElements-1, y, z);
        rightTrans.GetComponent<Element>().setDefaultMaterial();
        
        StartCoroutine(BinarySearch());
    }

    public void InvertPause(){
        pause = !pause;
    }

    IEnumerator BinarySearch(){
        bartext.SetText($"Item to find : {key}");
        
        yield return delay5;
        yield return new WaitUntil(() => pause == false);

        float x = startposx;
        float y = startposy;
        float z = startposz;
        
        Transform leftTrans = pointerHolder.transform.Find("Left");
        Transform rightTrans = pointerHolder.transform.Find("Right");

        int leftPos = (int)leftTrans.position.x;
        int rightPos = (int)rightTrans.position.x;

        int left = 0;
        int right = elementArray.Length-1;

        while(left <= right){
            int midPos = (leftPos + rightPos)/2;
            int mid = (left + right)/2;
            
            Vector3 pos = new Vector3(midPos, y, z);
            bartext.SetText($"Moving to index {mid}");

            elementObjectArray[mid].GetComponent<Element>().setYellowMaterial();
            agent.GetComponent<NavController>().moveToVector3(pos);
            yield return new WaitUntil(() => (agent.transform.position - pos).magnitude < 0.1);
            yield return delay1;

            //looking at the box
            Vector3 lookpos = elementObjectArray[mid].position;
            yield return StartCoroutine(agent.GetComponent<NavController>().lookAtPoint(lookpos, algoSpeed1to10));
            yield return delay1;
            yield return new WaitUntil(() => pause == false);

            //Lifting the box up            
            yield return StartCoroutine(elementObjectArray[mid].GetComponent<Element>().LiftElementUp(algoSpeed1to10));
            yield return delay4;
            yield return new WaitUntil(() => pause == false);

            bartext.SetText($"is {elementArray[mid]} == {key} ?");
            yield return delay4;
            yield return new WaitUntil(() => pause == false);

            if(elementArray[mid] == key){
                bartext.SetText($"Yes");
                yield return delay2;
                yield return new WaitUntil(() => pause == false);
                bartext.SetText($"{key} found at index {mid}");
                elementObjectArray[mid].GetComponent<Element>().setGreenMaterial();
                break;
            }
            else{
                bartext.SetText($"No");
                yield return delay2;
                yield return new WaitUntil(() => pause == false);

                bartext.SetText($"is {elementArray[mid]} < {key} ?");
                yield return delay4;
                yield return new WaitUntil(() => pause == false);

                if(elementArray[mid] < key){
                    bartext.SetText($"Yes");
                    yield return delay2;
                    yield return new WaitUntil(() => pause == false);
                    bartext.SetText($"{key} should be after index {mid}");

                    elementObjectArray[mid].GetComponent<Element>().setRedMaterial();

                    //Droping the box down
                    yield return StartCoroutine(elementObjectArray[mid].GetComponent<Element>().DropElementDown(algoSpeed1to10));
                    yield return delay4;
                    yield return new WaitUntil(() => pause == false);

                    pos = new Vector3(leftPos, y, z);
                    bartext.SetText($"Moving Left pointer to index {mid} + 1 = {mid+1}");
                    leftTrans.GetComponent<Element>().setYellowMaterial();
                    agent.GetComponent<NavController>().moveToVector3(pos);
                    yield return new WaitUntil(() => (agent.transform.position - pos).magnitude < 0.1);
                    yield return delay1;

                    //looking at the box
                    lookpos = leftTrans.position;
                    yield return StartCoroutine(agent.GetComponent<NavController>().lookAtPoint(lookpos, algoSpeed1to10));
                    yield return delay1;
                    yield return new WaitUntil(() => pause == false);

                    //picking left pointer
                    agent.GetComponent<NavController>().PickObject(leftTrans, "right");
                    yield return delay1;
                    yield return new WaitUntil(() => pause == false);

                    int oldLeft = left;
                    left = mid + 1;
                    leftPos = midPos + 1;

                    StartCoroutine(colorRedLTOR(oldLeft, mid));

                    pos = new Vector3(leftPos, y, z);
                    agent.GetComponent<NavController>().moveToVector3(pos);
                    yield return new WaitUntil(() => (agent.transform.position - pos).magnitude < 0.1);
                    yield return delay1;

                    //looking at the drop location
                    lookpos = new Vector3(leftPos, y, z-1);
                    yield return StartCoroutine(agent.GetComponent<NavController>().lookAtPoint(lookpos, algoSpeed1to10));
                    yield return delay1;
                    yield return new WaitUntil(() => pause == false);

                    //droping left pointer
                    agent.GetComponent<NavController>().DropObject(lookpos, pointerHolder.transform, "right");
                    yield return delay1;
                    yield return new WaitUntil(() => pause == false);

                    leftTrans.GetComponent<Element>().setDefaultMaterial();

                }
                else {
                    bartext.SetText($"No");
                    yield return delay2;
                    yield return new WaitUntil(() => pause == false);
                    bartext.SetText($"{key} should be before index {mid}");

                    elementObjectArray[mid].GetComponent<Element>().setRedMaterial();

                    //Droping the box down
                    yield return StartCoroutine(elementObjectArray[mid].GetComponent<Element>().DropElementDown(algoSpeed1to10));
                    yield return delay4;
                    yield return new WaitUntil(() => pause == false);

                    pos = new Vector3(rightPos, y, z);
                    bartext.SetText($"Moving Right pointer to index {mid} - 1 = {mid-1}");
                    rightTrans.GetComponent<Element>().setYellowMaterial();
                    agent.GetComponent<NavController>().moveToVector3(pos);
                    yield return new WaitUntil(() => (agent.transform.position - pos).magnitude < 0.1);
                    yield return delay1;

                    //looking at the box
                    lookpos = rightTrans.position;
                    yield return StartCoroutine(agent.GetComponent<NavController>().lookAtPoint(lookpos, algoSpeed1to10));
                    yield return delay1;
                    yield return new WaitUntil(() => pause == false);

                    //picking right pointer
                    agent.GetComponent<NavController>().PickObject(rightTrans, "right");
                    yield return delay1;
                    yield return new WaitUntil(() => pause == false);

                    int oldRight = right;
                    right = mid - 1;
                    rightPos = midPos - 1;

                    StartCoroutine(colorRedRTOL(oldRight, mid));

                    pos = new Vector3(rightPos, y, z);
                    agent.GetComponent<NavController>().moveToVector3(pos);
                    yield return new WaitUntil(() => (agent.transform.position - pos).magnitude < 0.1);
                    yield return delay1;

                    //looking at the drop location
                    lookpos = new Vector3(rightPos, y, z-1);
                    yield return StartCoroutine(agent.GetComponent<NavController>().lookAtPoint(lookpos, algoSpeed1to10));
                    yield return delay1;
                    yield return new WaitUntil(() => pause == false);

                    //droping right pointer
                    agent.GetComponent<NavController>().DropObject(lookpos, pointerHolder.transform, "right");
                    yield return delay1;
                    yield return new WaitUntil(() => pause == false);

                    rightTrans.GetComponent<Element>().setDefaultMaterial();
                }
            }
        }
        if(left>right){
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

    IEnumerator colorRedLTOR(int oldLeft, int mid){
        for(int i=oldLeft; i<mid; i++){
            elementObjectArray[i].GetComponent<Element>().setRedMaterial();
            yield return null;
        }
    }

    IEnumerator colorRedRTOL(int oldRight, int mid){
        for(int i=oldRight; i>mid; i--){
            elementObjectArray[i].GetComponent<Element>().setRedMaterial();
            yield return null;
        }
    }
}
