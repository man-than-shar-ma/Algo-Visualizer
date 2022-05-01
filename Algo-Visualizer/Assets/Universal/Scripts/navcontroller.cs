using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavController : MonoBehaviour
{
    Camera cam;
    NavMeshAgent agent;

    void Start() {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        agent = gameObject.GetComponent<NavMeshAgent>();
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
    }

    public static void moveToVector3(NavMeshAgent agent, Vector3 pos){
        agent.SetDestination(pos);
    }
}
