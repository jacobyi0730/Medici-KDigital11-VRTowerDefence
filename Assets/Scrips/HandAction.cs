using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 마우스 왼쪽 버튼을 누르면 hand에서 hand의 앞방향으로
// raycast를 이용해서 총을 쏘고싶다.
// 부딪힌 위치에 총알자국을 남기고싶다.
[RequireComponent(typeof(LineRenderer))]
public class HandAction : MonoBehaviour
{
    enum Hall
    {
        NO,
        YES
    }
    public Transform hand;
    public GameObject[] bImpactFactoryList;

    LineRenderer lr;

    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
    }

    float zoom = 60;
    float zoomSpeed = 50;
    public Camera scopeCamera;
    // Update is called once per frame
    void Update()
    {
        // -1, 0 , 1
        Vector2 axis = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.RTouch);
        zoom += zoomSpeed * axis.y * Time.deltaTime;
        zoom = Mathf.Clamp(zoom, 10, 150);
        scopeCamera.fieldOfView = zoom;


        // hand에서 hand의 앞방향으로
        Ray ray = new Ray(hand.position, hand.forward);
        lr.SetPosition(0, ray.origin);
        RaycastHit hitInfo;
        // raycast를 이용해서 총을 쏘고싶다.
        if (Physics.Raycast(ray, out hitInfo))
        {
            lr.SetPosition(1, hitInfo.point);
            
            // 마우스 왼쪽 버튼을 누르면
            //if (Input.GetButtonDown("Fire1"))
            if (OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.RTouch))
            {
                GameObject prefab = null;
                // 부딪힌 위치에 총알자국을 남기고싶다.
                if (hitInfo.transform.CompareTag("Enemy"))
                {
                    prefab = bImpactFactoryList[(int)Hall.NO];

                    Enemy enemy = hitInfo.transform.GetComponent<Enemy>();

                    enemy.OnMyDamageProcess(1);


                }
                else
                {
                    prefab = bImpactFactoryList[(int)Hall.YES];
                }
                GameObject bi = Instantiate(prefab);

                bi.transform.position = hitInfo.point;
                bi.transform.forward = hitInfo.normal;
            }
        }
        else
        {
            lr.SetPosition(1, ray.origin + ray.direction * 1000);
        }
    }
}
