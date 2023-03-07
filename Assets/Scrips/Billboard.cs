using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }
    Camera cam;
    // Update is called once per frame
    void Update()
    {
        transform.rotation = cam.transform.rotation;
    }
}
