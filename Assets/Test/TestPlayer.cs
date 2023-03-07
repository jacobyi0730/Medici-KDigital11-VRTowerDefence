using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    float h, v;
    // Update is called once per frame
    void Update()
    {
        h =  Input.GetAxis("Horizontal");
        v =  Input.GetAxis("Vertical");

        
        transform.position += new Vector3(h, 0, v).normalized * 5 * Time.deltaTime;
    }
}
