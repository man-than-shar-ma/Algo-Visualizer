using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FindNearestTile : MonoBehaviour
{
    public static Vector3 nearestTile(NavMeshAgent agent, Vector3 toTile, int offset){
        NavMeshPath path = new NavMeshPath();
        Vector3 fromTile = agent.transform.position;

        Vector3[] possibleOption = {
            new Vector3(toTile.x, toTile.y, toTile.z - offset),
            new Vector3(toTile.x - offset, toTile.y, toTile.z),
            new Vector3(toTile.x + offset, toTile.y, toTile.z),
            new Vector3(toTile.x, toTile.y, toTile.z + offset)
        };        
        
        Vector3 targetTile = Vector3.zero;
        float minDis = System.Int32.MaxValue;

        bool isPath;
        float calcDis;
        
        for(int i=0; i<possibleOption.Length; i++){
            isPath = agent.CalculatePath(possibleOption[i], path);

            calcDis = Vector3.Distance(fromTile, toTile);
            if(isPath &&  calcDis < minDis){
                targetTile = possibleOption[i];
                minDis = calcDis;
            }
        }

        return targetTile;
    }
}
