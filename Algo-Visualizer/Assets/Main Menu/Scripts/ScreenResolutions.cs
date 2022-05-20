using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenResolutions : MonoBehaviour
{
    public static Resolution[] screenResolutions;
    // Start is called before the first frame update
    void Start()
    {
        screenResolutions = Screen.resolutions;
    }
}
