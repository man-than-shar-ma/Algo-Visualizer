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

    AudioSource boxDefaultSound;
    AudioSource boxRedSound;
    AudioSource boxYellowSound;
    AudioSource boxGreenSound;
    
    GameObject boxSoundsObj;

    [SerializeField] Canvas canvas;
    [SerializeField] private float boxoffset = 1;
    [SerializeField] float boxMoveSpeed = 1;

    private Camera cam;

    void Start() {
        cam = Camera.main;
        boxSoundsObj = GameObject.FindGameObjectWithTag("BoxColorSounds");
        boxDefaultSound = boxSoundsObj.transform.GetChild(0).GetComponent<AudioSource>();
        boxRedSound = boxSoundsObj.transform.GetChild(1).GetComponent<AudioSource>();
        boxYellowSound = boxSoundsObj.transform.GetChild(2).GetComponent<AudioSource>();
        boxGreenSound = boxSoundsObj.transform.GetChild(3).GetComponent<AudioSource>();
    }

    public void setElementValue(string text){
        tmprovalue.SetText(text);
    }

    public void setDefaultMaterial(){
        MeshRenderer elementMesh = gameObject.GetComponent<MeshRenderer>();
        elementMesh.materials = baseMaterials;
        boxDefaultSound.Play();
    }

    public void setRedMaterial(){
        MeshRenderer elementMesh = gameObject.GetComponent<MeshRenderer>();
        elementMesh.materials = redMaterials;
        boxRedSound.Play();
    }

    public void setYellowMaterial(){
        MeshRenderer elementMesh = gameObject.GetComponent<MeshRenderer>();
        elementMesh.materials = yellowMaterials;
        boxYellowSound.Play();
    }

    public void setGreenMaterial(){
        MeshRenderer elementMesh = gameObject.GetComponent<MeshRenderer>();
        elementMesh.materials = greenMaterials;
        boxGreenSound.Play();
    }

    void Update(){
        canvas.transform.rotation = Quaternion.LookRotation(new Vector3(0, canvas.transform.position.y - cam.transform.position.y, canvas.transform.position.z - cam.transform.position.z));
        float size = (cam.transform.position - transform.position).magnitude;
        tmprovalue.transform.localScale = new Vector3(size/5, size/5, size/5);

    }

    public IEnumerator LiftElementUp(float speed = 1f){
        Vector3 newpos = new Vector3(transform.position.x, transform.position.y+boxoffset, transform.position.z);
            while(transform.position.y < newpos.y){
                transform.position = new Vector3(transform.position.x,transform.position.y+boxMoveSpeed*Time.deltaTime*speed, transform.position.z);
                yield return null;
            }
            transform.position = newpos;
    }

    public IEnumerator DropElementDown(float speed = 1f){
        Vector3 newpos = new Vector3(transform.position.x, transform.position.y-boxoffset, transform.position.z);
            while(transform.position.y > newpos.y){
                transform.position = new Vector3(transform.position.x,transform.position.y-boxMoveSpeed*Time.deltaTime*speed, transform.position.z);
                yield return null;
            }
            transform.position = newpos;
    }
    
}
