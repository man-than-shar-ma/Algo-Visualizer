using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    private Transform target;
    public float smoothspeed = 2f;

    [SerializeField]
    Vector3 defaultOffset = new Vector3(0,5,-5);

    [SerializeField]
    Vector3 defaultOffsetTop = new Vector3(0,10,-5);


    [Range (-250f, 250f)]
    public float offsetx = 0f;

    [Range (5f, 50f)]
    public float offsety = 5f;

    [Range (-250f, 250f)]
    public float offsetz = -5f;

    [SerializeField]
    bool defaultFollowMode = true;
    [SerializeField]
    bool defaultTopMode = false;

    [Range (0.05f, 1f)]
    public float camControlSpeed = 0.25f;
    

    void FixedUpdate() {
        if(!defaultFollowMode){
            offsetx += Input.GetAxisRaw("Horizontal") * camControlSpeed;
            offsetz += Input.GetAxisRaw("Vertical") * camControlSpeed;
            offsety += Input.GetAxisRaw("Mouse ScrollWheel") * camControlSpeed;
        }        
        if(!target && GameObject.FindGameObjectsWithTag("Player").Length > 0){
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
            
        else if(target){
            Vector3 offset;
            if(defaultFollowMode){
                if(defaultTopMode){
                    offset = defaultOffsetTop;
                }
                else{
                    offset = defaultOffset;
                }
            }
            else{
                offset = new Vector3(offsetx, offsety, offsetz);
            }
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothspeed * Time.deltaTime);
            transform.position = smoothedPosition;
        }   
    }

    public void changeFollowMode(){
        defaultFollowMode = !defaultFollowMode;
        if(defaultFollowMode && !defaultTopMode){
            offsetx = defaultOffset.x;
            offsety = defaultOffset.y;
            offsetz = defaultOffset.z;
        }
        else if(defaultFollowMode && defaultTopMode){
            offsetx = defaultOffsetTop.x;
            offsety = defaultOffsetTop.y;
            offsetz = defaultOffsetTop.z;
        }
    }

    public void changeTopMode(){
        defaultTopMode = !defaultTopMode;
        if(defaultTopMode){
            offsetx = defaultOffsetTop.x;
            offsety = defaultOffsetTop.y;
            offsetz = defaultOffsetTop.z;
            transform.rotation = Quaternion.Euler(60,0,0);
        }
        else{
            offsetx = defaultOffset.x;
            offsety = defaultOffset.y;
            offsetz = defaultOffset.z;
            transform.rotation = Quaternion.Euler(45,0,0);
        }
    }
}
