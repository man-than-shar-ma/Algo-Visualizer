using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JSONAddressLoader : MonoBehaviour
{
    [SerializeField]
    AlertPanelUI alertPanelUI;

    public void callJSONAddressLoader(){
        string returnedAddress = JSONHandler.loadJSONAddress();
        StartCoroutine(alertPanelUI.alertAnim("alert", returnedAddress));
    }
}
