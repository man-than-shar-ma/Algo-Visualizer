using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Element : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI tmprovalue;

    [SerializeField] Material[] baseMaterials;
    [SerializeField] Material[] redMaterials;
    [SerializeField] Material[] yellowMaterials;
    [SerializeField] Material[] greenMaterials;
    

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

    public void setDefaultMaterial(){
        MeshRenderer elementMesh = gameObject.GetComponent<MeshRenderer>();
        elementMesh.materials = baseMaterials;
    }

    public void setRedMaterial(){
        MeshRenderer elementMesh = gameObject.GetComponent<MeshRenderer>();
        elementMesh.materials = redMaterials;
    }

    public void setGreenMaterial(){
        MeshRenderer elementMesh = gameObject.GetComponent<MeshRenderer>();
        elementMesh.materials = greenMaterials;
    }

    void Update(){
        canvas.transform.rotation = Quaternion.LookRotation(new Vector3(0, canvas.transform.position.y - cam.transform.position.y, canvas.transform.position.z - cam.transform.position.z));
        float size = (cam.transform.position - transform.position).magnitude;
        tmprovalue.transform.localScale = new Vector3(size/5, size/5,size/5);

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
