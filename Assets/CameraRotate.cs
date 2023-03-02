using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���콺�� �Է¿����� ī�޶� ȸ���ϰ�ʹ�.
public class CameraRotate : MonoBehaviour
{
    public float rotSpeed = 200;
    float rx;
    float ry;

    void Start()
    {

    }

    void Update()
    {
        // ���콺�� ��ȭ���� ������ʹ�.
        float mx = Input.GetAxis("Mouse X");
        float my = Input.GetAxis("Mouse Y");
        // �� ��ȭ���� �����ϰ�ʹ�.
        rx += my * rotSpeed * Time.deltaTime;
        ry += mx * rotSpeed * Time.deltaTime;
        // �� �������� ȸ�������� ����ϰ�ʹ�.
        rx = Mathf.Clamp(rx, -90, 90);
        transform.eulerAngles = new Vector3(-rx, ry, 0);
    }
}
