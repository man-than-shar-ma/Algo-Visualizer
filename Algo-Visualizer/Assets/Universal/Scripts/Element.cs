using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Element : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI tmprovalue;
    [SerializeField] Canvas canvas;
    [SerializeField] private float boxoffset = 1;
    [SerializeField] float boxMoveSpeed = 1;

    private Camera cam;

    void Start() {
        cam = Camera.main;
    }

    public void setElementValue(string text){
        tmprovalue.SetText(text);
    }

    void Update(){
        canvas.transform.rotation = Quaternion.LookRotation(canvas.transform.position - cam.transform.position);
    }

    public IEnumerator LiftElementUp(){
        Vector3 newpos = new Vector3(transform.position.x, transform.position.y+boxoffset, transform.position.z);
            while(transform.position.y < newpos.y){
                transform.position = new Vector3(transform.position.x,transform.position.y+boxMoveSpeed*Time.deltaTime, transform.position.z);
                yield return null;
            }
            transform.position = newpos;
    }

    public IEnumerator DropElementDown(){
        Vector3 newpos = new Vector3(transform.position.x, transform.position.y-boxoffset, transform.position.z);
            while(transform.position.y > newpos.y){
                transform.position = new Vector3(transform.position.x,transform.position.y-boxMoveSpeed*Time.deltaTime, transform.position.z);
                yield return null;
            }
            transform.position = newpos;
    }
    
}
