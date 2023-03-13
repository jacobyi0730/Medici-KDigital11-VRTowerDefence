using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 왼쪽 컨트롤러의 One버튼을 이용해서 텔레포트 하고싶다.
// 컨트롤러에서 컨트롤러 앞방향으로 가상의 점프를 하고싶다.
// 그 궤적으로 텔레포트를 구현하고싶다.
// 닿은곳이 있다면 그곳에 텔레포트아이콘을 배치하고싶다.
public class TeleportCurve : MonoBehaviour
{
    public Transform hand;
    public OVRInput.Controller controller;
    public OVRInput.Button button;
    public GameObject icon;
    public LineRenderer lr;


    // Start is called before the first frame update
    void Start()
    {
        //InvokeRepeating("TryJump", 1, 3);
    }
    //void TryJump()
    //{
    //    simulObject.position = transform.position;
    //}

    public float gravity = -9.81f;
    public float jumpPower = 150;
    public int maxCount = 100;
    public Transform player;
    public float kAdjust = 0.1f;
    Vector3 movePosition;
    // Update is called once per frame
    void Update()
    {
        // 만약 키를 뗏다면 그 위치로 이동하고싶다. (라인, 아이콘 비활성)
        if (OVRInput.GetUp(button, controller))
        {
            lr.enabled = false;
            icon.SetActive(false);
            player.position = movePosition;
        }

        // 만약 키를 누르는 중이라면 (라인과 아이콘을 활성)
        if (OVRInput.GetDown(button, controller))
        {
            lr.enabled = true;
            icon.SetActive(true);
        }

        if (true == lr.enabled)
        {
            // 힘을 가하고
            float simDT = 1f / maxCount;
            Vector3 velocity = hand.transform.forward * jumpPower;
            Vector3 pos = hand.transform.position;
            Vector3[] points = new Vector3[maxCount + 1];
            points[0] = pos;
            // 선의 궤적을 100개 생성하고싶다. 
            int lrMaxPositions = maxCount + 1;
            bool isHit = false;
            for (int i = 0; i < maxCount; i++)
            {
                velocity += Vector3.up * gravity;
                pos += velocity * simDT;
                points[i + 1] = pos;
                // 만약 이전위치와 새로운위치 사이에 물체가있다면
                Vector3 direction = points[i + 1] - points[i];
                Ray ray = new Ray(points[i], direction.normalized);
                RaycastHit hitInfo;
                // 그 물체와 부딪힌곳까지만 궤적으로 하고싶다.
                int layer = 1 << LayerMask.NameToLayer("Tower") | 1 << LayerMask.NameToLayer("Ground");
                if (Physics.Raycast(ray, out hitInfo, direction.magnitude, layer))
                {
                    movePosition = hitInfo.point;
                    points[i + 1] = hitInfo.point;
                    lrMaxPositions = i + 2;
                    icon.transform.position = hitInfo.point;
                    icon.transform.up = hitInfo.normal;
                    float dist = Vector3.Distance(hand.transform.position, hitInfo.point);
                    icon.transform.localScale = Vector3.one * dist * kAdjust;
                    isHit = true;
                    break;
                }
            }

            if (false == isHit)
            {
                Vector3 target = points[points.Length - 1];
                icon.transform.position = target;
                icon.transform.up = Vector3.up;
                float dist = Vector3.Distance(hand.transform.position, target);
                icon.transform.localScale = Vector3.one * dist * kAdjust;
            }

            lr.positionCount = lrMaxPositions;
            lr.SetPositions(points);
        }
    }
}
