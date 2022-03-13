using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VersionDisplay : MonoBehaviour
{
    // Start is called before the first frame update

    TextMeshProUGUI version;
    void Start() {
        version = gameObject.GetComponent<TextMeshProUGUI>();
        version.SetText(Application.version);
    }
}
