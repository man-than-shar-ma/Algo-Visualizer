using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI.Navigation;
using UnityEngine.AI;

public class RuntimeNavmesh : MonoBehaviour
{
    // Start is called before the first frame update
    public NavMeshSurface surface;
    public GameObject agentPrefab;
    private GameObject agent;

    void Start()
    {
        //will create a navmesh surface on the generated tiles and instantiate an agent
        surface.BuildNavMesh();
        agent = Instantiate(agentPrefab, Vector3.zero, Quaternion.identity);
        agent.name = "Agent";
        agent.tag = "Player";
    }

    // Update is called once per frame
    // void Update()
    // {
        
    // }
}
