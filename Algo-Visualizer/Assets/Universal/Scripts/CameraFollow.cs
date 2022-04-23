using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    private Transform target;
    public float smoothspeed = 10f;
    public Vector3 offset;

    void LateUpdate() {
        if(!target)
            target = GameObject.FindGameObjectWithTag("Player").transform;
        else{
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothspeed * Time.deltaTime);
            transform.position = smoothedPosition;
        }   
    }
}
