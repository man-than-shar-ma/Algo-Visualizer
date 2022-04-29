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

    public void buildMeshandAgent(){
        surface.BuildNavMesh();
        agent = Instantiate(agentPrefab, Vector3.zero, Quaternion.identity);
        agent.name = "Agent";
        agent.tag = "Player";
    }
}
