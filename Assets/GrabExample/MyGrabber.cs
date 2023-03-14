using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGrabber : MonoBehaviour
{
    LineRenderer lr;
    public Transform hand;
    public OVRInput.Button button;
    public OVRInput.Controller controller;
    void Start()
    {
        lr = GetComponent<LineRenderer>();
    }
    void Update()
    {
        // 만약 잡은 물체가 있다면 
        if (null != grabObj)
        {
            // 내 손으로 점점점 다가오게 하고싶다.
            grabObj.transform.position = Vector3.Lerp(grabObj.transform.position, hand.position, Time.deltaTime * 10);

            grabObj.transform.rotation = Quaternion.Lerp(grabObj.transform.rotation, hand.rotation, Time.deltaTime * 10);
        }
        // 만약 버튼을 떼면
        if (OVRInput.GetUp(button, controller))
        {
            DoThrow();// 놓다.
        }

        Ray ray = new Ray(hand.position, hand.forward);
        lr.SetPosition(0, hand.position);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo))
        {
            lr.SetPosition(1, hitInfo.point);
            // 잡다
            if (OVRInput.GetDown(button, controller))
            {
                DoGrab(hitInfo);
            }
        }
        else
        {
            lr.SetPosition(1, hand.position + hand.forward * 1000);
        }
    }

    public MyGrabbable grabObj;
    private void DoGrab(RaycastHit hitInfo)
    {
        // 인덱스 버튼을 눌렀을 때 부딪힌 물체가
        // MyGrabbable을 상속받은 녀석이라면
        // 잡고싶다.
        MyGrabbable temp = hitInfo.transform.GetComponent<MyGrabbable>();

        if (temp)
        {
            grabObj = temp;
            grabObj.CatchYou(this);
        }
    }

    public float kAdujustForce = 5;
    private void DoThrow()
    {
        if (grabObj)
        {
            Vector3 v = OVRInput.GetLocalControllerVelocity(controller);
            Vector3 anv = OVRInput.GetLocalControllerVelocity(controller);
            grabObj.Put(v * kAdujustForce, anv);
            grabObj = null;
        }
    }
}
