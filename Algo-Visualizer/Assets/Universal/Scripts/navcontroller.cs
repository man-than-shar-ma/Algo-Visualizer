using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

public class NavController : MonoBehaviour
{
    Camera cam;
    NavMeshAgent agent;

    [SerializeField] ThirdPersonCharacter character;

    [SerializeField] Transform PickObjectTransfomLeft;
    [SerializeField] Transform PickObjectTransfomRight;

    public bool isholdingLeft = false;
    public bool isholdingRight = false;

    AudioSource pickBoxSound;
    AudioSource dropBoxSound;
    GameObject boxSoundsObj;
  

    void Start() {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        agent = gameObject.GetComponent<NavMeshAgent>();
        agent.updateRotation = false;

        boxSoundsObj = GameObject.FindGameObjectWithTag("PickDropSounds");
        pickBoxSound = boxSoundsObj.transform.GetChild(0).GetComponent<AudioSource>();
        dropBoxSound = boxSoundsObj.transform.GetChild(1).GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // if(Input.GetMouseButtonDown(0)){
        //     Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        //     RaycastHit hit;

        //     if(Physics.Raycast(ray, out hit)){
        //         agent.SetDestination(hit.point);
        //     }
        // }
        
        if(agent.remainingDistance > agent.stoppingDistance){
            character.Move(agent.desiredVelocity, false, false);
        }
        else{
            character.Move(Vector3.zero, false, false);
        }
    }

    public void moveToVector3(Vector3 pos){
        transform.GetComponent<NavMeshAgent>().SetDestination(pos);
    }

    public IEnumerator lookAtPoint(Vector3 orgPos, float speed = 1f){
        Vector3 pos = new Vector3(orgPos.x, transform.position.y, orgPos.z);
        float degreesPerSecond = 90 * Time.deltaTime * speed;
        Vector3 direction = pos - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        // (agent.transform.position - pos).magnitude < 0.1;
        while(Quaternion.Angle(transform.rotation, targetRotation) > 0.1f){
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, degreesPerSecond);
            yield return null;
        }
        transform.rotation = targetRotation;

    }

    public void PickObject(Transform gameObjectTransform, string hand){
        gameObjectTransform.GetComponent<BoxCollider>().enabled = false;
        gameObjectTransform.GetComponent<NavMeshObstacle>().enabled = false;
        if(hand == "left")
        {
            gameObjectTransform.parent = PickObjectTransfomLeft;
            isholdingLeft = true;
        }
        else if(hand == "right"){
            gameObjectTransform.parent = PickObjectTransfomRight;
            isholdingRight = true;
        }
        gameObjectTransform.localPosition = Vector3.zero;
        gameObjectTransform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
        gameObjectTransform.localRotation = Quaternion.identity;
        pickBoxSound.Play();
    }

    public void DropObject(Vector3 location, Transform parent, string hand){
        Transform objectTransform = null;
        if(hand == "left")
        {
            objectTransform = PickObjectTransfomLeft.GetChild(0);
            isholdingLeft = false;
        }
        else if(hand == "right"){
            objectTransform = PickObjectTransfomRight.GetChild(0);
            isholdingRight = false;
        }
        objectTransform.parent = parent;
        objectTransform.localPosition = location;
        objectTransform.localScale = Vector3.one;
        objectTransform.localRotation = Quaternion.identity;
        objectTransform.GetComponent<BoxCollider>().enabled = true;
        objectTransform.GetComponent<NavMeshObstacle>().enabled = true;
        dropBoxSound.Play();
    }

    public GameObject returnHoldedGameObject(string hand){
        if(hand == "left")
            return PickObjectTransfomLeft.GetChild(0).gameObject;
        else if(hand == "right")
            return PickObjectTransfomRight.GetChild(0).gameObject;
        return null;
    }
}
