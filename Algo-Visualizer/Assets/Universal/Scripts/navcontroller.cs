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

    void Start() {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        agent = gameObject.GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
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

    public IEnumerator lookAtPoint(Vector3 pos){
        float degreesPerSecond = 90 * Time.deltaTime;
        Vector3 direction = pos - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        while(transform.rotation != targetRotation){
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, degreesPerSecond);
            yield return null;
        }

    }
}
