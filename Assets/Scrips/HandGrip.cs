using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� �ڵ�Ʈ���Ÿ� ������ �� �ݴ��� ��Ʈ�ѷ����� �Ÿ��� 2M ���϶��
// �տ� ��ź(GripObject)�� ���ʹ�.
// ���� �ڵ�Ʈ���Ÿ� ���� �� �տ� ��ź�� �ִٸ� ����ʹ�.
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
        // ���� �ڵ�Ʈ���Ÿ� ������ ��
        if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, controlller))
        {
            DoGrab();
        }

        // ���� �ڵ�Ʈ���Ÿ� ���� �� 
        if (OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger, controlller))
        {
            DoThrow();
        }
    }

    public float kAdjustVelocity = 5;
    private void DoThrow()
    {
        // �տ� ��ź�� �ִٸ�
        if (null != gripObj)
        {
            // ����ʹ�.
            // �θ��ڽ� ���踦 ����
            gripObj.transform.parent = null;
            // ��ź�� ������ �Ѱ�
            Rigidbody bombRB = gripObj.GetComponent<Rigidbody>();
            bombRB.isKinematic = false;
            // ��Ʈ�ѷ��� �ӵ��� ���ӵ��� ��ź���� �Ѱ��ְ�ʹ�.
            bombRB.velocity = OVRInput.GetLocalControllerVelocity(controlller) * kAdjustVelocity;
            bombRB.angularVelocity = OVRInput.GetLocalControllerAngularVelocity(controlller);
            // ���� Bomb�̶�� "�տ��� �������� ������ ��" ��� �ϰ�ʹ�.
            Bomb bomb = gripObj.GetComponent<Bomb>();
            if (null != bomb)
            {
                bomb.canExplosion = true;
            }
            // �������� "��ü�� ���Ҵ�" ��� �ϰ�ʹ�.
            gripObj = null;
        }
    }

    private void DoGrab()
    {
        // �ݴ��� ��Ʈ�ѷ����� �Ÿ��� 2M ���϶��
        float dist = Vector3.Distance(transform.position, otherHand.position);
        if (dist < 2)
        {
            // �տ� ��ź(GripObject)�� ���ʹ�.
            GameObject bomb = Instantiate(bombFactory);
            // ���� �θ� = ��
            bomb.transform.parent = transform;
            bomb.transform.localPosition = Vector3.zero;
            bomb.transform.forward = transform.forward;
            // bomb�� ������ ����ʹ�.
            Rigidbody bombRB = bomb.GetComponent<Rigidbody>();
            bombRB.isKinematic = true;
            // �տ� �� ��ź�� ����ϰ�ʹ�.
            gripObj = bomb;

        }
    }
}
