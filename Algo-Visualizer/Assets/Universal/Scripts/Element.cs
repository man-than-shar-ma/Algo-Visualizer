using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Element : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI tmprovalue;
    [SerializeField] Canvas canvas;

    private Camera cam;

    void Start() {
        cam = Camera.main;
    }

    public void elementValueSet(string text){
        tmprovalue.SetText(text);
    }

    void Update(){
        canvas.transform.rotation = Quaternion.LookRotation(canvas.transform.position - cam.transform.position);
    }
    
}
