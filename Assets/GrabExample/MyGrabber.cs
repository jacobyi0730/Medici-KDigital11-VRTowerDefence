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
        // ���� ���� ��ü�� �ִٸ� 
        if (null != grabObj)
        {
            // �� ������ ������ �ٰ����� �ϰ�ʹ�.
            grabObj.transform.position = Vector3.Lerp(grabObj.transform.position, hand.position, Time.deltaTime * 10);

            grabObj.transform.rotation = Quaternion.Lerp(grabObj.transform.rotation, hand.rotation, Time.deltaTime * 10);
        }
        // ���� ��ư�� ����
        if (OVRInput.GetUp(button, controller))
        {
            DoThrow();// ����.
        }

        Ray ray = new Ray(hand.position, hand.forward);
        lr.SetPosition(0, hand.position);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo))
        {
            lr.SetPosition(1, hitInfo.point);
            // ���
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
        // �ε��� ��ư�� ������ �� �ε��� ��ü��
        // MyGrabbable�� ��ӹ��� �༮�̶��
        // ���ʹ�.
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
