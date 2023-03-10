using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 만약 핸드트리거를 눌렀을 때 반대쪽 컨트롤러와의 거리가 2M 이하라면
// 손에 폭탄(GripObject)을 쥐고싶다.
// 만약 핸드트리거를 뗏을 때 손에 폭탄이 있다면 놓고싶다.
public class HandGrip : MonoBehaviour
{
    public OVRInput.Controller controlller;
    public Transform otherHand;
    public GameObject bombFactory;

    GameObject gripObj;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // 만약 핸드트리거를 눌렀을 때
        if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, controlller))
        {
            DoGrab();
        }

        // 만약 핸드트리거를 뗏을 때 
        if (OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger, controlller))
        {
            DoThrow();
        }
    }

    public float kAdjustVelocity = 5;
    private void DoThrow()
    {
        // 손에 폭탄이 있다면
        if (null != gripObj)
        {
            // 놓고싶다.
            // 부모자식 관계를 끊고
            gripObj.transform.parent = null;
            // 폭탄의 물리를 켜고
            Rigidbody bombRB = gripObj.GetComponent<Rigidbody>();
            bombRB.isKinematic = false;
            // 컨트롤러의 속도와 각속도를 폭탄에게 넘겨주고싶다.
            bombRB.velocity = OVRInput.GetLocalControllerVelocity(controlller) * kAdjustVelocity;
            bombRB.angularVelocity = OVRInput.GetLocalControllerAngularVelocity(controlller);
            // 만약 Bomb이라면 "손에서 놓았으니 터져도 되" 라고 하고싶다.
            Bomb bomb = gripObj.GetComponent<Bomb>();
            if (null != bomb)
            {
                bomb.canExplosion = true;
            }
            // 던졌으면 "물체를 놓았다" 라고 하고싶다.
            gripObj = null;
        }
    }

    private void DoGrab()
    {
        // 반대쪽 컨트롤러와의 거리가 2M 이하라면
        float dist = Vector3.Distance(transform.position, otherHand.position);
        if (dist < 2)
        {
            // 손에 폭탄(GripObject)을 쥐고싶다.
            GameObject bomb = Instantiate(bombFactory);
            // 나의 부모 = 너
            bomb.transform.parent = transform;
            bomb.transform.localPosition = Vector3.zero;
            bomb.transform.forward = transform.forward;
            // bomb의 물리를 끄고싶다.
            Rigidbody bombRB = bomb.GetComponent<Rigidbody>();
            bombRB.isKinematic = true;
            // 손에 쥔 폭탄을 기억하고싶다.
            gripObj = bomb;

        }
    }
}
