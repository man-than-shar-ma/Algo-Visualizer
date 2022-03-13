using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VersionCheck : MonoBehaviour
{
    TextMeshProUGUI versionNumber;
    // Start is called before the first frame update
    void Start()
    {
        versionNumber = gameObject.GetComponent<TextMeshProUGUI>();
        versionNumber.SetText(UnityEditor.PlayerSettings.bundleVersion);
    }
}
