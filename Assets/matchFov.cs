using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class matchFov : MonoBehaviour
{
    public Camera sourceCamera;
    public Camera targetCamera;
    // Start is called before the first frame update
    void Start()
    {
        targetCamera.fieldOfView = sourceCamera.fieldOfView;
    }
}
