using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tile : MonoBehaviour
{
    [SerializeField] private Material _baseMaterial, _offsetMaterial;
    [SerializeField] private MeshRenderer _renderer;
    [SerializeField] TextMeshProUGUI tmprovalue;

    public void Init(bool isOffset){
        _renderer.material = isOffset ? _offsetMaterial : _baseMaterial;
    }

    public void setElementValue(string text){
        tmprovalue.SetText(text);
    }

}
