using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGrabbable : MonoBehaviour
{
    protected Rigidbody rb;
    virtual public void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    virtual public void DoAction()
    {

    }

    virtual public void CatchYou(MyGrabber grabber)
    {
        rb.isKinematic = true;
        transform.parent = grabber.transform;
    }

    virtual public void Put(Vector3 velocity, Vector3 angularVelocity)
    {
        rb.isKinematic = false;
        transform.parent = null;
        rb.velocity = velocity;
        rb.angularVelocity = angularVelocity;
    }
}
