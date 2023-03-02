using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 트리거 버튼을 누르면 텔레포트 선이 활성화 되고
// 트리거 버튼을 떼면 텔레포트 선이 비활성화 된다.
// 버튼을 뗏을 때 부딪힌 곳이 타워라면 그곳으로 이동하고싶다.
public class Teleport : MonoBehaviour
{
    // 트리거 버튼은 일단 마우스 오른쪽 버튼을 의미한다.
    // 선.
    LineRenderer lr;
    // 손.
    public Transform hand;
    public Player player;
    Transform target = null;
    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        if (lr == null)
        {
            lr = gameObject.AddComponent<LineRenderer>();
        }
        //lr.widthCurve = Player.instance.animCurve;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            lr.enabled = true;
        }

        if (Input.GetButtonUp("Fire2"))
        {
            lr.enabled = false;
            // 손을 뗏을 때 타워를 바라보고 있었다면 그곳으로 이동.
            if (target != null)
            {
                player.transform.position = target.position;
                target = null;
            }
            // 그렇지 않다면 아무것도 안함.
        }

        if (lr.enabled && Input.GetButton("Fire2"))
        {
            Ray ray = new Ray(hand.position, hand.forward);
            lr.SetPosition(0, hand.position);

            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                lr.SetPosition(1, hitInfo.point);
                if (hitInfo.transform.name.Contains("Tower"))
                {
                    target = hitInfo.transform;
                }
                else
                {
                    target = null;
                }
            }
            else
            {
                target = null;
                lr.SetPosition(1, hand.position + hand.forward * 1000);
            }
        }

       
      
    }
}
