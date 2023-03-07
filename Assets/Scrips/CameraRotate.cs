using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 마우스의 입력에따라 카메라를 회전하고싶다.
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
        // 마우스의 변화량을 얻어오고싶다.
        float mx = Input.GetAxis("Mouse X");
        float my = Input.GetAxis("Mouse Y");
        // 그 변화량을 누적하고싶다.
        rx += my * rotSpeed * Time.deltaTime;
        ry += mx * rotSpeed * Time.deltaTime;
        // 그 누적값을 회전값으로 사용하고싶다.
        rx = Mathf.Clamp(rx, -90, 90);
        transform.eulerAngles = new Vector3(-rx, ry, 0);
    }
}
